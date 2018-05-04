using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SmartEngine.Network;
using SmartEngine.Network.Utils;

using SagaBNS.Common.Packets;
using SagaBNS.Common.Packets.CharacterServer;

namespace SagaBNS.Common.Network
{
    public abstract partial class CharacterSession<T> : DefaultClient<CharacterPacketOpcode>
    {
        public void OnLoginResult(Packets.CharacterServer.SM_LOGIN_RESULT p)
        {
            switch (p.Result)
            {
                case SM_LOGIN_RESULT.Results.OK:
                    this.state = SESSION_STATE.IDENTIFIED;
                    break;
                case SM_LOGIN_RESULT.Results.WRONG_PASSWORD:
                    this.state = SESSION_STATE.REJECTED;
                    break;
            }
        }
    }
}
