using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace SmartEngine.Core
{
    internal class WindowsLogPlatform : LogPlatform
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int MessageBox(IntPtr hwnd, string text, string caption, int type);
        [DllImport("user32.dll")]
        private static extern int ShowCursor(int bShow);
        public override void ShowMessageBox(string text, string caption)
        {
            while (ShowCursor(1) < 0)
            {
            }
            MessageBox(IntPtr.Zero, text, caption, 48);
        }

    }

}
