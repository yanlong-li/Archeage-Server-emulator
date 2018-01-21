using LocalCommons.Native.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace LocalCommons.Native.Significant
{
    /// <summary>
    /// Initializing Class
    /// Author: Raphail
    /// </summary>
    public class Main
    {
        public static readonly bool Is64Bit = Environment.Is64BitProcess;
        private static AutoResetEvent signal = new AutoResetEvent(true);

        public static void Set() { signal.Set(); }

        public static void InitializeStruct(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Logger.Section("info");
            Logger.Trace("修正By：Yanlongli  email:ahlyl94@gmail.com");
            Logger.Trace("website:www.yanlongli.com");
            Logger.Trace("本软体仅用于学习交流，不得用于任何公开活动使用");
            Version ver = Assembly.GetExecutingAssembly().GetName().Version;

            Logger.Trace("ArcheAge Emu - 版本号 {0}.{1}, Build {2}.{3}", ver.Major, ver.Minor, ver.Build, ver.Revision);
            //Logger.Trace("Main: Running On .NET Framework (C#) Version {0}.{1}.{2}", Environment.Version.Major, Environment.Version.Minor, Environment.Version.Build);
            //Logger.Trace("如果你想停止该服务请直接按下 Ctrl + C");
            int platform = (int)Environment.OSVersion.Platform;
            if (platform == 4 || platform == 128)
            {
                Logger.Trace("Main: Unix Platform Detected");
            }
            Console.ResetColor();
        }
    }
}
