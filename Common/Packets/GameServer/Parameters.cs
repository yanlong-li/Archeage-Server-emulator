using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaBNS.Common.Packets.GameServer
{
    public static class Parameters
    {
        static Dictionary<PacketParameter, int> lengths;
        public static int GetLength(this PacketParameter p)
        {
            if (lengths == null)
            {
                lengths = new Dictionary<PacketParameter, int>();
                foreach (PacketParameter i in Enum.GetValues(typeof(PacketParameter)))
                {
                    lengths[i] = GetAttr(i).Length;
                }
            }
            return lengths[p];
        }

        private static ParameterData GetAttr(PacketParameter p)
        {
            return (ParameterData)Attribute.GetCustomAttribute(ForValue(p), typeof(ParameterData));
        }

        private static MemberInfo ForValue(PacketParameter p)
        {
            return typeof(PacketParameter).GetField(Enum.GetName(typeof(PacketParameter), p));
        }

    }

}
