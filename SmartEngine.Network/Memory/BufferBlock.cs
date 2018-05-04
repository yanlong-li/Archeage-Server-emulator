using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartEngine.Network.Memory
{
    public class BufferBlock
    {
        public int StartIndex;
        public int UsedLength;
        public int MaxLength;
        public byte[] Buffer;
        public object UserToken;
        internal bool inUse;

        /// <summary>
        /// 释放当前缓存块
        /// </summary>
        public void Free()
        {
            lock (this)
            {
                if (inUse)
                {
                    inUse = false;
                    UserToken = null;
                    BufferManager.Instance.FreeBufferBlock(this);
                }
            }
        }
    }
}
