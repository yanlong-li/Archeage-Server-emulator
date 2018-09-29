using ArcheAge.ArcheAge.Holders;
using ArcheAge.ArcheAge.Network.Connections;
using ArcheAge.ArcheAge.Structuring;
using LocalCommons.Network;
using System.Collections.Generic;

namespace ArcheAge.ArcheAge.Network.Packets.Server.Utils
{
    public sealed class ItemInfo : NetPacket
    {
        public static void Write(int itemId)
        {
            ns.Write((int) itemId);
            switch (itemId)
            {
                case 0:
                    break;
                default:
                    ns.Write((long) Program.ObjectUid.Next()); //id[1] Q //TODO: сделать у вещей постоянные UID
                    ns.Write((byte) 0); //type[1] c
                    ns.Write((byte) 0); //flags[1] c
                    ns.Write((int) 0x01); //stackSize[1] d
                    byte detailType = 1;
                    ns.Write((byte) detailType); //detailType c
                    switch (detailType)
                    {
                        case 0:
                            break;
                        case 1:
                            ns.WriteHex(
                                "000000005500000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000"); //detail 51 b
                            break;
                        case 2:
                            ns.WriteHex("0000000000000000000000000000000000000000000000000000000000"); //detail 29 b
                            break;
                        case 3:
                            ns.WriteHex("000000000000"); //detail 6 b
                            break;
                        case 4:
                            ns.WriteHex("000000000000000000"); //detail 9 b
                            break;
                        case 5:
                            ns.WriteHex("00000000000000000000000000000000000000000000000000"); //detail 25 b
                            //ns.Write((long) 0); //type d
                            //ns.Write((long) 0); //x Q
                            //ns.Write((long) 0); //y Q
                            //ns.Write((float) 0); //z f
                            break;
                        case 6:
                        case 7:
                            ns.WriteHex("00000000000000000000000000000000"); //detail 16 b
                            break;
                        case 8:
                            ns.WriteHex("0000000000000000"); //detail 8 b
                            break;
                    }

                    ns.Write((long) 0); //creationTime[1] Q
                    ns.Write((int) 0); //lifespanMins[1] d
                    ns.Write((int) 0); //type[1] d
                    ns.Write((byte) 0x0b); //worldId c"
                    ns.Write((long) 0); //unsecureDateTime Q
                    ns.Write((long) 0); //unpackDateTime Q
                    break;
            }
        }

        public ItemInfo(byte level, int packetId) : base(level, packetId)
        {
        }
    }
}