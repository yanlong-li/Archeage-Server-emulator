using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using SmartEngine.Core.Math;
namespace SmartEngine.Network.Map.PathFinding
{
    /// <summary>
    /// 使用A*算法实现的寻路算法，需提供表示可移动范围的地理数据
    /// </summary>
    public class PathFinding
    {
        /// <summary>
        /// 最大尝试次数，用于早期中止无法找到的路径
        /// </summary>
        public static int MaxIteration = 1000;
        /// <summary>
        /// 地理数据，用于取得某坐标是否可通行
        /// </summary>
        public IGeoData GeoData { get; set; }

        /// <summary>
        /// 寻找路径
        /// </summary>
        /// <param name="x">起始X</param>
        /// <param name="y">起始Y</param>
        /// <param name="z">起始Z</param>
        /// <param name="x2">目标X</param>
        /// <param name="y2">目标Y</param>
        /// <param name="z2">目标Z</param>
        /// <returns>找到的路径</returns>
        public List<PathNode> FindPath(int x, int y, int z, int x2, int y2, int z2)
        {
            PathNode src = new PathNode();
            DateTime now = DateTime.Now;
            GeoData.NormalizeCoordinates(x, y, z, out x, out y, out z);
            GeoData.NormalizeCoordinates(x2, y2, z2, out x2, out y2, out z2);
            int count = 0;
            src.X = x;
            src.Y = y;
            src.Z = z;
            src.F = 0;
            src.G = 0;
            src.H = 0;
            int difX = x2 - x;
            int difY = y2 - y;
            int difZ = z2 - z;
            int dist = (int)Math.Sqrt(difX * difX + difY * difY + difZ * difZ) * 25;

            List<PathNode> path = new List<PathNode>();
            PathNode current = src;
            if (!GeoData.IsWalkable(x2, y2, z2, x2, y2, z2))
            {
                path.Add(current);
                return path;
            }
            if (x == x2 && y == y2 && z == z2)
            {
                path.Add(current);
                return path;
            }
            Dictionary<ulong, PathNode> openedNode = new Dictionary<ulong, PathNode>();
            GetNeighbor(src, x2, y2, z2, openedNode);
            while (openedNode.Count != 0)
            {
                PathNode shortest = new PathNode();
                shortest.F = int.MaxValue;
                if (count > MaxIteration)
                    break;
                foreach (PathNode i in openedNode.Values)
                {
                    if (i.X == x2 && i.Y == y2 && i.Z == z2)
                    {
                        openedNode.Clear();
                        shortest = i;
                        break;
                    }
                    if (i.F < shortest.F)
                        shortest = i;
                }
                current = shortest;
                if (openedNode.Count == 0 || current.F > dist)
                    break;
                ulong hash = CalcHash((uint)shortest.X, (uint)shortest.Y, (uint)shortest.Z);

                openedNode.Remove(hash);
                current = GetNeighbor(shortest, x2, y2, z2, openedNode);
                count++;
            }

            while (current.Previous != null)
            {
                path.Add(current);
                current = current.Previous;
            }
            List<PathNode> result = new List<PathNode>();

            for (int idx = path.Count - 1; idx >= 0; idx--)
            {
                PathNode i = path[idx];
                GeoData.RealCoordinatesFromNormalized(i.X, i.Y, i.Z, out x, out y, out z);
                i.X = x;
                i.Y = y;
                i.Z = z;
                result.Add(i);
            }
            return result;
        }

        ulong CalcHash(uint x, uint y, uint z)
        {
            ulong key = ((ulong)x << 32) | y;
            key = (~key) + (key << 18); // key = (key << 18) - key - 1;
            key = key ^ (key >> 31);
            key = key * 21; // key = (key + (key << 2)) + (key << 4);
            key = key ^ (key >> 11);
            key = key + (key << 6);
            key = key ^ (key >> 22);
            return ((ulong)((int)key) << 32) | z;
        }

        private PathNode GetNeighbor(PathNode node, int x, int y, int z, Dictionary<ulong, PathNode> openedNode)
        {
            PathNode res = node;
            for (int i = node.X - 1; i <= node.X + 1; i++)
            {
                for (int j = node.Y - 1; j <= node.Y + 1; j++)
                {
                    for (int k = node.Z - 1; k <= node.Z + 1; k++)
                    {
                        if (j == node.Y && i == node.X)
                            continue;
                        if (GeoData.IsWalkable(node.X, node.Y, node.Z, i, j, k))
                        {
                            ulong hash = CalcHash((uint)i, (uint)j, (uint)k);
                            PathNode tmp;
                            if (!openedNode.TryGetValue(hash, out tmp))
                            {
                                PathNode node2 = new PathNode();
                                node2.X = i;
                                node2.Y = j;
                                node2.Z = k;
                                node2.Previous = node;
                                int dif = 0;
                                if (i != node.X)
                                    dif++;
                                if (j != node.Y)
                                    dif++;
                                if (k != node.Z)
                                {
                                    dif++;
                                    node2.G += 10;
                                }
                                switch (dif)
                                {
                                    case 1:
                                        node2.G = node.G + 10;
                                        break;
                                    case 2:
                                        node2.G = node.G + 14;
                                        break;
                                    case 3:
                                        node2.G = node.G + 17;
                                        break;
                                }
                                int delX = Math.Abs(x - node2.X) * 10;
                                int delY = Math.Abs(y - node2.Y) * 10;
                                int delZ = Math.Abs(z - node2.Z) * 10;

                                node2.H = (int)Math.Sqrt(delX * delX + delY * delY + delZ * delZ);
                                node2.F = node2.G + node2.H;
                                openedNode.Add(hash, node2);
                            }
                            else
                            {
                                int G = 0;
                                int dif = 0;
                                if (i != node.X)
                                    dif++;
                                if (j != node.Y)
                                    dif++;
                                if (k != node.Z)
                                    dif++;
                                switch (dif)
                                {
                                    case 1:
                                        G = 10;
                                        break;
                                    case 2:
                                        G = 14;
                                        break;
                                    case 3:
                                        G = 17;
                                        break;
                                }
                                if (node.G + G > tmp.G)
                                {
                                    res = tmp;
                                }
                            }
                        }
                    }
                }
            }
            return res;
        }
    }
}
