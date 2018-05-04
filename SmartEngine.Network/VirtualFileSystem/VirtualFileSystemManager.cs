using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SmartEngine.Network.VirtualFileSystem.IFileSystemImp;

namespace SmartEngine.Network.VirtualFileSystem
{
    /// <summary>
    /// 文件系统类型
    /// </summary>
    public enum FileSystems
    {
        /// <summary>
        /// 真实文件系统
        /// </summary>
        Real,
        /// <summary>
        /// LPK压缩归档文件
        /// </summary>
        LPK,
        /// <summary>
        /// 使用引擎的虚拟文件系统
        /// </summary>
        Engine,
    }

    /// <summary>
    /// 虚拟文件系统管理器
    /// </summary>
    public class VirtualFileSystemManager : Singleton<VirtualFileSystemManager>
    {
        IFileSystem fs;
        public bool Init(FileSystems type, string path)
        {
            if (fs != null)
                fs.Close();
            switch (type)
            {
                case FileSystems.Real:
                    fs = new RealFileSystem();
                    break;
                case FileSystems.LPK:
                    fs = new LPKFileSystem();
                    break;
                case FileSystems.Engine:
                    //fs = new EngineFileSystem();
                    break;
            }
            return fs.Init(path);
        }

        public IFileSystem FileSystem
        {
            get
            {
                return fs;
            }
        }
    }
}
