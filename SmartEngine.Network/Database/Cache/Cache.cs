using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace SmartEngine.Network.Database.Cache
{
    /// <summary>
    /// 数据库缓存类
    /// </summary>
    /// <typeparam name="KeyType">Key</typeparam>
    /// <typeparam name="ValueType">Value</typeparam>
    public abstract class Cache<KeyType, ValueType>
    {
        #region Setting

        /// <summary>
        /// 可容納的最大量
        /// </summary>
        protected int capacity = 1000;
        /// <summary>
        /// 一次移除多少個數據
        /// </summary>
        protected int EldestRemoveCount = 50;
        /// <summary>
        /// 最大等待寫入時間(MicroSec)
        /// </summary>
        public static int WriteMaxWaitTime = 30;
        /// <summary>
        /// 每次寫入的數據量
        /// </summary>
        public static int SaveDataCountPerTimes = 10;

        #endregion

        #region Statistic

        /// <summary>
        /// 保存失敗時重試次數
        /// </summary>
        public static int MaxSaveRetryTimes = 3;
        /// <summary>
        /// 清理老數據的次數
        /// </summary>
        public uint EldestDataClearTimes = 0;
        /// <summary>
        /// 清理老數據的平均時間
        /// </summary>
        public TimeSpan EldestDataClearAvgTime = TimeSpan.FromSeconds(0);
        /// <summary>
        /// Cache請求次數
        /// </summary>
        public ulong CacheRequestTimes = 0;
        /// <summary>
        /// Cache請求命中次數
        /// </summary>
        public ulong CacheRequestHitTimes = 0;
        /// <summary>
        /// 寫入請求次數
        /// </summary>
        public ulong WriteRequestTimes { get { return DBWriter.RequestTimes; } }
        /// <summary>
        /// 寫入請求運行次數
        /// </summary>
        public ulong ExecutedRequestTimes { get { return DBWriter.ExecutedRequestTimes; } }
        /// <summary>
        /// 寫入重試次數
        /// </summary>
        public ulong TotalRetryTimes { get { return DBWriter.TotalRetryTimes; } }
        /// <summary>
        /// 寫入平均等待時間
        /// </summary>
        public TimeSpan WriteAvgWaitingTime { get { return TimeSpan.FromMilliseconds(DBWriter.AvgWaitingTime); } }


        #endregion

        /// <summary>
        /// 數據保存的地方
        /// </summary>
        public ConcurrentDictionary<KeyType, CacheDataInfo<KeyType, ValueType>> CacheData = new ConcurrentDictionary<KeyType, CacheDataInfo<KeyType, ValueType>>();
        /// <summary>
        /// 寫入數據的類別
        /// </summary>
        protected WriteCache<KeyType, ValueType> DBWriter;
        /// <summary>
        /// 存取DB類別
        /// </summary>
        protected ICacheDBHandler<KeyType, ValueType> DBHandler;

        /// <summary>
        /// 最大的Key值
        /// </summary>
        protected KeyType MaxIdentity;

        protected object IdentityLocker = new object();

        public Cache(ICacheDBHandler<KeyType, ValueType> DBHandler)
        {
            DBWriter = new WriteCache<KeyType, ValueType>(DBHandler);
            this.DBHandler = DBHandler;
            MaxIdentity = DBHandler.GetMaxID<ValueType>();
            GetNewIdentity();// ID++

            DBWriter.OnDiscardData += new WriteCache<KeyType, ValueType>.OnDiscardDataHandler(DBWriter_OnDiscardData);
        }

        protected void DBWriter_OnDiscardData(CacheDataInfo<KeyType, ValueType> data)
        {
            Remove(data.Key, false);
        }

        ~Cache()
        {
            Shutdown();
        }

        public void Shutdown()
        {
            if (DBWriter != null)
            {
                DBWriter.Shutdown();
            }
        }

        #region Property
        /// <summary>
        /// 最大容量
        /// </summary>
        public int Capacity { get { return capacity; } }

        /// <summary>
        /// 目前內含數據數量
        /// </summary>
        public int Count { get { return CacheData.Count; } }

        #endregion

        #region Method

        /// <summary>
        /// 從Cache中依據指定條件尋找數據
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IEnumerable<ValueType> Find(Func<ValueType, bool> condition)
        {
            return CacheData.AsParallel().Where(o => condition.Invoke(o.Value.Value)).Select(o => o.Value.Value);
        }

        /// <summary>
        /// 新增或更新數據
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public void Add(KeyType key, ValueType data)
        {
            if (CacheData.ContainsKey(key))
            {
                CacheData[key].Value = data;
                return;
            }
            if (IsFull())
            {
                RemoveEldest();
            }

            CacheDataInfo<KeyType, ValueType> info = new CacheDataInfo<KeyType, ValueType>(key, data);

            CacheData[key] = info;
        }

        /// <summary>
        /// 從Cache中移除數據
        /// </summary>
        /// <param name="key">key</param>
        public void Remove(KeyType key)
        {
            Remove(key, true);
        }

        /// <summary>
        /// 從Cache中移除數據
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="NotifyWriteCache">通知WriteCache同步清除數據</param>
        protected void Remove(KeyType key, bool NotifyWriteCache)
        {
            CacheDataInfo<KeyType, ValueType> t;
            CacheData.TryRemove(key, out t);
            if (NotifyWriteCache)
            {
                DBWriter.Remove(key);
            }
        }

        /// <summary>
        /// 是否滿了
        /// </summary>
        /// <returns></returns>
        public bool IsFull()
        {
            return CacheData.Count >= capacity;
        }

        /// <summary>
        /// 移除最舊的數據(前 EldestRemoveCount 個)
        /// </summary>
        protected void RemoveEldest()
        {
            DateTime dtBefore = DateTime.Now;

            var oldData = (from p in CacheData
                           orderby p.Value.LastModifyTime ascending
                           select p.Value).Take(EldestRemoveCount).ToList();

            foreach (CacheDataInfo<KeyType, ValueType> i in oldData)
            {
                CacheDataInfo<KeyType, ValueType> t = null;
                if (CacheData.TryRemove(i.Key, out t) && t != null && t.IsNeedToWrite)
                {
                    DBWriter.Add(ref t);
                }
            }

            double total = EldestDataClearAvgTime.TotalMilliseconds * EldestDataClearTimes + (DateTime.Now - dtBefore).TotalMilliseconds;
            EldestDataClearTimes += 1;
            EldestDataClearAvgTime = TimeSpan.FromMilliseconds(total / EldestDataClearTimes);
        }

        /// <summary>
        /// 取得數據
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>數據</returns>
        public ValueType Get(KeyType key)
        {
            CacheRequestTimes += 1;
            if (CacheData.ContainsKey(key))
            {
                CacheRequestHitTimes += 1;
                CacheDataInfo<KeyType, ValueType> ret = CacheData[key];
                if (ret.IsNeedToWrite)
                {
                    DBWriter.Add(ref ret);
                }
                return ret.Value;
            }

            CacheDataInfo<KeyType, ValueType> wData = DBWriter.Find(key);
            if (wData != null)
            {
                Add(key, wData.Value);
                return wData.Value;
            }

            ValueType data = DBHandler.LoadData(key);
            if (data != null)
            {
                Add(key, data);
            }
            return data;
        }

        /// <summary>
        /// 取得數據
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>數據</returns>
        public ValueType this[KeyType key]
        {
            get
            {
                return this.Get(key);
            }
        }

        /// <summary>
        /// 儲存數據
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="data">數據</param>
        public void Save(KeyType key, ValueType data)
        {
            DBWriter.Add(key, data);
            Add(key, data);
        }

        /// <summary>
        /// 儲存數據
        /// </summary>
        /// <param name="info">數據</param>
        protected void Save(ref CacheDataInfo<KeyType, ValueType> info)
        {
            Save(info.Key, info.Value);
        }

        /// <summary>
        /// 取得新的ID
        /// </summary>
        protected KeyType GetNewIdentity()
        {
            KeyType value;
            lock (IdentityLocker)
            {
                value = MaxIdentity;
                MaxIdentity = IncraseIdentity(MaxIdentity);

            }
            return value;
        }

        /// <summary>
        /// 建立數據
        /// </summary>
        /// <param name="data">數據</param>
        /// <returns>ID</returns>
        public KeyType Create(ValueType data)
        {
            KeyType ID = GetNewIdentity();
            var info = new CacheDataInfo<KeyType, ValueType>(ID, data);
            DBWriter.AddCreate(ref info);
            Add(ID, data);
            return ID;
        }

        /// <summary>
        /// 刪除一筆數據
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="data"></param>
        public void Delete(KeyType key, ValueType data)
        {
            CacheDataInfo<KeyType, ValueType> info = null;
            CacheData.TryRemove(key, out info);
            if (info == null)
            {
                info = new CacheDataInfo<KeyType, ValueType>(key, data);
            }
            DBWriter.AddDelete(ref info);
        }

        /// <summary>
        /// 刪除一筆數據
        /// </summary>
        /// <param name="key">Key</param>
        public void Delete(KeyType key)
        {
            if (CacheData.ContainsKey(key))
            {
                Delete(key, CacheData[key].Value);
            }
        }

        #endregion

        #region MustOverride

        /// <summary>
        /// 增加Key值 (+1)
        /// </summary>
        /// <remarks>(不須處理ThreadSafe問題)</remarks>
        protected abstract KeyType IncraseIdentity(KeyType oriKey);

        #endregion
    }
}
