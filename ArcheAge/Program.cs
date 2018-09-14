using ArcheAge.ArcheAge.Network;
using ArcheAge.ArcheAge.Network.Connections;
using ArcheAge.Properties;
using LocalCommons.Logging;
using LocalCommons.Network;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using LocalCommons.UID;
using ArcheAge.ArcheAge.Holders;

namespace ArcheAge
{
    /// <summary>
    /// Main Application Enter Point.
    /// </summary>
    class Program
    {
        static string ServerClientVersion = "3";
        public static UInt32UidFactory CharcterUid; //UID для персонажа
        public static UInt32UidFactory AccountUid; //UID для аккаунта
        public static UInt32UidFactory ObjectUid; //UID для вещей

        static void Main(string[] args)
        {
            AccountUid = new UInt32UidFactory(AccountHolder.MaxAccountUid());      //генерим UID для аккаунтов
            CharcterUid = new UInt32UidFactory(CharacterHolder.MaxCharacterUid()); //генерим UID для персонажей
            //ObjectUid = new UInt32UidFactory(CharacterHolder.MaxObjectUid()); //генерим UID для вещей
            ObjectUid = new UInt32UidFactory(); //TODO: тест, пока начинаем с нуля

            Console.Title = "ARCHEAGE GAME SERVER";
            Console.CancelKeyPress += Console_CancelKeyPress;
            Stopwatch watch = Stopwatch.StartNew();
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            selectVersion();
            LoadExecutingAssembly(args);
            watch.Stop();
            Logger.Trace("ArcheAge Game Server started in {0} seconds", (watch.ElapsedMilliseconds / 1000).ToString("0.00"));
            watch = null;
            Key_Pressed();
        }
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
            if (info == null) return;
            Key_Pressed();
        }

        static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            if (e.SpecialKey != ConsoleSpecialKey.ControlC) return;
            Shutdown();
        }

        static void Shutdown()
        {
            //HERE SHUTDOWN.
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Logger.Trace("Unhandled exceptions: {0} , Exception - \n{1}", sender.GetType().Name, ((Exception)e.ExceptionObject).ToString());
            Console.WriteLine();
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
            Environment.Exit(0);
        }

        static void LoadExecutingAssembly(string[] args)
        {
            //----- Initialize Commons --------------------------------
            Logger.Init(); //Load Logger
            LocalCommons.Main.InitializeStruct(args); //Initializing LocalCommons.dll

            //------ Binary ------------------------------------------
            //Logger.Section("Binary data");

            //------ Network ------------------------------------------
            Logger.Section("Network");
            DelegateList.Initialize(Program.ServerClientVersion);
            InstallLoginServer();
            new AsyncListener(Settings.Default.ArcheAge_IP, Settings.Default.ArcheAge_Port, typeof(ClientConnection)); //Waiting For ArcheAge Connections
        }

        static void InstallLoginServer()
        {
            IPEndPoint point = new IPEndPoint(IPAddress.Parse(Settings.Default.LoginServer_IP), Settings.Default.LoginServer_Port);
            Socket con = new Socket(point.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            try { 
                con.Connect(point);
            }
            catch (Exception exp)
            {
                //throw exp;
                Logger.Trace("Unable to connect to login server, retry after 1 second");
            }
            if (con.Connected)
                new LoginConnection(con);
            else
                InstallLoginServer();
        }
    }
}
