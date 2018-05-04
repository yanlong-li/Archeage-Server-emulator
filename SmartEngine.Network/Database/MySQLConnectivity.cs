using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading;
using MySql;
using MySql.Data.MySqlClient;
using SmartEngine.Core;
using SmartEngine.Network.Utils;

namespace SmartEngine.Network.Database
{
    /// <summary>
    /// 带处理队列的Mysql数据库连接器
    /// </summary>
    public abstract class MySQLConnectivity
    {
        class MySQLCommand
        {
            public enum CommandType
            {
                NonQuery,
                Query,
                Scalar
            }

            public enum Results
            {
                PENDING,
                OK,
                FAILED,
            }

            MySqlCommand cmd;
            DataRowCollection reader;
            CommandType type;
            Results result = Results.PENDING;
            uint scalar = uint.MaxValue;
            int errorCount = 0;
            int maxRetry = 5;
            DateTime nextExecuteTime = DateTime.Now;
            internal AutoResetEvent waiter = new AutoResetEvent(false);

            public MySqlCommand Command { get { return this.cmd; } }

            public DataRowCollection DataRows { get { return this.reader; } set { this.reader = value; } }

            public CommandType Type { get { return this.type; } }

            public Results Result { get { return result; } set { this.result = value; } }

            public DateTime NextExecuteTime { get { return nextExecuteTime; } set { this.nextExecuteTime = value; } }

            public uint Scalar { get { return this.scalar; } set { this.scalar = value; } }

            public int MaxRetry { get { return maxRetry; } }

            public MySQLCommand(MySqlCommand cmd)
            {
                this.cmd = cmd;
                type = CommandType.NonQuery;
                maxRetry = 0;
            }

            public MySQLCommand(MySqlCommand cmd, CommandType type)
            {
                this.cmd = cmd;
                this.type = type;
                if (type == CommandType.NonQuery)
                    maxRetry = 0;
            }

            public int ErrorCount { get { return this.errorCount; } set { this.errorCount = value; } }
        }
        MySqlConnection db;
        MySqlConnection dbinactive;
        Thread mysqlPool;
        AutoResetEvent waiter = new AutoResetEvent(false);

        ConcurrentQueue<MySQLCommand> waitQueue = new ConcurrentQueue<MySQLCommand>();
        internal int cuurentCount = 0;
        private DateTime tick = DateTime.Now;
        private bool isconnected;
        private string host;
        private string port;
        private string database;
        private string dbuser;
        private string dbpass;
        bool shutingdown = false;
        
        /// <summary>
        /// 是否可以关闭Mysql连接（是否已处理完全部查询）
        /// </summary>
        public bool CanClose
        {
            get
            {
                return (waitQueue.Count == 0 && cuurentCount == 0);
            }
        }

        /// <summary>
        /// 每分钟数据库读写请求次数
        /// </summary>
        public int RequestsPerMinute { get; set; }

        /// <summary>
        /// 每分钟查询执行次数
        /// </summary>
        public int ExecutionPerMinute { get; set; }

