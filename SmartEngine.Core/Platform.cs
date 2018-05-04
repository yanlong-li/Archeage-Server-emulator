using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartEngine.Core
{
    internal static class PlatformHelper
    {
        private static bool detected;
        private static Platforms currentPlatform;

        public static Platforms Platform
        {
            get
            {
                if (!detected)
                {
                    if (Environment.OSVersion.Platform == PlatformID.Unix)
                    {
                        currentPlatform = Platforms.MacOSX;
                    }
                    else
                    {
                        currentPlatform = Platforms.Windows;
                    }
                    detected = true;
                }
                return currentPlatform;

            }
        }

        public enum Platforms
        {
            Windows,
            MacOSX
        }

    }
}
