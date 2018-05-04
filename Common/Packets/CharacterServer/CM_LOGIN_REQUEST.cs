using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SmartEngine.Network;
namespace SagaBNS.Common.Packets.CharacterServer
{
    public class CM_LOGIN_REQUEST : Packet<CharacterPacketOpcode>
    {
        public CM_LOGIN_REQUEST()
        {
            this.ID = CharacterPacketOpcode.CM_LOGIN_REQUEST;
        }

        public string Password
        {
            get
            {
                return GetString(2);
            }
            set
            {
                PutString(value, 2);
            }
        }
    }
}
