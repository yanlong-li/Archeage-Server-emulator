using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace SmartEngine.Core
{
    internal class MacLogPlatform : LogPlatform
    {
        public override void ShowMessageBox(string text, string caption)
        {
            throw new Exception("Log: MacOSXPlatformFunctionality");
        }

    }

}
