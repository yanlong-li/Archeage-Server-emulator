using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

using SmartEngine.Network.Utils;

namespace SmartEngine.Network
{
    /// <summary>
    /// 控制台用LoggerOld
    /// </summary>
    public partial class LoggerOld
    {
        public static LoggerOld defaultLoggerOld;
        public static LoggerOld CurrentLoggerOld = defaultLoggerOld;
        private string path;
        private string filename;

        BitMask<LogContent> logLevel = new BitMask<LogContent>(new BitMask(31));

        public BitMask<LogContent> LogLevel { get { return logLevel; } }

        public enum LogContent
        {
            Info = 1,
            Warning = 2,
            Error = 4,
            SQL = 8,
            Debug = 16
        }
        public LoggerOld(string filename)
        {
            this.filename = filename;
            path = GetLogFile();
            if (!File.Exists(path))
            {
                System.IO.File.Create(path);
            }
        }

        void WriteLog(string p)
        {
            try
            {
                path = GetLogFile();
                FileStream file = new FileStream(path, FileMode.Append);
                StreamWriter sw = new StreamWriter(file);
                string final = GetDate() + "|" + p; 
                sw.WriteLine(final);
                sw.Close();
            }
            catch (Exception)
            {

            }

        }

        void WriteLog(string prefix, string p)
        {
            try
            {
                path = GetLogFile();
                p = string.Format("{0}->{1}", prefix, p);
                FileStream file = new FileStream(path, FileMode.Append);
                StreamWriter sw = new StreamWriter(file);
                string final = GetDate() + "|" + p; // Add character to make exploding string easier for reading specific log entry by ReadLog()
                sw.WriteLine(final);
                sw.Close();
            }
            catch (Exception)
            {

            }

        }

        public static void ShowInfo(Exception ex, LoggerOld log)
        {
            ShowInfo(ex.Message + "\r\n" + ex.StackTrace, log);
        }

        public static void ShowInfo(string ex)
        {
            ShowInfo(ex, null);
        }

        public static void ShowInfo(string ex, LoggerOld log)
        {
            if (!defaultLoggerOld.LogLevel.Test(LogContent.Info))
                return;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[Info]");
            Console.ResetColor();
            Console.WriteLine(ex);
            if (log != null)
            {
                log.WriteLog(ex);
            }
        }

        public static void ShowWarning(Exception ex)
        {
            if (!defaultLoggerOld.LogLevel.Test(LogContent.Warning))
                return;
            ShowWarning(ex, defaultLoggerOld);
        }

        public static void ShowWarning(string ex)
        {
            if (!defaultLoggerOld.LogLevel.Test(LogContent.Warning))
                return;
            ShowWarning(ex, defaultLoggerOld);
        }

