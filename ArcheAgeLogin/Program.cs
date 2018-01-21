using ArcheAgeLogin.ArcheAge;
using ArcheAgeLogin.ArcheAge.Holders;
using ArcheAgeLogin.ArcheAge.Network;
using ArcheAgeLogin.ArcheAge.Structuring;
using ArcheAgeLogin.Properties;
using LocalCommons.Native.Logging;
using LocalCommons.Native.Network;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace ArcheAgeLogin
{
    /// <summary>
    /// Main Class For Program Entering.
    /// </summary>
    class Program
    {
        // .method private hidebysig static void Main(string[] args) cil managed
        static void Main(string[] args)
        {
            Console.Title = "上古世纪登陆服务Login Server";
            Console.CancelKeyPress += Console_CancelKeyPress;
            Stopwatch watch = Stopwatch.StartNew();
            watch.Start();
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            LoadExecutingAssembly(args);
            watch.Stop();
            Logger.Trace("ArcheAge Login Server Started In {0} sec.", (watch.ElapsedMilliseconds / 1000.0).ToString("0.00"));
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
            else
            {
                return;
            }
        }

        static void Shutdown()
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
            Console.WriteLine("按任意键退出");
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
            LocalCommons.Native.Significant.Main.InitializeStruct(args);

            //------------- Controllers -------------------------
            Logger.Section("Controllers");
            GameServerController.LoadAvailableGameServers();

            //--------------- MySQL ---------------------------
            Logger.Section("MySQL");
            AccountHolder.LoadAccountData();

            //----------------Network ---------------------------
            Logger.Section("Network");
            PacketList.Initialize();
            new AsyncListener(m_Current.Main_IP, m_Current.Game_Port, defined: typeof(GameConnection)); //Waiting For Game Server Connections
            new AsyncListener(m_Current.Main_IP, m_Current.ArcheAge_Port, defined: typeof(ArcheAgeConnection)); //Waiting For ArcheAge Connections

        }
    }
}
