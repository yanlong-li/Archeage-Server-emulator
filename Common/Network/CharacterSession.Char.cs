using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SmartEngine.Network;
using SmartEngine.Network.Utils;

using SagaBNS.Common.Actors;
using SagaBNS.Common.Packets;
using SagaBNS.Common.Packets.CharacterServer;

namespace SagaBNS.Common.Network
{
    public abstract partial class CharacterSession<T> : DefaultClient<CharacterPacketOpcode>
    {
        public void RequestCharList(uint accountID, T client)
        {
            long session = Global.PacketSession;
            packetSessions[session] = client;

            CM_CHAR_LIST_REQUEST p = new CM_CHAR_LIST_REQUEST();
            p.SessionID = session;
            p.AccountID = accountID;

            this.Network.SendPacket(p);
        }

        public void RequestCharInfo(uint charID, T client)
        {
            long session = Global.PacketSession;
            packetSessions[session] = client;

            CM_ACTOR_INFO_REQUEST p = new CM_ACTOR_INFO_REQUEST();
            p.SessionID = session;
            p.CharID = charID;

            this.Network.SendPacket(p);
        }

        public void OnCharList(SM_CHAR_LIST p)
        {
            long session = p.SessionID;
            T client;
            if (packetSessions.TryRemove(session, out client))
            {
                OnCharList(client, p.Characters);
            }
        }

        protected abstract void OnCharList(T client, List<ActorPC> chars);

        public void OnActorInfo(SM_ACTOR_INFO p)
        {
            long session = p.SessionID;
            T client;
            if (packetSessions.TryRemove(session, out client))
            {
                OnActorInfo(client, p.Character);
            }
        }

        protected abstract void OnActorInfo(T client, Common.Actors.ActorPC character);

        public void CreateChar(ActorPC character, T client)
        {
            long session = Global.PacketSession;
            packetSessions[session] = client;

            CM_CHAR_CREATE p = new CM_CHAR_CREATE();
            p.SessionID = session;
            p.Character = character;

            this.Network.SendPacket(p);
        }

        public void OnCharCreateResult(SM_CHAR_CREATE_RESULT p)
        {
            long session = p.SessionID;
            T client;
            if (packetSessions.TryRemove(session, out client))
            {
                OnCharCreateResult(client, p.CharID, p.Result);
            }
        }

        protected abstract void OnCharCreateResult(T client, uint charID, SM_CHAR_CREATE_RESULT.Results result);

        public void DeleteChar(uint charID, T client)
        {
            long session = Global.PacketSession;
            packetSessions[session] = client;

            CM_CHAR_DELETE p = new CM_CHAR_DELETE();
            p.SessionID = session;
            p.CharID = charID;

            this.Network.SendPacket(p);
        }

        public void OnCharDeleteResult(SM_CHAR_DELETE_RESULT p)
        {
            long session = p.SessionID;
            T client;
            if (packetSessions.TryRemove(session, out client))
            {
                OnCharDeleteResult(client, p.Result);
            }
        }

        protected abstract void OnCharDeleteResult(T client, SM_CHAR_DELETE_RESULT.Results result);

        public void CharacterSave(ActorPC chara)
        {
            CharacterSave(chara, true);
        }

        public void CharacterSave(ActorPC chara, bool complete)
        {
            if (complete)
            {
                SaveQuest(chara);
                SaveLocations(chara);
                SaveSkill(chara);
            } 
            CM_CHAR_SAVE p = new CM_CHAR_SAVE();
            p.Character = chara;
            this.Network.SendPacket(p);
        }
    }
}
