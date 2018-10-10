// Copyright (c) Aura development team - Licensed under GNU GPL
// For more information, see licence.txt in the main folder
using MySql.Data.MySqlClient;
using System;

namespace LocalCommons.Database
{
    public class ArcheageDb
    {
        private static string _connectionString;

        /// <summary>
        /// Sets connection string and calls TestConnection.
        /// </summary>
        /// <param name="host"></param>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <param name="db"></param>
        /// <exception cref="Exception">Thrown if connection couldn't be established.</exception>
        public static void Init(string host, string user, string pass, string db)
        {
            _connectionString = string.Format("server={0}; database={1}; uid={2}; password={3}; charset=utf8; pooling=true; min pool size=0; max pool size=100;", host, db, user, pass);
            TestConnection();
        }

        /// <summary>
        /// Returns a valid connection.
        /// </summary>
        protected static MySqlConnection GetConnection()
        {
            if (_connectionString == null)
            {
                throw new Exception("ArcheageDb has not been initialized.");
            }

            var result = new MySqlConnection(_connectionString);
            result.Open();
            return result;
        }

        /// <summary>
        /// Tests connection.
        /// </summary>
        /// <exception cref="Exception">Thrown if connection couldn't be established.</exception>
        public static void TestConnection()
        {
            MySqlConnection conn = null;
            try
            {
                conn = GetConnection();
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
    }
}
