using LocalCommons.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArcheAgeLogin.ArcheAge.Structuring;
using ArcheAgeLogin.Properties;

namespace ArcheAgeLogin.ArcheAge.Holders
{
    public class CharacterHolder
    {
        private static List<Character> m_DbCharacters;

        /// <summary>
        /// Loaded List of Characters.
        /// </summary>
        public static List<Character> CharactersList
        {
            get { return m_DbCharacters; }
        }

        /// <summary>
        /// Gets Character By CharName With LINQ Or Return Null.
        /// </summary>
        /// <param name="CharName"></param>
        /// <returns></returns>
        public static Character GetCharacter(string CharName)
        {
            foreach (var acc in m_DbCharacters)
            {
                if (acc.CharName == CharName) return acc;
            }

            return null;
        }

        /// <summary>
        /// Fully Load Characters Data From Current MySql DataBase.
        /// </summary>
        public static int LoadCharacterData(long accountId)
        {
            m_DbCharacters = new List<Character>();
            MySqlConnection con = new MySqlConnection(Settings.Default.DataBaseConnectionString);
            try
            {
                con.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM characters WHERE accountid = '" + accountId +"'", con);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Character character = new Character();
                    character.Id = reader.GetInt64("id");
                    character.AccountId = reader.GetInt64("accountid");
                    character.WorldId = reader.GetByte("worldid");
                    character.Type = reader.GetInt32("type0");
                    character.CharName = reader.GetString("charname");
                    character.CharRace = reader.GetByte("charrace");
                    character.CharGender = reader.GetByte("chargender");
                    character.GUID = reader.GetString("guid");
                    character.V = reader.GetInt64("v");
                    m_DbCharacters.Add(character);
                }
                command = null;
                reader = null;
            }
            catch (Exception e)
            {
                if (e.Message.IndexOf("using password: YES") >= 0)
                {
                    Logger.Trace("Error: Incorrect username or password");
                }
                else if (e.Message.IndexOf("Unable to connect to any of the specified MySQL hosts") >= 0)
                {
                    Logger.Trace("Error: Unable to connect to database");
                }
                else
                {
                    Logger.Trace("Error: Unknown error");
                }
                //Console.ReadKey();
                //Message = "Authentication to host '127.0.0.1' for user 'root' using method 'mysql_native_password' failed with message: Access denied for user 'root'@'localhost' (using password: YES)"
            }
            finally
            {
                con.Close();
                con = null;
            }
            Logger.Trace("Load to {0} characters", m_DbCharacters.Count);
            return m_DbCharacters.Count;
        }

        /// <summary>
        /// Fully Load Characters Data From Current MySql DataBase.
        /// </summary>
        public static void LoadCharacterData()
        {
            m_DbCharacters = new List<Character>();
            MySqlConnection con = new MySqlConnection(Settings.Default.DataBaseConnectionString);
            try
            {
                con.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM `characters`", con);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Character character = new Character();
                    character.Id = reader.GetInt64("id");
                    character.AccountId = reader.GetInt64("accountid");
                    character.WorldId = reader.GetByte("worldid");
                    character.Type = reader.GetInt32("type0");
                    character.CharName = reader.GetString("charname");
                    character.CharRace = reader.GetByte("charrace");
                    character.CharGender = reader.GetByte("chargender");
                    character.GUID = reader.GetString("guid");
                    character.V = reader.GetInt64("v");
                    m_DbCharacters.Add(character);
                }
                command = null;
                reader = null;
            }
            catch (Exception e)
            {
                if (e.Message.IndexOf("using password: YES") >= 0)
                {
                    Logger.Trace("Error: Incorrect username or password");
                }
                else if (e.Message.IndexOf("Unable to connect to any of the specified MySQL hosts") >= 0)
                {
                    Logger.Trace("Error: Unable to connect to database");
                }
                else
                {
                    Logger.Trace("Error: Unknown error");
                }
                //Console.ReadKey();
                //Message = "Authentication to host '127.0.0.1' for user 'root' using method 'mysql_native_password' failed with message: Access denied for user 'root'@'localhost' (using password: YES)"
            }
            finally
            {
                con.Close();
                con = null;
            }
            Logger.Trace("Load to {0} characters", m_DbCharacters.Count);
        }

        /// <summary>
        /// Inserts Or Update Existing Character Into your current Login Server MySql DataBase.
        /// </summary>
        /// <param name="character">Your Character Which you want Insert(If Not Exist) Or Update(If Exist)</param>
        public static void InsertOrUpdate(Character character)
        {
            MySqlConnection con = new MySqlConnection(Settings.Default.DataBaseConnectionString);
            try
            {
                con.Open();
                MySqlCommand command = null;
                if (m_DbCharacters.Contains(character))
                {
                    command = new MySqlCommand(
                        "UPDATE `characters` SET `id` = @id, `accountid` = @accountid, `worldid` = @worldid, `type` = @type, `charname` = @charname," +
                        " `charrace` = @charrace, `chargender` = @chargender, `guid` = @guid, `v` = @v" +
                        "WHERE `acharname` = @charname",
                        con);
                }
                else
                {
                    command = new MySqlCommand(
                        "INSERT INTO `characters`(id, accountid, worldid, type,  type, charname, charrace, chargender, guid, v)" +
                        "VALUES(@id, @accountid, @worldid, @type, @type, @charname, @charrace, @chargender, @guid, @v)",
                        con);
                }
                MySqlParameterCollection parameters = command.Parameters;
                parameters.Add("@id", MySqlDbType.Int64).Value = character.Id;
                parameters.Add("@accountid", MySqlDbType.Int64).Value = character.AccountId;
                parameters.Add("@worldid", MySqlDbType.Byte).Value = character.WorldId;
                parameters.Add("@type", MySqlDbType.Int32).Value = character.Type;
                parameters.Add("@charname", MySqlDbType.String).Value = character.CharName;
                parameters.Add("@charrace", MySqlDbType.Byte).Value = character.CharRace;
                parameters.Add("@chargender", MySqlDbType.Byte).Value = character.CharGender;
                parameters.Add("@token", MySqlDbType.String).Value = character.GUID;
                parameters.Add("@v", MySqlDbType.Int64).Value = character.V;

                if (m_DbCharacters.Contains(character))
                    parameters.Add("@acharname", MySqlDbType.Int64).Value = character.CharName;

                command.ExecuteNonQuery();
                command = null;
            }
            catch (Exception e)
            {
                Logger.Trace("Cannot InsertOrUpdate template for " + character.CharName + ": {0}", e);
            }
            finally
            {
                m_DbCharacters.Add(character);
                con.Close();
                con = null;
            }
        }
    }
}