        /// <summary>
        /// 等待队列长度
        /// </summary>
        public int QueueLength { get { return waitQueue.Count; } }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="host">Mysql服务器</param>
        /// <param name="port">端口</param>
        /// <param name="database">数据库名</param>
        /// <param name="user">用户名</param>
        /// <param name="pass">密码</param>
        public void Init(string host, int port, string database, string user, string pass)
        {
            this.host = host;
            this.port = port.ToString();
            this.dbuser = user;
            this.dbpass = pass;
            this.database = database;
            this.isconnected = false;
            try
            {
                db = new MySqlConnection(string.Format("Server={1};Port={2};Uid={3};Pwd={4};Database={0};Charset=utf8;", database, host, port, user, pass));
                dbinactive = new MySqlConnection(string.Format("Server={1};Port={2};Uid={3};Pwd={4};Database={0};Charset=utf8;", database, host, port, user, pass));
                db.Open();
            }
            catch (MySqlException ex)
            {
                Logger.ShowSQL(ex);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
            if (db != null)
            {
                if (db.State != ConnectionState.Closed)
                    this.isconnected = true;
                else
                {
                    Logger.ShowSQL("SQL Connection error");
                }
            }
            if (mysqlPool != null)
                mysqlPool.Abort();
            if (this.isconnected)
            {
                mysqlPool = new Thread(new ThreadStart(ProcessMysql));
                mysqlPool.Start();
            }
        }

        /// <summary>
        /// 开始连接Mysql服务器
        /// </summary>
        /// <returns></returns>
        public bool Connect()
        {
            if (!this.isconnected)
            {
                if (db.State == ConnectionState.Open) { this.isconnected = true; return true; }
                try
                {
                    db.Open();
                }
                catch (Exception) { }
                if (db != null)
                {
                    if (db.State != ConnectionState.Closed)
                        return true;
                    else
                        return false;
                }
            }
            return true;
        }

        bool CheckConnected()
        {
            if (this.isconnected)
            {
                TimeSpan newtime = DateTime.Now - tick;
                if (newtime.TotalMinutes > 5)
                {
                    db.Ping();
                    tick = DateTime.Now;
                }

                if (db.State == System.Data.ConnectionState.Broken || db.State == System.Data.ConnectionState.Closed)
                {
                    this.isconnected = false;
                }
            }
            return this.isconnected;
        }

        public bool Connected
        {
            get
            {
                return isconnected;
            }
        }

        /// <summary>
        /// 关闭Mysql连接
        /// </summary>
        public void Shutdown()
        {
            Logger.ShowSQL("Waiting for unprocessed sql queries...");
            while (!CanClose)
            {
                Thread.Sleep(100);
            }
            Logger.ShowSQL("Shutting down mysql connectivity...");
            shutingdown = true;
            mysqlPool.Abort();
        }

        DateTime exeStamp = DateTime.Now;
        DateTime reqStamp = DateTime.Now;
        int exeCounter = 0;
        int reqCounter = 0;

        void ProcessMysql()
        {
            while (true)
            {
                try
                {
                    while (!CheckConnected() && !shutingdown)
                    {
                        Logger.ShowSQL("Lost connection to Mysql Server...");
                        Logger.ShowSQL("Reconnecting...");
                        System.Threading.Thread.Sleep(1000);
                        this.Connect();
                    }

                    DateTime now = DateTime.Now;
                    MySQLCommand i;
                    List<MySQLCommand> pending = new List<MySQLCommand>();
                    while (waitQueue.TryDequeue(out i))
                    {
                        try
                        {
                            exeCounter++;
                            if ((DateTime.Now - exeStamp).TotalMinutes >= 1)
                            {
                                exeStamp = DateTime.Now;
                                if (ExecutionPerMinute > 0)
                                    ExecutionPerMinute = (ExecutionPerMinute + exeCounter) / 2;
                                else
                                    ExecutionPerMinute = exeCounter;
                                exeCounter = 0;
                            }
                            if (i.NextExecuteTime > now)
                            {
                                pending.Add(i);
                                continue;
                            }

                            i.Command.Connection = db;
                            switch (i.Type)
                            {
                                case MySQLCommand.CommandType.NonQuery:
                                    i.Command.Transaction = db.BeginTransaction();
                                    i.Command.ExecuteNonQuery();
                                    i.Command.Transaction.Commit();
                                    i.Result = MySQLCommand.Results.OK;
                                    break;
                                case MySQLCommand.CommandType.Query:
                                    MySqlDataAdapter adapter = new MySqlDataAdapter(i.Command);
                                    DataSet set = new DataSet();
                                    adapter.Fill(set);
                                    i.DataRows = set.Tables[0].Rows;
                                    i.Result = MySQLCommand.Results.OK;
                                    break;
                                case MySQLCommand.CommandType.Scalar:
                                    i.Scalar = Convert.ToUInt32(i.Command.ExecuteScalar());
                                    i.Result = MySQLCommand.Results.OK;
                                    break;
                            }
                            i.waiter.Set();
                        }
                        catch (Exception ex)
                        {
                            Logger.ShowSQL("Error on query:" + command2String(i.Command));
                            Logger.ShowSQL(ex);
                            i.ErrorCount++;
                            if (i.Command.Transaction != null)
                            {
                                try
                                {
                                    i.Command.Transaction.Rollback();
                                }
                                catch
                                {
                                }
                            }
                            i.NextExecuteTime = now.AddSeconds(5);
                            if (i.ErrorCount > i.MaxRetry)
                            {
                                Logger.ShowSQL("Error too many times, dropping command");
                                i.Result = MySQLCommand.Results.FAILED;
                                i.waiter.Set();
                            }
                            else
                                pending.Add(i);
                        }
                    }
                    foreach (MySQLCommand j in pending)
                        waitQueue.Enqueue(j);
                    cuurentCount = waitQueue.Count;
                    if (waitQueue.Count == 0)
                        waiter.WaitOne(100);
                    //Thread.Sleep(10);
                }
                catch (System.Threading.ThreadAbortException)
                {
                }
                catch(Exception ex)
                {
                    Logger.ShowError(ex);
                }
            }
        }

        public bool SQLExecuteNonQuery(string sqlstr)
        {
            if (string.IsNullOrEmpty(sqlstr))
                return true;
            else
                return SQLExecuteNonQuery(new MySqlCommand(sqlstr));
        }

        string command2String(MySqlCommand cmd)
        {
            string output;
            output = cmd.CommandText;
            if (cmd.Parameters.Count > 0)
            {
                string para = "";
                foreach (MySqlParameter i in cmd.Parameters)
                {
                    para += string.Format("{0}={1},", i.ParameterName, value2String(i.Value));
                }
                para = para.Substring(0, para.Length - 1);
                output = string.Format("{0} VALUES({1})", output, para);
            }
            return output;
        }

        string value2String(object val)
        {
            if (val.GetType() == typeof(byte[]))
            {
                byte[] tmp = (byte[])val;
                return "0x" + Conversions.bytes2HexString(tmp);
            }
            return val.ToString();
        }

        void IncreaseRequestCounter()
        {
            reqCounter++;
            if ((DateTime.Now - reqStamp).TotalMinutes >= 1)
            {
                reqStamp = DateTime.Now;
                if (RequestsPerMinute > 0)
                    RequestsPerMinute = (RequestsPerMinute + reqCounter) / 2;
                else
                    RequestsPerMinute = reqCounter;
                reqCounter = 0;
            }
        }

        public bool SQLExecuteNonQuery(MySqlCommand cmd_)
        {
            bool criticalarea = ClientManager.Blocked;
            bool result = false;
            if (criticalarea)
                ClientManager.LeaveCriticalArea();
            try
            {
                MySQLCommand cmd = new MySQLCommand(cmd_);
                waitQueue.Enqueue(cmd);
                waiter.Set();
                IncreaseRequestCounter();

                cmd.waiter.WaitOne();
                if (criticalarea)
                    ClientManager.EnterCriticalArea();
                if (cmd.Result == MySQLCommand.Results.OK)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                Logger.ShowSQL("Error on query:" + command2String(cmd_));
                Logger.ShowSQL(ex);
                if (criticalarea)
                    ClientManager.EnterCriticalArea();
                result = false;
            }
            return result;
        }

        public bool SQLExecuteScalar(string sqlstr, out uint index)
        {
            bool criticalarea = ClientManager.Blocked;
            bool result = true;
            if (criticalarea)
                ClientManager.LeaveCriticalArea();
            try
            {
                if (sqlstr.Substring(sqlstr.Length - 1) != ";") sqlstr += ";";
                sqlstr += "SELECT LAST_INSERT_ID();";
                MySQLCommand cmd = new MySQLCommand(new MySqlCommand(sqlstr), MySQLCommand.CommandType.Scalar);
                waitQueue.Enqueue(cmd);
                waiter.Set();
                IncreaseRequestCounter();

                cmd.waiter.WaitOne();
                if (cmd.Result == MySQLCommand.Results.OK)
                {
                    index = cmd.Scalar;
                    result = true;
                }
                else
                {
                    index = uint.MaxValue;
                    result = false;
                }
            }
            catch (Exception ex)
            {
                Logger.ShowSQL(ex);
                result = false;
                index = 0;
            }
            if (criticalarea)
                ClientManager.EnterCriticalArea();
            return result;
        }

        public bool SQLExecuteQuery(string sqlstr, out DataRowCollection result)
        {
            bool criticalarea = ClientManager.Blocked;
            if (criticalarea)
                ClientManager.LeaveCriticalArea();
            try
            {
                MySQLCommand cmd = new MySQLCommand(new MySqlCommand(sqlstr), MySQLCommand.CommandType.Query);
                waitQueue.Enqueue(cmd);
                waiter.Set();
                IncreaseRequestCounter();

                cmd.waiter.WaitOne();
                if (criticalarea)
                    ClientManager.EnterCriticalArea();
                if (cmd.Result == MySQLCommand.Results.OK)
                {
                    result = cmd.DataRows;
                    return true;
                }
                else
                {
                    result = null;
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.ShowSQL("Error on query:" + sqlstr);
                Logger.ShowSQL(ex);
                if (criticalarea)
                    ClientManager.EnterCriticalArea();
                result = null;
                return false;
            }

        }

        /// <summary>
        /// 将DateTime转换成Mysql安全的日期字符串
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>字符串</returns>
        public string ToSQLDateTime(DateTime date)
        {
            return string.Format("{0}-{1}-{2} {3}:{4}:{5}", date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second);
        }

        /// <summary>
        /// 检查字符串是否包含非法字符（防SQL注入），并将其修改为安全字符串
        /// </summary>
        /// <param name="str"></param>
        public void CheckSQLString(ref string str)
        {
            str = str.Replace("\\", "").Replace("'", "\\'");
        }

        /// <summary>
        /// 检查字符串是否包含非法字符（防SQL注入）
        /// </summary>
        /// <param name="str">需检查的字符</param>
        /// <returns>安全的字符串</returns>
        public string CheckSQLString(string str)
        {
            return str.Replace("\\", "").Replace("'", "\\'");
        }

    }
}
