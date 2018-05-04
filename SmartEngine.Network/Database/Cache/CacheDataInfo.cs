using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartEngine.Network.Database.Cache
{
    /// <summary>
    /// Cache保存數據的結構
    /// </summary>
    /// <typeparam name="KeyType">Key的類型</typeparam>
    /// <typeparam name="ValueType">數據的類型</typeparam>
    public class CacheDataInfo<KeyType, ValueType>
    {
        public CacheDataInfo(KeyType key, ValueType value)
        {
            _key = key;
            _value = value;
        }

        private DateTime lastModifyTime = DateTime.MinValue;
        /// <summary>
        ///  最後修改時間
        /// </summary>
        public DateTime LastModifyTime { get { return lastModifyTime; } set { lastModifyTime = value; } }

        private bool needToWrite = false;
        /// <summary>
        /// 需要寫回DB?
        /// </summary>
        public bool IsNeedToWrite { get { return needToWrite; } set { needToWrite = value; } }

        private KeyType _key;
        /// <summary>
        /// Key值(例如CharID、ItemID)
        /// </summary>
        public KeyType Key { get { return _key; } set { _key = value; } }

        private ValueType _value;
        /// <summary>
        /// Value值(例如ActorPC、Item)
        /// </summary>
        public ValueType Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                lastModifyTime = DateTime.Now;
                needToWrite = true;
            }
        }

        public enum ActionType
        {
            Create,
            Update,
            Delete
        }

        private ActionType _action = ActionType.Update;
        /// <summary>
        /// 行為
        /// </summary>
        public ActionType Action { get { return _action; } set { _action = value; } }

        /// <summary>
        /// 保存失敗的重試次數
        /// </summary>
        public int SaveRetryTimes = 0;

        /// <summary>
        /// 等待寫入的開始時間
        /// </summary>
        public DateTime WriteWaitTime = DateTime.MinValue;
    }
}
