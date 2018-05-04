using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartEngine.Network.Map.PathFinding
{
    public class PathNode
    {
        internal int G, H, F;
        /// <summary>
        /// 上一个节点
        /// </summary>
        public PathNode Previous;
        /// <summary>
        /// 坐标
        /// </summary>
        public int X, Y, Z;
    }
}
