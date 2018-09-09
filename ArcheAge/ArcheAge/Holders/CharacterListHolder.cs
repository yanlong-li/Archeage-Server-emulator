using ArcheAge.ArcheAge.Network.Connections;
using ArcheAge.Properties;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcheAge.ArcheAge.Holders
{
    class CharacterListHolder
    {
        public static List<Characters> LoadChars(string accid)
        {
            List<Characters> chars = new List<Characters>();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(accid);
            Console.ResetColor();
            string connection = "server=" + Settings.Default.DataBase_Host + ";user=" + Settings.Default.DataBase_User + ";database=" + Settings.Default.DataBase_Name + ";port=" + Settings.Default.DataBase_Port + ";password=" + Settings.Default.DataBase_Password + ((!Settings.Default.SSL) ? "; SslMode = none" : "");
            int serverid = Settings.Default.Game_Id;
            MySqlConnection dbCon = new MySqlConnection(connection);
            try
            {
                dbCon.Open();
                //MessageBox.Show(":D");
                //string sql = "SELECT * FROM characters WHERE AccountID =" + accid + "";
                string sql = "SELECT * FROM characters WHERE AccountID = '" + accid + "' AND WorldID = '" + serverid + "'";
                MySqlCommand command = dbCon.CreateCommand();
                command.CommandText = sql;
                try
                {
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int cid = Int32.Parse(reader["ID"].ToString());
                        string cname = reader["CharName"].ToString();
                        int ctype = Int32.Parse(reader["Type0"].ToString());
                        int crace = Int32.Parse(reader["CharRace"].ToString());
                        int cgender = Int32.Parse(reader["CharGender"].ToString());
                        string cGUID = reader["GUID"].ToString();
                        chars.Add(new Characters(cid, cname, ctype, crace, cgender, cGUID));
                    }
                }
                catch (Exception ex) { }
                dbCon.Close();
            }
            catch (MySqlException ex)
            {
                string error = "";
                if (ex.Number == 0) { error = "[CODE : 0] Cannot connect to database please check the credentials or contact the administrator."; }
                if (ex.Number == 1042) { error = "[CODE : 1042] Cannot contact the server please check network connection or contact the administrator."; }
                Console.WriteLine("Database Error " + error);
            }
            return chars;
        }

        public static void ShowChars()
        {

        }

        public class Characters
        {
            public int id;
            public string name;
            public int type;
            public int race;
            public int gender;
            public string GUID;

            public Characters() { }

            public Characters(int id, string name, int type, int race, int gender, string GUID)
            {
                this.id = id;
                this.name = name;
                this.type = type;
                this.race = race;
                this.gender = gender;
                this.GUID = GUID;
                
            }
        }
    }
}
