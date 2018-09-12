using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArcheAge.ArcheAge.Holders;
using LocalCommons.Network;

namespace ArcheAge.ArcheAge.Network.Packets.Server
{
    public sealed class NP_SCCharacterDeleted_0x0036 : NetPacket
    {
        public NP_SCCharacterDeleted_0x0036(int characterId, string characterName) : base(01, 0x0036)
        {
            ns.Write((int)characterId); //characterId d
            ns.WriteUTF8Fixed(characterName, characterName.Length); //characterName S
        }
    }
}
