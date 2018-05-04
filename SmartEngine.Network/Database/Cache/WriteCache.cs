using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading;

namespace SmartEngine.Network.Database.Cache
{
    public class WriteCache<KeyType, ValueType>
    {
        /// <summary>
        /// 數據保存的地方
        /// </summary>
        protected ConcurrentQueue<CacheDataInfo<KeyType, ValueType>> CacheData = new ConcurrentQueue<CacheDataInfo<KeyType, ValueType>>();
        /// <summary>
        /// 索引數據的位置
        /// </summary>
        protected ConcurrentDictionary<KeyType, CacheDataInfo<KeyType, ValueType>> IndexData = new ConcurrentDictionary<KeyType, CacheDataInfo<KeyType, ValueType>>();
        /// <summary>
        /// 最大等待寫入時間(MicroSec)
        /// </summary>
        protected int MaxWaitTime { get { return Cache<KeyType, ValueType>.WriteMaxWaitTime; } }
        /// <summary>
        /// 每次寫入的數據量
        /// </summary>
        protected int SaveDataCountPerTimes { get { return Cache<KeyType, ValueType>.SaveDataCountPerTimes; } }
        /// <summary>
        /// 是否要結束了
        /// </summary>
        protected bool Closing = false;

        /// <summary>
        /// DB指令運行方式
        /// </summary>
        protected ICacheDBExecuteType ExecuteType = ICacheDBExecuteType.Sync;

        #region Statistic

        /// <summary>
        /// 要求的寫入次數
        /// </summary>
        public ulong RequestTimes = 0;
        /// <summary>
        /// 已執行的寫入次數
        /// </summary>
        public ulong ExecutedRequestTimes = 0;
        /// <summary>
        /// 總重試次數
        /// </summary>
        public ulong TotalRetryTimes = 0;
        /// <summary>
        /// 平均等待時間
        /// </summary>
        /// <remarks>單位：毫秒</remarks>
        public double AvgWaitingTime = 0;

        #endregion

        /// <summary>
        /// 通知Cache數據被捨棄的介面
        /// </summary>
        /// <param name="data"></param>
        internal delegate void OnDiscardDataHandler(CacheDataInfo<KeyType, ValueType> data);
        /// <summary>
        /// 通知Cache數據被捨棄的事件
        /// </summary>
        internal event OnDiscardDataHandler OnDiscardData;

        protected AutoResetEvent waiter = new AutoResetEvent(false);
        /// <summary>
        /// Cache存取DB的界面
        /// </summary>
        protected ICacheDBHandler<KeyType, ValueType> DBHandler;

        public WriteCache(ICacheDBHandler<KeyType, ValueType> DBHandler)
        {
            this.DBHandler = DBHandler;
            this.ExecuteType = DBHandler.ExecuteType;
            this.DBHandler.SaveResultCallback += new SaveResultCallbackHandler<KeyType, ValueType>(DBHandler_SaveResultCallback);
            SaveThread = new Thread(new ThreadStart(SaveData));
            SaveThread.Start();
        }

        void DBHandler_SaveResultCallback(IEnumerable<KeyType> success = null, IEnumerable<KeyType> fails = null)
        {
            if (success != null)
            {
                foreach (var key in success)
                {
                    if (IndexData.ContainsKey(key))
                    {
                        var cData = IndexData[key];
                        cData.IsNeedToWrite = false;
                        cData.SaveRetryTimes = 0;
                    }
                    Remove(key);
                }
            }
            if (fails != null)
            {
                foreach (var key in fails)
                {
                    CacheDataInfo<KeyType, ValueType> cData = null;
                    if (IndexData.ContainsKey(key))
                    {
                        cData = IndexData[key];
                    }
                    else
                    {
                        cData = CacheData.AsParallel().Where(o => o.Key.Equals(key)).FirstOrDefault();
                    }
                    if (cData != null)
                    {
                        TotalRetryTimes += 1;
                        cData.SaveRetryTimes += 1;
                        if (cData.SaveRetryTimes < Cache<KeyType, ValueType>.MaxSaveRetryTimes)
                        {
                            Add(ref cData, true);
                        }
                        else
                        {
                            if (OnDiscardData != null)
                            {
                                OnDiscardData.Invoke(cData);
                            }
                        }
                    }
                }
            }
        }

        ~WriteCache()
        {
            Shutdown();
        }

