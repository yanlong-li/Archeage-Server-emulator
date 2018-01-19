using System;

namespace AALauncher
{
    [Serializable]
    public class SettingsKeeper
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string LoginServerIp { get; set; }
        public string GamePath { get; set; }

        public string Cookie { get; set; }

        public SettingsKeeper()
        {
            LoginServerIp = "127.0.0.1:1237";
        }
    }
}