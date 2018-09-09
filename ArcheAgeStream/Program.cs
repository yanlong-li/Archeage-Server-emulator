using LocalCommons.Logging;
using LocalCommons.Network;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using ArcheAgeStream.ArcheAge;
using ArcheAgeStream.ArcheAge.Network;
using ArcheAgeStream.Properties;

namespace ArcheAgeStream
{
    /// <summary>
    /// Main Class For Program Entering.
    /// </summary>
    class Program
    {
        //static string ServerClientVersion = "3";
        // .method private hidebysig static void Main(string[] args) cil managed
        static void Main(string[] args)
        {
            Console.Title = "ARCHEAGE STREAM SERVER";
            Console.CancelKeyPress += Console_CancelKeyPress;
            Stopwatch watch = Stopwatch.StartNew();
            watch.Start();
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            LoadExecutingAssembly(args);
            watch.Stop();
            Logger.Trace("ArcheAge Stream Server started in {0} seconds", (watch.ElapsedMilliseconds / 1000.0).ToString("0.00"));
            watch = null;
            Key_Pressed();
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
            Settings m_Current = Settings.Default;

            //--------------- Init Commons ----------------------
            LocalCommons.Main.InitializeStruct(args);

            //------------- Controllers -------------------------
            //Logger.Section("Controllers");
            //GameServerController.LoadAvailableGameServers();

            //----------------Network ---------------------------
            Logger.Section("Network");
            PacketList.Initialize();
            new AsyncListener(m_Current.ArcheAgeStream_IP, m_Current.ArcheAgeStream_PORT, defined: typeof(StreamConnection)); //Waiting For ArcheAge Connections

        }
    }
}
