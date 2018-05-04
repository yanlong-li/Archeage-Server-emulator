using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading;
using SmartEngine.Core;

namespace SmartEngine.Network.Memory
{
    /// <summary>
    /// 缓存区管理器
    /// </summary>
    public class BufferManager : Singleton<BufferManager>
    {
        List<byte[]> bufferBlocks;
        ConcurrentQueue<BufferBlock> freeBlocks;
        int bufferSize, bufferCount, blockSize;
        AutoResetEvent waiter = new AutoResetEvent(false);
        /// <summary>
        /// 请求缓存区时最多等待时间，超过则自动扩充缓存区
        /// </summary>
        public int MaxWaitTime = 10;

        /// <summary>
        /// 缓存管理器总共拥有的缓存
        /// </summary>
        public int TotalAllocatedMemory
        {
            get
            {
                return bufferBlocks.Count * bufferSize;
            }
        }

        /// <summary>
        /// 当前剩余可用缓存
        /// </summary>
        public int FreeMemory
        {
            get
            {
                return freeBlocks.Count * blockSize;
            }
        }

        /// <summary>
        /// 初始化缓存管理器
        /// </summary>
        /// <param name="bufferSize">单块缓存区大小</param>
        /// <param name="bufferCount">缓存区数量</param>
        /// <param name="blockSize">缓存块大小</param>
        public void Init(int bufferSize, int bufferCount, int blockSize)
        {
            if (bufferBlocks == null)
            {
                this.bufferSize = bufferSize;
                this.bufferCount = bufferCount;
                this.blockSize = blockSize;
                bufferBlocks = new List<byte[]>();
                freeBlocks = new ConcurrentQueue<BufferBlock>();
                for (int i = 0; i < bufferCount; i++)
                {
                    ExtendBuffer();
                }
            }
            else
                throw new NotSupportedException("BufferManager cannot be initialized twice!");
        }

        void ExtendBuffer()
        {
            lock (bufferBlocks)
            {
                byte[] buffer = new byte[bufferSize];
                bufferBlocks.Add(buffer);
                int blocks = bufferSize / blockSize;
                for (int j = 0; j < blocks; j++)
                {
                    BufferBlock block = new BufferBlock();
                    block.StartIndex = j * blockSize;
                    block.MaxLength = blockSize;
                    block.Buffer = buffer;
                    freeBlocks.Enqueue(block);
                    waiter.Set();
                }                
            }
        }

        /// <summary>
        /// 请求一个缓存块
        /// </summary>
        /// <returns></returns>
        public BufferBlock RequestBufferBlock()
        {
            if (bufferBlocks == null)
                Init(0x800000, 4, 0x1000);
            BufferBlock block;
            while (!freeBlocks.TryDequeue(out block))
            {
                if (!waiter.WaitOne(MaxWaitTime))
                    ExtendBuffer();
            }
            if (block.inUse)
                Logger.ShowWarning("BufferBlock in use!");
            block.inUse = true;
            return block;
        }

        /// <summary>
        /// 释放缓存块
        /// </summary>
        /// <param name="block">已经使用完毕的缓存块</param>
        internal void FreeBufferBlock(BufferBlock block)
        {
            freeBlocks.Enqueue(block);
            waiter.Set();
        }
    }
}
