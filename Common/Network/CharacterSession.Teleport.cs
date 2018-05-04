using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading;
using SmartEngine.Network;
using SmartEngine.Network.Utils;

using SagaBNS.Common.Packets;
using SagaBNS.Common.Actors;
using SagaBNS.Common.Packets.CharacterServer;

namespace SagaBNS.Common.Network
{
    public abstract partial class CharacterSession<T>: DefaultClient<CharacterPacketOpcode>
    {
        public void GetLocations(uint charID, T client)
        {
            long session = Global.PacketSession;
            packetSessions[session] = client;

            CM_TELEPORT_GET p = new CM_TELEPORT_GET();
            p.SessionID = session;
            p.CharID = charID;

            this.Network.SendPacket(p);
        }

        public void SaveLocations(ActorPC pc)
        {
            CM_TELEPORT_SAVE p = new CM_TELEPORT_SAVE();
            p.CharID = pc.CharID;
            p.TeleportLocations = pc.Locations;

            this.Network.SendPacket(p);
        }

        public void OnTeleportInfo(SM_TELEPORT_INFO p)
        {
            long session = p.SessionID;
            if (packetSessions.ContainsKey(session))
            {
                T client;
                if (packetSessions.TryRemove(session, out client))
                {
                    OnTeleportInfo(client, p.TeleportLocations);
                }
            }
        }

        protected abstract void OnTeleportInfo(T client, List<ushort> locations);
    }
}
