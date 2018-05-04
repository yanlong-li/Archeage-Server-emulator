using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaBNS.Common.Network
{
    public enum SESSION_STATE
    {
        /// <summary>
        /// 已连接
        /// </summary>
        CONNECTED,
        /// <summary>
        /// 断开
        /// </summary>
        DISCONNECTED,
        /// <summary>
        /// 尚未验证
        /// </summary>
        NOT_IDENTIFIED,
        /// <summary>
        /// 已通过验证
        /// </summary>
        IDENTIFIED,
        /// <summary>
        /// 拒绝
        /// </summary>
        REJECTED,
        /// <summary>
        /// 连接失败
        /// </summary>
        FAILED
    }
}
