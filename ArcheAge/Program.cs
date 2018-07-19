using ArcheAge.ArcheAge.Net;
using ArcheAge.ArcheAge.Net.Connections;
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

namespace ArcheAge
{
    /// <summary>
    /// Main Application Enter Point.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "ARCHEAGE GAME SERVER";
            Console.CancelKeyPress += Console_CancelKeyPress;
            Stopwatch watch = Stopwatch.StartNew();
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            LoadExecutingAssembly(args);
            watch.Stop();
            Logger.Trace("ArcheAge starts {0} seconds.", (watch.ElapsedMilliseconds / 1000).ToString("0.00"));
            watch = null;
            Key_Pressed();
        }

        static void Key_Pressed()
        {
            ConsoleKeyInfo info = Console.ReadKey();
            if (info != null)
            {
                Key_Pressed();
            }
        }

        static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            if (e.SpecialKey == ConsoleSpecialKey.ControlC)
            {
                Shutdown();
            }
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
            Logger.Section("network connection");
            DelegateList.Initialize();
            InstallLoginServer();
            new AsyncListener(Settings.Default.ArcheAge_IP, Settings.Default.ArcheAge_Port, typeof(ClientConnection)); //Waiting For ArcheAge Connections
        }

        //Original

        static void InstallLoginServer()
        {
            IPEndPoint point = new IPEndPoint(IPAddress.Parse(Settings.Default.LoginServer_IP), Settings.Default.LoginServer_Port);
            Socket con = new Socket(point.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            try { 
                con.Connect(point);
            }catch(Exception exp)
            {
                //throw exp;
                Logger.Trace("Unable to connect to login server, retry after 1 second");
            }
            if (con.Connected)
                new LoginConnection(con);
            else
                InstallLoginServer();
        }


        //Data used for testing
        //static void InstallLoginServer()
        //{
        //    IPEndPoint point = new IPEndPoint(IPAddress.Parse("101.226.100.108"), 1239);
        //    Socket con = new Socket(point.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        //    try
        //    {
        //        con.Connect(point);
        //    }
        //    catch (Exception exp)
        //    {
        //        //throw exp;
        //        Logger.Trace("Unable to connect to login server, retry after 1 second");
        //    }
        //    if (con.Connected)
        //        new LoginConnection(con);
        //    else
        //        InstallLoginServer();
        //}

    }
}
