using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartEngine.Core
{
    public partial class Log
    {
        public static class Handlers
        {
            public static event LogHandledDelegate ErrorHandler;
            public static event LogFatalHandledDelegate FatalHandler;
            public static event LogDelegate InfoHandler;
            public static event LogHandledDelegate WarningHandler;

            internal static void HandleError(string text, ref bool handled)
            {
                if (ErrorHandler != null)
                {
                    ErrorHandler(text, ref handled);
                }
            }

            internal static void HandleInfo(string text)
            {
                if (InfoHandler != null)
                {
                    InfoHandler(text);
                }
            }

            internal static void HandleWarning(string text, ref bool handled)
            {
                if (WarningHandler != null)
                {
                    WarningHandler(text, ref handled);
                }
            }

            internal static void HandleFatal(string text, string logFile, ref bool handled)
            {
                if (FatalHandler != null)
                {
                    FatalHandler(text, logFile, ref handled);
                }
            }

            // Nested Types
            public delegate void LogDelegate(string text);

            public delegate void LogFatalHandledDelegate(string text, string createdLogFilePath, ref bool handled);

            public delegate void LogHandledDelegate(string text, ref bool handled);
        }

    }
}
