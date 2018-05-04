using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SmartEngine.Network;
using SagaBNS.Common.Network;
namespace SagaBNS.Common.Packets.CharacterServer
{
    public class SM_CHAR_DELETE_RESULT : Packet<CharacterPacketOpcode>
    {
        public enum Results
        {
            OK,
            ERROR,
        }
        internal class SM_CHAR_DELETE_RESULT_INTERNAL<T> : SM_CHAR_DELETE_RESULT
        {
            public override Packet<CharacterPacketOpcode> New()
            {
                return new SM_CHAR_DELETE_RESULT_INTERNAL<T>();
            }

            public override void OnProcess(Session<CharacterPacketOpcode> client)
            {
                ((CharacterSession<T>)client).OnCharDeleteResult(this);
            }
        }
        public SM_CHAR_DELETE_RESULT()
        {
            this.ID = CharacterPacketOpcode.SM_CHAR_DELETE_RESULT;
        }

        public long SessionID
        {
            get
            {
                return GetLong(2);
            }
            set
            {
                PutLong(value, 2);
            }
        }

        public Results Result
        {
            get
            {
                return (Results)GetByte(10);
            }
            set
            {
                PutByte((byte)value, 10);
            }
        }
    }
}
