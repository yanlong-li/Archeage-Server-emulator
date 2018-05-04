using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartEngine.Network.Database.Cache
{

    /// <summary>
    /// 異步結果回傳的接口
    /// </summary>
    /// <param name="success">成功的Key集合</param>
    /// <param name="fails">失敗的Key集合</param>
    public delegate void SaveResultCallbackHandler<KeyType, ValueType>(IEnumerable<KeyType> success = null, IEnumerable<KeyType> fails = null);


    /// <summary>
    /// Cache存取DB的界面
    /// </summary>
    /// <typeparam name="KeyType">Key的類型</typeparam>
    /// <typeparam name="ValueType">數據的型別</typeparam>
    public interface ICacheDBHandler<KeyType, ValueType>
    {
        /// <summary>
        /// 讀取數據
        /// </summary>
        /// <param name="key">ID</param>
        /// <returns>數據</returns>
        ValueType LoadData(KeyType key);

        /// <summary>
        /// 寫入數據
        /// </summary>
        /// <param name="data">數據</param>
        void BeginSaveData(ref List<ValueType> data);

        /// <summary>
        /// 寫入數據
        /// </summary>
        /// <param name="data">數據</param>
        /// <param name="success">成功的Key集合</param>
        /// <param name="fails">失敗的Key集合</param>
        void SaveData(ref List<ValueType> data,out IEnumerable<KeyType> success,out IEnumerable<KeyType> fails );

        /// <summary>
        /// 創建數據(異步)
        /// </summary>
        /// <param name="key">ID</param>
        /// <param name="data">數據</param>\
        /// <remarks>INSERT INTO `Table` (ID,...) VALUES (Key,....);</remarks>
        void BeginCreateData(KeyType key, ValueType data);

        /// <summary>
        /// 創建數據(同步)
        /// </summary>
        /// <param name="key">ID</param>
        /// <param name="data">數據</param>
        /// <returns>寫入結果</returns>
        /// <remarks>INSERT INTO `Table` (ID,...) VALUES (Key,....);</remarks>
        ICacheDBSaveResult CreateData(KeyType key, ValueType data);

        /// <summary>
        /// 刪除數據(異步)
        /// </summary>
        /// <param name="data">數據</param>
        void BeginDeleteData(ref List<ValueType> data);

        /// <summary>
        /// 刪除數據(同步)
        /// </summary>
        /// <param name="data">數據</param>
        /// <param name="success">成功的Key集合</param>
        /// <param name="fails">失敗的Key集合</param>
        void DeleteData(ref List<ValueType> data,out IEnumerable<KeyType> success ,out IEnumerable<KeyType> fails);

        /// <summary>
        /// 異步結果回傳
        /// </summary>
        event SaveResultCallbackHandler<KeyType, ValueType> SaveResultCallback;

        /// <summary>
        /// DB連線狀態
        /// </summary>
        bool IsConnected();

        /// <summary>
        /// 取得ID(PK)最大值
        /// </summary>
        /// <returns></returns>
        /// <typeparam name="T">数据的类型</typeparam>
        /// <remarks>SELECT `ID` FROM `Table` ORDER BY `ID` DESC LIMIT 1</remarks>
        KeyType GetMaxID<T>();

        /// <summary>
        /// DB指令運行方式
        /// </summary>
        ICacheDBExecuteType ExecuteType { get; }
    }

    /// <summary>
    /// 寫入的結果
    /// </summary>
    public enum ICacheDBSaveResult
    {
        OK,
        Fail
    }

    /// <summary>
    /// 指令運行方式
    /// </summary>
    public enum ICacheDBExecuteType
    {
        /// <summary>
        /// 同步
        /// </summary>
        Sync,
        /// <summary>
        /// 異步
        /// </summary>
        Async
    }
}