        public void Shutdown()
        {
            if (SaveThread != null)
            {
                if (CacheData.Count > 0)
                {
                    try
                    {
                        Closing = true;
                        waiter.Set();
                        SaveThread.Join();
                        if (CacheData.Count > 0)
                        {
                            FlushAll();
                        }
                    }
                    catch (Exception) { }
                }
            }
        }

        #region Property

        /// <summary>
        /// 目前內含數據數量
        /// </summary>
        public int Count { get { return CacheData.Count; } }

        #endregion

        #region Method

        public void Remove(KeyType key)
        {
            CacheDataInfo<KeyType, ValueType> t;
            IndexData.TryRemove(key, out t);
        }

        public void Add(KeyType key, ValueType data)
        {
            var dt = new CacheDataInfo<KeyType, ValueType>(key, data);
            Add(ref dt);
        }

        public void Add(ref CacheDataInfo<KeyType, ValueType> info, bool IsRetry = false)
        {
            IndexData[info.Key] = info;
            CacheData.Enqueue(info);
            info.WriteWaitTime = DateTime.Now;
            if (!IsRetry)
            {
                RequestTimes += 1;
            }
            waiter.Set();
        }

        public void Add(ref List<CacheDataInfo<KeyType, ValueType>> info, bool IsRetry = false)
        {
            foreach (var i in info)
            {
                IndexData[i.Key] = i;
                CacheData.Enqueue(i);
                i.WriteWaitTime = DateTime.Now;
            }
            if (!IsRetry)
            {
                RequestTimes += (ulong)info.Count;
            }
            waiter.Set();
        }

        public void AddCreate(ref CacheDataInfo<KeyType, ValueType> info)
        {
            info.Action = CacheDataInfo<KeyType, ValueType>.ActionType.Create;
            Add(ref info);
        }

        public void AddDelete(ref CacheDataInfo<KeyType, ValueType> info)
        {
            info.Action = CacheDataInfo<KeyType, ValueType>.ActionType.Delete;
            Add(ref info);
        }

        internal CacheDataInfo<KeyType, ValueType> Find(KeyType key)
        {
            CacheDataInfo<KeyType, ValueType> ret = null;
            if (IndexData.ContainsKey(key))
            {
                ret = IndexData[key];
            }
            return ret;
        }

        /// <summary>
        /// 將數據寫入DB
        /// (預設寫入 SaveDataCountPerTimes 筆資料)
        /// </summary>
        public void Flush()
        {
            List<ValueType> data = new List<ValueType>();
            List<ValueType> dataDelete = new List<ValueType>();
            List<CacheDataInfo<KeyType, ValueType>> dataDeleteCache = new List<CacheDataInfo<KeyType, ValueType>>();
            List<CacheDataInfo<KeyType, ValueType>> dataSaveCache = new List<CacheDataInfo<KeyType, ValueType>>();
            //List<CacheDataInfo<KeyType, ValueType>> dataCreateCache = new List<CacheDataInfo<KeyType, ValueType>>();

            double TotalWaitTime = AvgWaitingTime * ExecutedRequestTimes;

            for (int i = 0; i < SaveDataCountPerTimes && CacheData.Count > 0; i++)
            {
                CacheDataInfo<KeyType, ValueType> cData = null;
                if (!CacheData.TryDequeue(out cData))
                {
                    continue;
                }
                ExecutedRequestTimes += 1;
                TotalWaitTime += (DateTime.Now - cData.WriteWaitTime).TotalMilliseconds;
                switch (cData.Action)
                {
                    case CacheDataInfo<KeyType, ValueType>.ActionType.Create:
                        if (ExecuteType == ICacheDBExecuteType.Sync)
                        {
                            if (DBHandler.CreateData(cData.Key, cData.Value) == ICacheDBSaveResult.Fail)
                            {
                                DBHandler_SaveResultCallback(fails: new KeyType[] { cData.Key });
                            }
                            else
                            {
                                DBHandler_SaveResultCallback(success: new KeyType[] { cData.Key });
                            }
                        }
                        else
                        {
                            DBHandler.BeginCreateData(cData.Key, cData.Value);
                        }
                        break;
                    case CacheDataInfo<KeyType, ValueType>.ActionType.Delete:
                        dataDelete.Add(cData.Value);
                        dataDeleteCache.Add(cData);
                        break;
                    case CacheDataInfo<KeyType, ValueType>.ActionType.Update:
                    default:
                        data.Add(cData.Value);
                        dataSaveCache.Add(cData);
                        break;
                }
            }

            AvgWaitingTime = TotalWaitTime / ExecutedRequestTimes;

            if (ExecuteType == ICacheDBExecuteType.Sync)
            {
                IEnumerable<KeyType> success = null;
                IEnumerable<KeyType> fails = null;
                DBHandler.SaveData(ref data, out  success, out fails);

                DBHandler_SaveResultCallback(success,fails);
            }
            else
            {
                DBHandler.BeginSaveData(ref data);
            }

            if (ExecuteType == ICacheDBExecuteType.Sync)
            {
                IEnumerable<KeyType> success = null;
                IEnumerable<KeyType> fails = null;
                DBHandler.DeleteData(ref dataDelete, out  success, out fails);

                DBHandler_SaveResultCallback(success, fails);
            }
            else
            {
                DBHandler.BeginDeleteData(ref data);
            }
        }

