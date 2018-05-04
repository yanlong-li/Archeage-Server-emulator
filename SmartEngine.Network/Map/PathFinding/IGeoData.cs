using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartEngine.Network.Map.PathFinding
{
    /// <summary>
    /// 地理数据接口
    /// </summary>
    public interface IGeoData
    {
        /// <summary>
        /// 某坐标是否可通行
        /// </summary>
        /// <param name="fromX">起始X坐标</param>
        /// <param name="fromY">起始Y坐标</param>
        /// <param name="fromZ">起始Z坐标</param>
        /// <param name="toX">目标X坐标</param>
        /// <param name="toY">目标X坐标</param>
        /// <param name="toZ">目标Z坐标</param>
        /// <returns>是否可通行</returns>
        bool IsWalkable(int fromX, int fromY, int fromZ, int toX, int toY, int toZ);
        /// <summary>
        /// 标准化坐标，将坐标转换成网格大小为1的坐标
        /// </summary>
        /// <param name="x">输入X坐标</param>
        /// <param name="y">输入Y坐标</param>
        /// <param name="z">输入Z坐标</param>
        /// <param name="res_x">输出X坐标</param>
        /// <param name="res_y">输出Y坐标</param>
        /// <param name="res_z">输出Z坐标</param>
        void NormalizeCoordinates(int x, int y, int z, out int res_x, out int res_y, out int res_z);

        /// <summary>
        /// 将标准化的坐标重新转换成游戏坐标
        /// </summary>
        /// <param name="x">输入X坐标</param>
        /// <param name="y">输入Y坐标</param>
        /// <param name="z">输入Z坐标</param>
        /// <param name="res_x">输出X坐标</param>
        /// <param name="res_y">输出Y坐标</param>
        /// <param name="res_z">输出Z坐标</param>
        void RealCoordinatesFromNormalized(int x, int y, int z, out int res_x, out int res_y, out int res_z);
    }
}