        public static void ShowDebug(Exception ex, LoggerOld log)
        {
            if (!defaultLoggerOld.LogLevel.Test(LogContent.Warning))
                return;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("[Debug]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            Console.ResetColor();
            if (log != null)
                log.WriteLog("[Debug]" + ex.Message + "\r\n" + ex.StackTrace);
        }

        public static void ShowDebug(string ex, LoggerOld log)
        {
            if (!defaultLoggerOld.LogLevel.Test(LogContent.Debug))
                return;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("[Debug]");
            Console.ForegroundColor = ConsoleColor.White;
            StackTrace Stacktrace = new StackTrace(1, true);
            string txt = ex;
            foreach (StackFrame i in Stacktrace.GetFrames())
            {
                txt = txt + "\r\n      at " + i.GetMethod().ReflectedType.FullName + "." + i.GetMethod().Name + " " + i.GetFileName() + ":" + i.GetFileLineNumber();
            }
            txt = FilterSQL(txt);
            Console.WriteLine(txt);
            Console.ResetColor();
            if (log != null)
            {
                log.WriteLog("[Debug]" + txt);
            }
        }

        public static void ShowSQL(Exception ex, LoggerOld log)
        {
            if (!defaultLoggerOld.LogLevel.Test(LogContent.SQL))
                return;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("[SQL]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(ex.Message + "\r\n" + FilterSQL(ex.StackTrace));
            Console.ResetColor();
            if (log != null)
                log.WriteLog("[SQL]" + ex.Message + "\r\n" + FilterSQL(ex.StackTrace));
        }

        static string FilterSQL(string input)
        {
            string[] tmp = input.Split('\n');
            string tmp2 = "";
            foreach (string i in tmp)
            {
                if (!i.Contains(" MySql.") && !i.Contains(" System."))
                    tmp2 = tmp2 + i + "\n";
            }
            return tmp2;
        }

        public static void ShowSQL(String ex, LoggerOld log)
        {
            if (!defaultLoggerOld.LogLevel.Test(LogContent.SQL))
                return;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("[SQL]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(ex);
            Console.ResetColor();
            if (log != null)
                log.WriteLog("[SQL]" + ex);
        }

        public static void ShowWarning(Exception ex, LoggerOld log)
        {
            if (!defaultLoggerOld.LogLevel.Test(LogContent.Warning))
                return;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("[Warning]");
            Console.ResetColor();
            Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            if (log != null)
            {
                log.WriteLog("Warning:" + ex.ToString());
            }
        }

        public static void ShowWarning(string ex, LoggerOld log)
        {
            if (!defaultLoggerOld.LogLevel.Test(LogContent.Warning))
                return;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("[Warning]");
            Console.ResetColor();
            Console.WriteLine(ex);
            if (log != null)
            {
                log.WriteLog("Warning:" + ex);
            }
        }
        public static void ShowError(Exception ex, LoggerOld log)
        {
            try
            {
                if (!defaultLoggerOld.LogLevel.Test(LogContent.Error))
                    return;
                if (log == null) log = defaultLoggerOld;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("[Error]");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                Console.ResetColor();
                log.WriteLog("[Error]" + ex.Message + "\r\n" + ex.StackTrace);
            }
            catch { }
        }

        public static void ShowError(string ex, LoggerOld log)
        {
            try
            {
                if (!defaultLoggerOld.LogLevel.Test(LogContent.Error))
                    return;
                if (log == null) log = defaultLoggerOld;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("[Error]");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(ex);
                Console.ResetColor();
                log.WriteLog("[Error]" + ex);
            }
            catch { }
        }

        public static void ShowError(string ex)
        {
            if (!defaultLoggerOld.LogLevel.Test(LogContent.Error))
                return;
            ShowError(ex, defaultLoggerOld);
        }

        public static void ShowError(Exception ex)
        {
            if (!defaultLoggerOld.LogLevel.Test(LogContent.Error))
                return;
            ShowError(ex, defaultLoggerOld);
        }

        string GetLogFile()
        {
            // Read in from XML here if needed.
            if (!System.IO.Directory.Exists("Log"))
                System.IO.Directory.CreateDirectory("Log");

            return "Log/[" + string.Format("{0}-{1}-{2}", DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day) + "]" + filename;
        }

        string GetDate()
        {
            return DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();
        }

        public static void ProgressBarShow(uint progressPos, uint progressTotal, string label)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\r[Info]");
            Console.ResetColor();
            Console.Write(string.Format("{0} [", label));
            StringBuilder sb = new StringBuilder();
            //sb.AppendFormat("\r{0} [", label);
            uint barPos = progressPos * 30 / progressTotal + 1;
            for (uint p = 0; p < barPos; p++) sb.AppendFormat("#");
            for (uint p = barPos; p < 30; p++) sb.AppendFormat(" ");
            sb.AppendFormat("] {0}%\r", progressPos * 100 / progressTotal);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(sb.ToString());
            Console.ResetColor();
        }

        public static void ProgressBarHide(string label)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\r[Info]");
            Console.ResetColor();
            Console.Write(string.Format("{0}                                                                                            \r", label));
        }
    }
}
