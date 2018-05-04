using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.IO;
using System.ComponentModel;
using System.Diagnostics;

namespace SmartEngine.Core
{
    public partial class Log
    {
        private enum LogType
        {
            Info,
            Warning,
            Error
        }
    
        [StructLayout(LayoutKind.Sequential)]
        private struct CachedItem
        {
            public Log.LogType type;
            public string text;
            public CachedItem(Log.LogType type, string text)
            {
                this.type = type;
                this.text = text;
            }
        }

        // Fields
        private static string fileName;
        private static Thread thread;
        private static bool dirCreated;
        private static List<CachedItem> logCache = new List<CachedItem>();
        private static object lockObj = new object();
        
        // Events
        public static event AfterFatalDelegate AfterFatal;
        // Methods
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void _FlushCachedLog()
        {
            lock (lockObj)
            {
                while (logCache.Count != 0)
                {
                    CachedItem item = logCache[0];
                    logCache.RemoveAt(0);
                    switch (item.type)
                    {
                        case LogType.Info:
                            Handlers.HandleInfo(item.text);
                            break;

                        case LogType.Warning:
                            ShowWarning(item.text);
                            break;

                        case LogType.Error:
                            ShowError(item.text);
                            break;
                    }
                }
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void _Init(Thread mainThread, string dumpRealFileName)
        {
            thread = mainThread;
            fileName = dumpRealFileName;
            if (!string.IsNullOrEmpty(dumpRealFileName) && File.Exists(dumpRealFileName))
            {
                File.Delete(dumpRealFileName);
            }
            DumpToFile("Log started: " + DateTime.Now.ToString() + "\r\n");
        }

        private static string CreateFatalLogFile()
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                string str = DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss");
                string path = Path.GetDirectoryName(DumpRealFileName) + @"\FatalError_" + Path.GetFileNameWithoutExtension(DumpRealFileName) + "_" + str + Path.GetExtension(DumpRealFileName);
                if (!File.Exists(path))
                {
                    try
                    {
                        File.Copy(DumpRealFileName, path);
                        return path;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
            return null;
        }

        private static void ShowError(string text)
        {
            bool handled = false;
            Handlers.HandleError(text, ref handled);
            if (!handled)
            {
                LogPlatform.GetLogPlatform().ShowMessageBox(text, "Error");
            }
        }

        private static string GetStackTrace()
        {
            string str2 = "";
            try
            {
                string stackTrace = "";
                stackTrace = Environment.StackTrace;
                if (stackTrace == null)
                {
                    return "";
                }
                str2 = stackTrace;
            }
            catch
            {
            }
            return str2;
        }

        private static void ShowWarning(string text)
        {
            bool handled = false;
            Handlers.HandleWarning(text, ref handled);
            if (!handled)
            {
                LogPlatform.GetLogPlatform().ShowMessageBox(text, "Warning");
            }
        }

        public static void DumpToFile(string text)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                if (!dirCreated)
                {
                    try
                    {
                        string directoryName = Path.GetDirectoryName(fileName);
                        if (!string.IsNullOrEmpty(directoryName))
                        {
                            Directory.CreateDirectory(directoryName);
                        }
                    }
                    catch
                    {
                    }
                    dirCreated = true;
                }
                try
                {
                    using (StreamWriter writer = new StreamWriter(fileName, true))
                    {
                        writer.Write(text);
                    }
                }
                catch
                {
                }
            }
        }

        public static void Error(string text)
        {
            lock (lockObj)
            {
                DumpToFile("Error: " + text + "\r\n" + GetStackTrace());
                Thread currentThread = Thread.CurrentThread;
                if ((thread == null) || (currentThread == thread))
                {
                    ShowError(text);
                }
                else
                {
                    logCache.Add(new CachedItem(LogType.Error, text));
                }
            }
        }

        public static void Error(string format, object arg0)
        {
            Error(string.Format(format, arg0));
        }

        public static void Error(string format, params object[] args)
        {
            Error(string.Format(format, args));
        }

        public static void Error(string format, object arg0, object arg1)
        {
            Error(string.Format(format, arg0, arg1));
        }

        public static void Error(string format, object arg0, object arg1, object arg2)
        {
            Error(string.Format(format, arg0, arg1, arg2));
        }

        public static void Error(string format, object arg0, object arg1, object arg2, object arg3)
        {
            Error(string.Format(format, new object[] { arg0, arg1, arg2, arg3 }));
        }

        public static void Error(string format, object arg0, object arg1, object arg2, object arg3, object arg4)
        {
            Error(string.Format(format, new object[] { arg0, arg1, arg2, arg3, arg4 }));
        }

        public static void Fatal(string text)
        {
            lock (lockObj)
            {
                bool handled = false;
                DumpToFile("Fatal: " + text + "\r\n" + GetStackTrace());
                string logFile = Log.CreateFatalLogFile();
                if (logFile != null)
                {
                    text = "Fatal log file created: " + logFile + ".\t\n\t\n" + text;
                }
                Handlers.HandleFatal(text, logFile, ref handled);
                if (!handled)
                {
                    string str2 = text + "\r\n\r\n\r\n" + GetStackTrace();
                    LogPlatform.GetLogPlatform().ShowMessageBox(str2, "Fatal: Exception");
                    if (AfterFatal != null)
                    {
                        AfterFatal();
                    }
                    Process.GetCurrentProcess().Kill();
                }
            }
        }

        public static void Fatal(string format, params object[] args)
        {
            Fatal(string.Format(format, args));
        }

        public static void Fatal(string format, object arg0)
        {
            Fatal(string.Format(format, arg0));
        }

        public static void Fatal(string format, object arg0, object arg1)
        {
            Fatal(string.Format(format, arg0, arg1));
        }

        public static void Fatal(string format, object arg0, object arg1, object arg2)
        {
            Fatal(string.Format(format, arg0, arg1, arg2));
        }

        public static void Fatal(string format, object arg0, object arg1, object arg2, object arg3)
        {
            Fatal(string.Format(format, new object[] { arg0, arg1, arg2, arg3 }));
        }

        public static void Fatal(string format, object arg0, object arg1, object arg2, object arg3, object arg4)
        {
            Fatal(string.Format(format, new object[] { arg0, arg1, arg2, arg3, arg4 }));
        }

        public static void FatalAsException(string text)
        {
            lock (lockObj)
            {
                bool b = false;
                DumpToFile("Exception: " + text);
                string a = Log.CreateFatalLogFile();
                if (a != null)
                {
                    text = "Fatal log file created: " + a + ".\t\n\t\n" + text;
                }
                Handlers.HandleFatal(text, a, ref b);
                if (!b)
                {
                    LogPlatform.GetLogPlatform().ShowMessageBox(text, "Fatal: Exception");
                    if (AfterFatal != null)
                    {
                        AfterFatal();
                    }
                    Process.GetCurrentProcess().Kill();
                }
            }
        }

        public static void Info(string text)
        {
            lock (lockObj)
            {
                DumpToFile("Info: " + text + "\r\n");
                Thread currentThread = Thread.CurrentThread;
                if ((thread == null) || (currentThread == thread))
                {
                    Handlers.HandleInfo(text);
                }
                else
                {
                    logCache.Add(new CachedItem(LogType.Info, text));
                }
            }
        }

        public static void Info(string format, object arg0)
        {
            Info(string.Format(format, arg0));
        }

        public static void Info(string format, params object[] args)
        {
            Info(string.Format(format, args));
        }

        public static void Info(string format, object arg0, object arg1)
        {
            Info(string.Format(format, arg0, arg1));
        }

        public static void Info(string format, object arg0, object arg1, object arg2)
        {
            Info(string.Format(format, arg0, arg1, arg2));
        }

        public static void Info(string format, object arg0, object arg1, object arg2, object arg3)
        {
            Info(string.Format(format, new object[] { arg0, arg1, arg2, arg3 }));
        }

        public static void Info(string format, object arg0, object arg1, object arg2, object arg3, object arg4)
        {
            Info(string.Format(format, new object[] { arg0, arg1, arg2, arg3, arg4 }));
        }

        public static void InvisibleInfo(string text)
        {
            lock (lockObj)
            {
                DumpToFile("Info: " + text + "\r\n");
            }
        }

        public static void InvisibleInfo(string format, params object[] args)
        {
            InvisibleInfo(string.Format(format, args));
        }

        public static void InvisibleInfo(string format, object arg0)
        {
            InvisibleInfo(string.Format(format, arg0));
        }

        public static void InvisibleInfo(string format, object arg0, object arg1)
        {
            InvisibleInfo(string.Format(format, arg0, arg1));
        }

        public static void InvisibleInfo(string format, object arg0, object arg1, object arg2)
        {
            InvisibleInfo(string.Format(format, arg0, arg1, arg2));
        }

        public static void InvisibleInfo(string format, object arg0, object arg1, object arg2, object arg3)
        {
            InvisibleInfo(string.Format(format, new object[] { arg0, arg1, arg2, arg3 }));
        }

        public static void InvisibleInfo(string format, object arg0, object arg1, object arg2, object arg3, object arg4)
        {
            InvisibleInfo(string.Format(format, new object[] { arg0, arg1, arg2, arg3, arg4 }));
        }

        public static void Warning(string text)
        {
            lock (lockObj)
            {
                DumpToFile("Warning: " + text + "\r\n");
                Thread currentThread = Thread.CurrentThread;
                if ((thread == null) || (currentThread == thread))
                {
                    ShowWarning(text);
                }
                else
                {
                    logCache.Add(new CachedItem(LogType.Warning, text));
                }
            }
        }

        public static void Warning(string format, params object[] args)
        {
            Warning(string.Format(format, args));
        }

        public static void Warning(string format, object arg0)
        {
            Warning(string.Format(format, arg0));
        }

        public static void Warning(string format, object arg0, object arg1)
        {
            Warning(string.Format(format, arg0, arg1));
        }

        public static void Warning(string format, object arg0, object arg1, object arg2)
        {
            Warning(string.Format(format, arg0, arg1, arg2));
        }

        public static void Warning(string format, object arg0, object arg1, object arg2, object arg3)
        {
            Warning(string.Format(format, new object[] { arg0, arg1, arg2, arg3 }));
        }

        public static void Warning(string format, object arg0, object arg1, object arg2, object arg3, object arg4)
        {
            Warning(string.Format(format, new object[] { arg0, arg1, arg2, arg3, arg4 }));
        }

        // Properties
        public static string DumpRealFileName
        {
            get
            {
                return fileName;
            }
        }

        public delegate void AfterFatalDelegate();


    }
}
