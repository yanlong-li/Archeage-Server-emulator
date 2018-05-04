using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartEngine.Network.Map
{
    public enum MoveType
    {
        Start,
        End
    }
    public class MoveArg
    {
        public MoveType MoveType;
        public int X, Y, Z;
        public ushort Dir;
        public ushort Speed;
    }
}
