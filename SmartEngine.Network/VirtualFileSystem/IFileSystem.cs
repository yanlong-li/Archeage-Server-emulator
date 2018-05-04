using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SmartEngine.Network.VirtualFileSystem
{
    public interface IFileSystem
    {
        bool Init(string path);

        Stream OpenFile(string path);

        bool Exists(string path);

        string[] SearchFile(string path, string pattern);

        string[] SearchFile(string path, string pattern, System.IO.SearchOption option);

        void Close();
    }
}