        /// <summary>
        /// 將全部數據寫入DB
        /// </summary>
        protected void FlushAll()
        {
            List<ValueType> data = new List<ValueType>();
            List<ValueType> dataDelete = new List<ValueType>();
            List<CacheDataInfo<KeyType, ValueType>> dataDeleteCache = new List<CacheDataInfo<KeyType, ValueType>>();
            List<CacheDataInfo<KeyType, ValueType>> dataSaveCache = new List<CacheDataInfo<KeyType, ValueType>>();
            List<CacheDataInfo<KeyType, ValueType>> dataCreateCache = new List<CacheDataInfo<KeyType, ValueType>>();
            List<CacheDataInfo<KeyType, ValueType>> alldata;
            alldata = (from d in CacheData select d).ToList();
            double TotalWaitTime = AvgWaitingTime * ExecutedRequestTimes;

            //IndexData.Clear();
            foreach (var cData in alldata)
            {
                switch (cData.Action)
                {
                    case CacheDataInfo<KeyType, ValueType>.ActionType.Create:
                        if (ExecuteType == ICacheDBExecuteType.Sync)
                        {
                            if (DBHandler.CreateData(cData.Key, cData.Value) == ICacheDBSaveResult.Fail)
                            {
                                DBHandler_SaveResultCallback(fails: new KeyType[] { cData.Key });
                            }
                            else
                            {
                                DBHandler_SaveResultCallback(success: new KeyType[] { cData.Key });
                            }
                        }
                        else
                        {
                            DBHandler.BeginCreateData(cData.Key, cData.Value);
                        }
                        break;
                    case CacheDataInfo<KeyType, ValueType>.ActionType.Delete:
                        dataDelete.Add(cData.Value);
                        dataDeleteCache.Add(cData);
                        break;
                    case CacheDataInfo<KeyType, ValueType>.ActionType.Update:
                    default:
                        data.Add(cData.Value);
                        dataSaveCache.Add(cData);
                        break;
                }
            }

            AvgWaitingTime = TotalWaitTime / ExecutedRequestTimes;

            if (ExecuteType == ICacheDBExecuteType.Sync)
            {
                IEnumerable<KeyType> success = null;
                IEnumerable<KeyType> fails = null;
                DBHandler.SaveData(ref data, out  success, out fails);

                DBHandler_SaveResultCallback(success, fails);
            }
            else
            {
                DBHandler.BeginSaveData(ref data);
            }

            if (ExecuteType == ICacheDBExecuteType.Sync)
            {
                IEnumerable<KeyType> success = null;
                IEnumerable<KeyType> fails = null;
                DBHandler.DeleteData(ref dataDelete, out  success, out fails);

                DBHandler_SaveResultCallback(success, fails);
            }
            else
            {
                DBHandler.BeginDeleteData(ref data);
            }
        }

        #endregion

        #region Thread

        /// <summary>
        /// 數據儲存的Thread
        /// </summary>
        protected Thread SaveThread;

        /// <summary>
        /// 數據儲存的Thread所執行的Method
        /// </summary>
        protected void SaveData()
        {
            while (!Closing)
            {
                if (DBHandler.IsConnected())
                {
                    if (CacheData.Count > 0)
                    {
                        Flush();
                    }
                }
                if (CacheData.Count == 0)
                {
                    waiter.WaitOne();
                }
            }
        }

        #endregion

    }
}
