using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaBNS.Common.Packets.GameServer
{
    public class ParameterData : Attribute
    {

        internal ParameterData(int length)
        {
            this.Length = length;
        }

        public int Length { get; private set; }
    }
}
