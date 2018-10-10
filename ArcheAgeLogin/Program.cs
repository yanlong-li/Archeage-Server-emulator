using ArcheAgeLogin.ArcheAge;
using ArcheAgeLogin.ArcheAge.Holders;
using ArcheAgeLogin.ArcheAge.Network;
using ArcheAgeLogin.Properties;
using LocalCommons.Logging;
using LocalCommons.Network;
using LocalCommons.Database;
using MySql.Data.MySqlClient;
using LocalCommons.UID;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using ArcheAgeLogin.ArcheAge.Database;

namespace ArcheAgeLogin
{

    /// <summary>
    /// Main Class For Program Entering.
    /// </summary>
    class Program
    {
        public static UInt32UidFactory CharcterUid; //UID для персонажа
        public static UInt32UidFactory AccountUid; //UID для аккаунта

        static string ServerClientVersion = "3";
        // .method private hidebysig static void Main(string[] args) cil managed
        static void Main(string[] args)
        {
            AccountUid = new UInt32UidFactory(AccountHolder.MaxAccountUid());      //генерим UID для аккаунтов
            CharcterUid = new UInt32UidFactory(CharacterHolder.MaxCharacterUid()); //генерим UID для персонажей

            Console.Title = "ARCHEAGE LOGIN SERVER";
            Console.CancelKeyPress += Console_CancelKeyPress;
            Stopwatch watch = Stopwatch.StartNew();
            watch.Start();
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            selectVersion();
            LoadExecutingAssembly(args);
            watch.Stop();
            Logger.Trace("ArcheAge Login Server started in {0} seconds", (watch.ElapsedMilliseconds / 1000.0).ToString("0.00"));
            watch = null;
            Key_Pressed();

        }

        /// <summary>
        /// Login server's database.
        /// </summary>
        public static LoginDb Database { get; private set; }

        static void selectVersion()
        {
            Console.WriteLine("Select Client Version: Default 3");
            Console.WriteLine("1:   1.0");
            Console.WriteLine("3:   3.0");
            Console.WriteLine("4:   4.0");
            //0 is manually selected
            if (Settings.Default.ServerClientVersion == "0")
            {
                Program.ServerClientVersion = Console.ReadLine();
                if (Program.ServerClientVersion == "")
                {
                    //The default is 3
                    Program.ServerClientVersion = "3";
                }
            }
            else
            {
                Console.WriteLine("AutoSelectServerClientVersion:" + Settings.Default.ServerClientVersion);
                Program.ServerClientVersion = Settings.Default.ServerClientVersion;
            }
        }
        static void Key_Pressed()
        {
            ConsoleKeyInfo info = Console.ReadKey();
            if (info == null)
            {
                return;
            }

            Key_Pressed();
        }

        static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            if (e.SpecialKey != ConsoleSpecialKey.ControlC)
            {
                return;
            }

            Shutdown();
        }

        public static void Shutdown()
        {
            //TODO : Here Shutdowning.
        }

        /// <summary>
        /// Calls When Program Catches a Exception That Wasn't Catched By Try-Catch Block. (Unhandled)
        /// </summary>
        /// <param name="sender">Exception Sender - AppDomain</param>
        /// <param name="e">Event Arguments</param>
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Logger.Trace("Unhandled Exception - Sender: {0} , Exception - \n{1}", sender.GetType().Name, ((Exception)e.ExceptionObject).ToString());
            Console.WriteLine();
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
            Shutdown();
        }

        //.method hidebysig static void LoadExecutngAssembly(string[] args) cil managed
        static void LoadExecutingAssembly(string[] args)
        {
            Logger.Init();

            //Logger.Trace("TODO: REMAKE ALL CONTAINSKEY [] - TO TRYGETVALUE");

            Settings m_Current = Settings.Default;

            //--------------- Init Commons ----------------------
            LocalCommons.Main.InitializeStruct(args);

            //------------- Controllers -------------------------
            Logger.Section("Controllers");
            GameServerController.LoadAvailableGameServers();

            //--------------- MySQL ---------------------------
            Logger.Section("MySQL");

            // Database
            ArcheageDb.Init(m_Current.DataBase_Host, m_Current.DataBase_User, m_Current.DataBase_Password, m_Current.DataBase_Name);

            Database = new LoginDb();

            // Check if there are any updates
            CheckDatabaseUpdates();

            AccountHolder.LoadAccountData();

            //----------------Network ---------------------------
            Logger.Section("Network");
            PacketList.Initialize(Program.ServerClientVersion);
            new AsyncListener(m_Current.Main_IP, m_Current.Game_Port, defined: typeof(GameConnection)); //Waiting For GameServer Connections
            new AsyncListener(m_Current.Main_IP, m_Current.ArcheAge_Port, defined: typeof(ArcheAgeConnection)); //Waiting For ArcheAge Connections
        }

        static private void CheckDatabaseUpdates()
        {
            Logger.Trace("Checking for updates...");

            var files = Directory.GetFiles("sql");
            foreach (var filePath in files.Where(file => Path.GetExtension(file).ToLower() == ".sql"))
                RunUpdate(Path.GetFileName(filePath));
        }

        private static void RunUpdate(string updateFile)
        {
            if (Database.CheckUpdate(updateFile))
                return;

            Logger.Trace("Update '{0}' found, executing...", updateFile);

            Database.RunUpdate(updateFile);
        }

    }
}
