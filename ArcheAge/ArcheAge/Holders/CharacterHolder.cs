using ArcheAge.ArcheAge.Structuring;
using ArcheAge.Properties;
using LocalCommons.Logging;
using LocalCommons.Utilities;
using LocalCommons.World;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ArcheAge.ArcheAge.Holders
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

        public static int GetCount()
        {
            return CharactersList.Count;
        }

        /// <summary>
        /// Gets Character By CharName With LINQ Or Return Null.
        /// </summary>
        /// <param name="charName"></param>
        /// <returns></returns>
        public static Character GetCharacter(string charName)
        {
            foreach (var acc in CharactersList)
            {
                if (acc.CharName == charName)
                {
                    return acc;
                }
            }
            return null;
        }

        /// <summary>
        /// Возвращает максимальный использованный ID
        /// </summary>
        /// <returns></returns>
        public static uint MaxCharacterUid()
        {
            uint uid = 0;
            using (MySqlConnection conn = new MySqlConnection(Settings.Default.DataBaseConnectionString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand("SELECT * FROM `character_records`", conn);
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Character character = new Character();
                        character.CharacterId = reader.GetUInt32("characterid");
                        if (uid < character.CharacterId)
                        {
                            uid = character.CharacterId;
                        }
                    }

                    command.Dispose();
                    reader.Close();
                    reader.Dispose();
                }
                catch (Exception ex)
                {
                    Logger.Trace("Error: characters {0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            return uid;
        }

        public static void DeleteCharacterData(uint characterId)
        {
            int serverid = Settings.Default.Game_Id;
            using (MySqlConnection conn = new MySqlConnection(Settings.Default.DataBaseConnectionString))
            {
                try
                {
                    conn.Open(); //Устанавливаем соединение с базой данных
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM `character_records` WHERE `characterid` = '" + characterId +
                                      "' AND `worldid` = '" + serverid + "'";
                    //выполняем sql запрос
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
                catch (Exception ex)
                {
                    Logger.Trace("MySql: Ошибка удаления данных персонажа charID {0}, {1}", characterId, ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Берем из базы значение body для персонажа
        /// </summary>
        /// <param name="chr">куда быдем записывать данные</param>
        /// <param name="charRace">раса персонажа</param>
        /// <param name="charGender">пол персонажа</param>
        /// <returns>возвращает значение body для персонажа</returns>
        public static void LoadCharacterBodyCoord(Character chr, byte charRace, byte charGender)
        {
            using (MySqlConnection conn = new MySqlConnection(Settings.Default.DataBaseConnectionString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand command =
                        new MySqlCommand(
                            "SELECT * FROM `charactermodel` WHERE `race` = '" + charRace + "' AND `gender` = '" + charGender + "'", conn);
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        chr.CharBody = reader.GetInt32("body");
                        chr.ModelRef = reader.GetInt32("model_id");
                        chr.X = reader.GetInt32("start_location_x");
                        chr.Y = reader.GetInt32("start_location_y");
                        chr.Z = reader.GetInt32("start_location_z");
                    }

                    command.Dispose();
                    reader.Close();
                    reader.Dispose();
                }
                catch (Exception ex)
                {
                    Logger.Trace("MySql: Ошибка чтения данных charactermodel {0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public static void LoadZoneFaction(Character chr, byte charRace, byte charGender)
        {
            using (MySqlConnection conn = new MySqlConnection(Settings.Default.DataBaseConnectionString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand command =
                        new MySqlCommand(
                            "SELECT * FROM `characters` WHERE `char_race_id` = '" + charRace + "' AND `char_gender_id` = '" + charGender + "'", conn);
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        chr.FactionId = reader.GetInt32("faction_id");
                        chr.StartingZoneId = reader.GetInt32("starting_zone_id");
                    }

                    command.Dispose();
                    reader.Close();
                    reader.Dispose();
                }
                catch (Exception ex)
                {
                    Logger.Trace("MySql: Ошибка чтения данных charactermodel {0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public static void LoadClothsData(Character chr, int itemSet)
        {
            using (MySqlConnection conn = new MySqlConnection(Settings.Default.DataBaseConnectionString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand command =
                        new MySqlCommand(
                            "SELECT * FROM `equip_pack_cloths` WHERE `id` = '" + itemSet + "'", conn);
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        chr.Head = reader.GetInt32("headgear_id");
                        chr.Chest = reader.GetInt32("shirt_id");
                        chr.Legs = reader.GetInt32("pants_id");
                        chr.Gloves = reader.GetInt32("glove_id");
                        chr.Feet = reader.GetInt32("shoes_id");
                    }

                    command.Dispose();
                    reader.Close();
                    reader.Dispose();
                }
                catch (Exception ex)
                {
                    Logger.Trace("MySql: Ошибка чтения данных equip_pack_cloths {0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public static void LoadWeaponsData(Character chr, int itemSet)
        {
            using (MySqlConnection conn = new MySqlConnection(Settings.Default.DataBaseConnectionString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand command =
                        new MySqlCommand(
                            "SELECT * FROM `equip_pack_weapons` WHERE `id` = '" + itemSet + "'", conn);
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        chr.Weapon = reader.GetInt32("mainhand_id");
                        chr.WeaponExtra = reader.GetInt32("offhand_id");
                        chr.WeaponRanged = reader.GetInt32("ranged_id");
                        chr.Instrument = reader.GetInt32("musical_id");
                    }

                    command.Dispose();
                    reader.Close();
                    reader.Dispose();
                }
                catch (Exception ex)
                {
                    Logger.Trace("MySql: Ошибка чтения данных equip_pack_weapons {0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public static void LoadEquipPacksData(Character chr, int ability_id)
        {
            using (MySqlConnection conn = new MySqlConnection(Settings.Default.DataBaseConnectionString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand command =
                        new MySqlCommand(
                            "SELECT * FROM `character_equip_packs` WHERE `ability_id` = '" + ability_id + "'", conn);
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        chr.NewbieClothPackId = reader.GetInt32("newbie_cloth_pack_id");
                        chr.NewbieWeaponPackId = reader.GetInt32("newbie_weapon_pack_id");
                    }

                    command.Dispose();
                    reader.Close();
                    reader.Dispose();
                }
                catch (Exception ex)
                {
                    Logger.Trace("MySql: Ошибка чтения данных character_equip_packs {0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Fully Load Character Data From Current MySql DataBase
        /// </summary>
        public static Character LoadCharacterData(uint accountId, uint chcracterId)
        {
            var character = new Character();
            var serverid = Settings.Default.Game_Id;
            using (var conn = new MySqlConnection(Settings.Default.DataBaseConnectionString))
            {
                try
                {
                    conn.Open();
                    using (var command = new MySqlCommand("SELECT * FROM `character_records` WHERE `accountid` = '" + accountId + "' AND `characterid` = '" + chcracterId + "'", conn))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            GetData(reader);
                            command.Dispose();
                            reader.Close();
                            reader.Dispose();
                        }
                    }
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
                        Logger.Trace("Error: Unknown error +  {0}", e);
                    }
                }
                finally
                {
                    conn.Close();
                    //Logger.Trace("Load to {0} characters", CharactersList.GetCount());
                }
            }

            return character;
        }

        /// <summary>
        /// Returns true if a character with the given name exists.
        /// </summary>
        /// <param name="charName"></param>
        /// <returns></returns>
        public static bool CharacterExists(string charName)
        {
            using (var conn = new MySqlConnection(Settings.Default.DataBaseConnectionString))
            {
                conn.Open();
                using (var mc = new MySqlCommand("SELECT `characterid` FROM `character_records` WHERE `charName` = @charName", conn))
                {
                    mc.Parameters.AddWithValue("@charName", charName);

                    using (var reader = mc.ExecuteReader())
                    {
                        return reader.HasRows;
                    }
                }
            }
        }

        /// <summary>
        /// Fully Load Characters Data From Current MySql DataBase.
        /// </summary>
        public static List<Character> LoadCharacterData(uint accountId)
        {
            m_DbCharacters = new List<Character>();
            var serverid = Settings.Default.Game_Id;
            using (var conn = new MySqlConnection(Settings.Default.DataBaseConnectionString))
            {
                try
                {
                    conn.Open();
                    var command =
                        new MySqlCommand(
                            "SELECT * FROM `character_records` WHERE `accountid` = '" + accountId + "' AND `worldid` = '" +
                            serverid + "'", conn);
                    var reader = command.ExecuteReader();
                    GetData(reader);
                    command.Dispose();
                    reader.Close();
                    reader.Dispose();
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
                        Logger.Trace("Error: Unknown error +  {0}", e);
                    }
                }
                finally
                {
                    conn.Close();
                    //Logger.Trace("Load to {0} characters", CharactersList.GetCount());
                }
            }
            return CharactersList;
        }

        private static void GetData(MySqlDataReader reader)
        {
            while (reader.Read())
            {
                var character = new Character();

                character.CharacterId = reader.GetUInt32("characterid");
                character.AccountId = reader.GetUInt32("accountid");
                character.CharGender = reader.GetByte("chargender");
                character.CharName = reader.GetString("charname");
                character.CharRace = reader.GetByte("charrace");
                character.Decor = reader.GetInt32("decor");
                character.Ext = reader.GetByte("ext");
                character.Eyebrow = reader.GetInt32("eyebrow");
                character.Guid = reader.GetString("guid");
                character.LeftPupil = reader.GetInt32("leftpupil");
                character.Level = reader.GetByte("level");
                character.Lip = reader.GetInt32("lip");
                character.Modifiers = reader.GetString("modifiers");
                character.MoveX = reader.GetFloat("movex");
                character.MoveY = reader.GetFloat("movey");
                character.RightPupil = reader.GetInt32("rightpupil");
                character.Rotate = reader.GetFloat("rotate");
                character.Scale = reader.GetFloat("scale");
                character.Type[0] = reader.GetInt32("type0");
                character.Type[1] = reader.GetInt32("type1");
                character.Type[2] = reader.GetInt32("type2");
                character.Type[3] = reader.GetInt32("type3");
                character.Type[4] = reader.GetInt32("type4");
                character.Type[5] = reader.GetInt32("type5");
                character.Type[6] = reader.GetInt32("type6");
                character.Type[7] = reader.GetInt32("type7");
                character.Type[8] = reader.GetInt32("type8");
                character.Type[9] = reader.GetInt32("type9");
                character.Type[10] = reader.GetInt32("type10");
                character.Type[11] = reader.GetInt32("type11");
                character.Type[12] = reader.GetInt32("type12");
                character.Type[13] = reader.GetInt32("type13");
                character.Type[14] = reader.GetInt32("type14");
                character.Type[15] = reader.GetInt32("type15");
                character.Type[16] = reader.GetInt32("type16");
                character.Type[17] = reader.GetInt32("type17");
                character.V = reader.GetInt64("v");
                character.Weight[0] = reader.GetFloat("Weight0");
                character.Weight[1] = reader.GetFloat("Weight1");
                character.Weight[2] = reader.GetFloat("Weight2");
                character.Weight[3] = reader.GetFloat("Weight3");
                character.Weight[4] = reader.GetFloat("Weight4");
                character.Weight[5] = reader.GetFloat("Weight5");
                character.Weight[6] = reader.GetFloat("Weight6");
                character.Weight[7] = reader.GetFloat("Weight7");
                character.Weight[8] = reader.GetFloat("Weight8");
                character.Weight[9] = reader.GetFloat("Weight9");
                character.Weight[10] = reader.GetFloat("Weight10");
                character.Weight[11] = reader.GetFloat("Weight11");
                character.Weight[12] = reader.GetFloat("Weight12");
                character.Weight[13] = reader.GetFloat("Weight13");
                character.Weight[14] = reader.GetFloat("Weight14");
                character.Weight[15] = reader.GetFloat("Weight15");
                character.Weight[16] = reader.GetFloat("Weight16");
                character.Weight[17] = reader.GetFloat("Weight17");
                character.WorldId = reader.GetByte("worldid");
                character.Ability[0] = reader.GetByte("ability0");
                character.Ability[1] = reader.GetByte("ability1");
                character.Ability[2] = reader.GetByte("ability2");
                //character.LiveObjectId = reader.GetUInt32("liveobjectid");
                character.MapId = reader.GetInt32("zone");

                short rotx = reader.GetInt16("rotx");
                short roty = reader.GetInt16("roty");
                short rotz = reader.GetInt16("rotz");
                character.Heading = new Direction(rotx, roty, rotz);

                float x = reader.GetFloat("x");
                float y = reader.GetFloat("y");
                float z = reader.GetFloat("z");
                character.Position = new Position(x, y, z);

                //character.Exp = reader.GetInt32("exp");
                //character.MaxExp = reader.GetInt32("maxexp");
                //character.TotalExp = reader.GetInt32("totalexp");
                //character.Hp = reader.GetInt32("hp");
                //character.MaxHp = reader.GetInt32("maxHp");
                //character.Sp = reader.GetInt32("sp");
                //character.MaxSp = reader.GetInt32("maxSp");
                //character.Stamina = reader.GetInt32("stamina");
                //character.MaxStamina = reader.GetInt32("maxStamina");
                //character.Str = reader.GetInt32("str");
                //character.Con = reader.GetInt32("con");
                //character.Int = reader.GetInt32("int");
                //character.Spr = reader.GetInt32("spr");
                //character.Dex = reader.GetInt32("dex");
                //
                m_DbCharacters.Add(character);
            }
        }

        /// <summary>
        /// Inserts Or Update Existing Character Into your current Login Server MySql DataBase.
        /// </summary>
        /// <param name="character">Your Character Which you want Insert(If Not Exist) Or Update(If Exist)</param>
        public static List<Character> InsertOrUpdate(Character character)
        {
            using (var conn = new MySqlConnection(Settings.Default.DataBaseConnectionString))
            {
                try
                {
                    conn.Open(); //Устанавливаем соединение с базой данных
                    var cmd = new MySqlCommand();
                    cmd.Connection = conn;
                    if (m_DbCharacters.Contains(character))
                    {
                        cmd.CommandText =
                            "UPDATE `character_records` SET `characterid` = @characterid, `accountid` = @accountid, `chargender` = @chargender, `charname` = @charname," +
                            " `charrace` = @charrace, `decor` = @decor, `ext` = @ext, `eyebrow` = @eyebrow, `guid` = @guid, `leftPupil` = @leftPupil, `level` = @level," +
                            " `lip` = @lip, `modifiers` = @modifiers, `movex` = @movex, `movey` = @movey, `rightpupil` = @rightpupil, `rotate` = @rotate," +
                            " `scale` = @scale, `type0` = @type0, `type1` = @type1, `type2` = @type2, `type3` = @type3, `type4` = @type4, `type5` = @type5," +
                            " `type6` = @type6, `type7` = @type7, `type8` = @type8, `type9` = @type9, `type10` = @type10, `type11` = @type11, `type12` = @type12," +
                            " `type13` = @type13, `type14` = @type14, `type15` = @type15, `type16` = @type16, `type17` = @type17, `v` = @v, `Weight0` = @Weight0," +
                            " `Weight1` = @Weight1, `Weight2` = @Weight2, `Weight3` = @Weight3, `Weight4` = @Weight4, `Weight5` = @Weight5, `Weight6` = @Weight6," +
                            " `Weight7` = @Weight7, `Weight8` = @Weight8, `Weight9` = @Weight9, `Weight10` = @Weight10, `Weight11` = @Weight11, `Weight12` = @Weight12," +
                            " `Weight13` = @Weight13, `Weight14` = @Weight14, `Weight15` = @Weight15, `Weight16` = @Weight16, `Weight17` = @Weight17," +
                            " `worldid` = @worldid, `ability0` = @ability0, `ability1` = @ability1, `ability2` = @ability2," + //" `liveobjectid` = @liveobjectid," +
                            " `zone` = @zone, `x` = @x, `y` = @y, `z` = @z, `rotx` = rotx, `roty` = roty, `rotz` = rotz " +
                            " WHERE `charname` = @charname";
                    }
                    else
                    {
                        cmd.CommandText =
                            @"INSERT INTO `character_records` (characterid, accountid, chargender, charname, charrace, decor, ext, eyebrow, guid, leftPupil, level, lip, modifiers," +
                            " movex, movey, rightpupil, rotate, scale, type0, type1, type2, type3, type4, type5, type6, type7, type8, type9, type10, type11, type12, type13," +
                            " type14, type15, type16, type17, v, Weight0, Weight1, Weight2, Weight3, Weight4, Weight5, Weight6, Weight7, Weight8, Weight9, Weight10," +
                            " Weight11, Weight12, Weight13, Weight14, Weight15, Weight16, Weight17, Worldid, ability0, ability1, ability2, zone," +
                            " x, y, z, rotx, roty, rotz) " + //, liveobjectid) " +

                            " VALUES (@characterid, @accountid, @chargender, @charname, @charrace, @decor, @ext, @eyebrow, @guid, @leftPupil, @level, @lip, @modifiers, @movex, @movey," +
                            " @rightpupil, @rotate, @scale, @type0, @type1, @type2, @type3, @type4, @type5, @type6, @type7, @type8, @type9, @type10, @type11, @type12," +
                            " @type13, @type14, @type15, @type16, @type17, @v, @Weight0, @Weight1, @Weight2, @Weight3, @Weight4, @Weight5, @Weight6, @Weight7, @Weight8," +
                            " @Weight9, @Weight10, @Weight11, @Weight12, @Weight13, @Weight14, @Weight15, @Weight16, @Weight17, @Worldid, @ability0, @ability1, @ability2," +
                            " @zone, @x, @y, @z, @rotx, @roty, @rotz)"; //", @liveobjectid)";
                    }

                    MySqlParameterCollection parameters = cmd.Parameters;

                    parameters.Add("@characterid", MySqlDbType.UInt32).Value = character.CharacterId;
                    parameters.Add("@accountid", MySqlDbType.UInt32).Value = character.AccountId;
                    parameters.Add("@chargender", MySqlDbType.Byte).Value = character.CharGender;
                    parameters.Add("@charname", MySqlDbType.String).Value = character.CharName;
                    parameters.Add("@charrace", MySqlDbType.Byte).Value = character.CharRace;
                    parameters.Add("@decor", MySqlDbType.Int32).Value = character.Decor;
                    parameters.Add("@ext", MySqlDbType.Int32).Value = character.Ext;
                    parameters.Add("@eyebrow", MySqlDbType.Int32).Value = character.Eyebrow;
                    parameters.Add("@guid", MySqlDbType.String).Value = character.Guid;
                    parameters.Add("@leftPupil", MySqlDbType.Int32).Value = character.LeftPupil;
                    parameters.Add("@level", MySqlDbType.Byte).Value = character.Level;
                    parameters.Add("@lip", MySqlDbType.Int32).Value = character.Lip;
                    parameters.Add("@modifiers", MySqlDbType.String).Value = character.Modifiers;
                    parameters.Add("@movex", MySqlDbType.Float).Value = character.MoveX;
                    parameters.Add("@movey", MySqlDbType.Float).Value = character.MoveY;
                    parameters.Add("@rightpupil", MySqlDbType.Int32).Value = character.RightPupil;
                    parameters.Add("@rotate", MySqlDbType.Float).Value = character.Rotate;
                    parameters.Add("@scale", MySqlDbType.Float).Value = character.Scale;
                    parameters.Add("@type0", MySqlDbType.Int32).Value = character.Type[0];
                    parameters.Add("@type1", MySqlDbType.Int32).Value = character.Type[1];
                    parameters.Add("@type2", MySqlDbType.Int32).Value = character.Type[2];
                    parameters.Add("@type3", MySqlDbType.Int32).Value = character.Type[3];
                    parameters.Add("@type4", MySqlDbType.Int32).Value = character.Type[4];
                    parameters.Add("@type5", MySqlDbType.Int32).Value = character.Type[5];
                    parameters.Add("@type6", MySqlDbType.Int32).Value = character.Type[6];
                    parameters.Add("@type7", MySqlDbType.Int32).Value = character.Type[7];
                    parameters.Add("@type8", MySqlDbType.Int32).Value = character.Type[8];
                    parameters.Add("@type9", MySqlDbType.Int32).Value = character.Type[9];
                    parameters.Add("@type10", MySqlDbType.Int32).Value = character.Type[10];
                    parameters.Add("@type11", MySqlDbType.Int32).Value = character.Type[11];
                    parameters.Add("@type12", MySqlDbType.Int32).Value = character.Type[12];
                    parameters.Add("@type13", MySqlDbType.Int32).Value = character.Type[13];
                    parameters.Add("@type14", MySqlDbType.Int32).Value = character.Type[14];
                    parameters.Add("@type15", MySqlDbType.Int32).Value = character.Type[15];
                    parameters.Add("@type16", MySqlDbType.Int32).Value = character.Type[16];
                    parameters.Add("@type17", MySqlDbType.Int32).Value = character.Type[17];
                    parameters.Add("@v", MySqlDbType.Int64).Value = character.V;
                    parameters.Add("@Weight0", MySqlDbType.Float).Value = character.Weight[0];
                    parameters.Add("@Weight1", MySqlDbType.Float).Value = character.Weight[1];
                    parameters.Add("@Weight2", MySqlDbType.Float).Value = character.Weight[2];
                    parameters.Add("@Weight3", MySqlDbType.Float).Value = character.Weight[3];
                    parameters.Add("@Weight4", MySqlDbType.Float).Value = character.Weight[4];
                    parameters.Add("@Weight5", MySqlDbType.Float).Value = character.Weight[5];
                    parameters.Add("@Weight6", MySqlDbType.Float).Value = character.Weight[6];
                    parameters.Add("@Weight7", MySqlDbType.Float).Value = character.Weight[7];
                    parameters.Add("@Weight8", MySqlDbType.Float).Value = character.Weight[8];
                    parameters.Add("@Weight9", MySqlDbType.Float).Value = character.Weight[9];
                    parameters.Add("@Weight10", MySqlDbType.Float).Value = character.Weight[10];
                    parameters.Add("@Weight11", MySqlDbType.Float).Value = character.Weight[11];
                    parameters.Add("@Weight12", MySqlDbType.Float).Value = character.Weight[12];
                    parameters.Add("@Weight13", MySqlDbType.Float).Value = character.Weight[13];
                    parameters.Add("@Weight14", MySqlDbType.Float).Value = character.Weight[14];
                    parameters.Add("@Weight15", MySqlDbType.Float).Value = character.Weight[15];
                    parameters.Add("@Weight16", MySqlDbType.Float).Value = character.Weight[16];
                    parameters.Add("@Weight17", MySqlDbType.Float).Value = character.Weight[17];
                    parameters.Add("@worldid", MySqlDbType.Byte).Value = Settings.Default.Game_Id; //character.WorldId;
                    parameters.Add("@ability0", MySqlDbType.Byte).Value = character.Ability[0];
                    parameters.Add("@ability1", MySqlDbType.Byte).Value = character.Ability[1];
                    parameters.Add("@ability2", MySqlDbType.Byte).Value = character.Ability[2];
                    parameters.Add("@zone", MySqlDbType.Int32).Value = character.MapId;
                    
                    parameters.Add("@x", MySqlDbType.Float).Value = character.Position.X;
                    parameters.Add("@y", MySqlDbType.Float).Value = character.Position.Y;
                    parameters.Add("@z", MySqlDbType.Float).Value = character.Position.Z;

                    parameters.Add("@rotx", MySqlDbType.Int32).Value = character.Heading.X;
                    parameters.Add("@roty", MySqlDbType.Int32).Value = character.Heading.Y;
                    parameters.Add("@rotz", MySqlDbType.Int32).Value = character.Heading.Z;

                    //parameters.Add("@liveobjectid", MySqlDbType.Int32).Value = character.LiveObjectId;
                    //parameters.Add("@x", MySqlDbType.Float).Value = character.Position.X;
                    //parameters.Add("@y", MySqlDbType.Float).Value = character.Position.Y;
                    //parameters.Add("@z", MySqlDbType.Float).Value = character.Position.Z;
                    //parameters.Add("@exp", MySqlDbType.Int32).Value = character.Exp;
                    //parameters.Add("@maxexp", MySqlDbType.Int32).Value = character.MaxExp;
                    //parameters.Add("@totalexp", MySqlDbType.Int32).Value = character.TotalExp;
                    //parameters.Add("@hp", MySqlDbType.Int32).Value = character.Hp;
                    //parameters.Add("@maxhp", MySqlDbType.Int32).Value = character.MaxHp;
                    //parameters.Add("@sp", MySqlDbType.Int32).Value = character.Sp;
                    //parameters.Add("@maxsp", MySqlDbType.Int32).Value = character.MaxSp;
                    //parameters.Add("@stamina", MySqlDbType.Int32).Value = character.Stamina;
                    //parameters.Add("@maxstamina", MySqlDbType.Int32).Value = character.MaxStamina;
                    //parameters.Add("@str", MySqlDbType.Int32).Value = character.Str;
                    //parameters.Add("@con", MySqlDbType.Int32).Value = character.Con;
                    //parameters.Add("@int", MySqlDbType.Int32).Value = character.Int;
                    //parameters.Add("@spr", MySqlDbType.Int32).Value = character.Spr;
                    //parameters.Add("@dex", MySqlDbType.Int32).Value = character.Dex;
                    //
                    m_DbCharacters.Add(character);

                    if (m_DbCharacters.Contains(character))
                    {
                        parameters.Add("@acharname", MySqlDbType.String).Value = character.CharName;
                    }

                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
                catch (Exception ex)
                {
                    Logger.Trace("Cannot InsertOrUpdate template for " + character.CharName + ": {0}",
                        ex); // ex.Message - написать так, если нужно только сообщение без указания строки в сурсах
                }
                finally
                {
                    conn.Close();
                }
            }
            return CharactersList;
        }

    }
}