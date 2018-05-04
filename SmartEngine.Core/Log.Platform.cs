using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartEngine.Core
{
    internal abstract class LogPlatform
    {
        private static LogPlatform currentPlatform;

        public static LogPlatform GetLogPlatform()
        {
            if (currentPlatform == null)
            {
                if (PlatformHelper.Platform == PlatformHelper.Platforms.MacOSX)
                {
                    currentPlatform = new MacLogPlatform();
                }
                else
                {
                    currentPlatform = new WindowsLogPlatform();
                }
            }
            return currentPlatform;
        }

        public abstract void ShowMessageBox(string text, string caption);

    }
}
