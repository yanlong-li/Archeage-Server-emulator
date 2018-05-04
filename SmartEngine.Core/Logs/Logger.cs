using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using NLog;

namespace SmartEngine.Core
{
    /// <summary>
    /// Logger
    /// </summary>
    /// <remarks>封裝nlog</remarks>
    public class Logger
    {
        public static string DefaultLoggerName = "default";

        #region Logger

        public static NLog.Logger InitLogger(string LoggerName = "")
        {
            string loggerName = DefaultLoggerName;
            if(LoggerName != "")
            {
                loggerName = LoggerName; 
            }
            DefaultLogger =LogManager.GetLogger(loggerName);
            if (!LoggerIntern.Ready)
                LoggerIntern.Init();
            return DefaultLogger;
        }

        public static void InitDefaultLogger(string LoggerName = "")
        {
            if (!LoggerIntern.Ready)
                LoggerIntern.Init();
            DefaultLogger = InitLogger(LoggerName);
        }

        public static NLog.Logger DefaultLogger = null;

        #endregion

        public static void ShowTrace(string ex)
        {
            LoggerIntern.EnqueueMsg(Level.Debug, ex, DefaultLogger);
        }

        public static void ShowTrace(Exception ex)
        {
            LoggerIntern.EnqueueMsg(Level.Debug, ex.ToString(), DefaultLogger);            
        }

        public static void ShowInfo(string ex)
        {
            LoggerIntern.EnqueueMsg(Level.Info, ex, DefaultLogger);            
        }

        public static void ShowWarning(Exception ex)
        {
            LoggerIntern.EnqueueMsg(Level.Warn, ex.ToString(), DefaultLogger);            
        }

        public static void ShowWarning(string ex)
        {
            LoggerIntern.EnqueueMsg(Level.Warn, ex, DefaultLogger);            
        }

        public static void ShowError(string ex)
        {
            LoggerIntern.EnqueueMsg(Level.Error, ex, DefaultLogger);            
        }

        public static void ShowError(Exception ex)
        {
            LoggerIntern.EnqueueMsg(Level.Error, ex.ToString(), DefaultLogger);
        }

        public static void ShowDebug(Exception ex)
        {
            LoggerIntern.EnqueueMsg(Level.Debug, ex.ToString(), DefaultLogger);
        }

        public static void ShowDebug(string ex)
        {
            StackTrace Stacktrace = new StackTrace(1, true);
            string txt = ex;
            foreach (StackFrame i in Stacktrace.GetFrames())
            {
                txt = txt + "\r\n      at " + i.GetMethod().ReflectedType.FullName + "." + i.GetMethod().Name + " " + i.GetFileName() + ":" + i.GetFileLineNumber();
            }
            txt = FilterSQL(txt);
            LoggerIntern.EnqueueMsg(Level.Debug, txt, DefaultLogger);            
        
        }

        public static void ShowSQL(Exception ex)
        {
            LoggerIntern.EnqueueMsg(Level.SQL, ex.ToString(), DefaultLogger);  
        }

        public static void ShowSQL(string ex)
        {
            LoggerIntern.EnqueueMsg(Level.SQL, ex, DefaultLogger);                    
        }

        /*public static void ShowTrace(string ex ,NLog.Logger logger)
        {
            logger.Trace(ex);
        }

        public static void ShowTrace(Exception ex, NLog.Logger logger)
        {
            logger.Trace(ex);
        }

        public static void ShowInfo(string ex, NLog.Logger logger)
        {
            logger.Info(ex);
        }

        public static void ShowWarning(Exception ex, NLog.Logger logger)
        {
            logger.Warn(ex.Message + "\r\n" + ex.StackTrace);
        }

        public static void ShowWarning(string ex, NLog.Logger logger)
        {
            logger.Warn(ex);
        }

        public static void ShowError(string ex, NLog.Logger logger)
        {
            logger.Error(ex);
        }

        public static void ShowError(Exception ex, NLog.Logger logger)
        {
            logger.Error(ex.Message + "\r\n" + ex.StackTrace);
        }

        public static void ShowDebug(Exception ex, NLog.Logger logger)
        {
            logger.Debug(ex.Message + "\r\n" + ex.StackTrace);
        }

        public static void ShowDebug(string ex, NLog.Logger logger)
        {
            StackTrace Stacktrace = new StackTrace(1, true);
            string txt = ex;
            foreach (StackFrame i in Stacktrace.GetFrames())
            {
                txt = txt + "\r\n      at " + i.GetMethod().ReflectedType.FullName + "." + i.GetMethod().Name + " " + i.GetFileName() + ":" + i.GetFileLineNumber();
            }
            txt = FilterSQL(txt);
            logger.Debug(txt);
        }

        public static void ShowSQL(Exception ex, NLog.Logger logger)
        {
            logger.Debug(ex.Message + "\r\n" + FilterSQL(ex.StackTrace));
        }

        public static void ShowSQL(string ex, NLog.Logger logger)
        {
            logger.Debug(ex);
        }*/

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
