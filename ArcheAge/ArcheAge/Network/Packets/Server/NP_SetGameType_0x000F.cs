using LocalCommons.Network;

namespace ArcheAge.ArcheAge.Network
{
    public sealed class NP_SetGameType_0x000F : NetPacket
    {
        public NP_SetGameType_0x000F() : base(02, 0x000F)
        {
            /*
[7]             S>c             0ms.            0:22:16 .659      24.03.14
               -------------------------------------------------------------------------------
               TType: ArcheageServer: GS1     Parse: off (auto)  EnCode: off         
               ------- 0  1  2  3  4  5  6  7 -  8  9  A  B  C  D  E  F    -------------------
               000000 08 00 DD 02 00 00 01 00 | 00 00 26 00 DD 02 0F 00     ..Ý.......&.Ý...
               000010 17 00 77 5F 64 61 72 6B | 5F 73 69 64 65 5F 6F 66     ..w_dark_side_of
               000020 5F 74 68 65 5F 6D 6F 6F | 6E 00 00 00 00 00 00 00     _the_moon.......
               000030 00 01 2B 00 DD 01 05 00 | 0A 00 61 61 2E 6D 61 69     ..+.Ý.....aa.mai
               000040 6C 2E 72 75 07 00 3E 32 | 0F 0F 79 00 33 00 00 00     l.ru..>2..y.3...
               000050 00 00 0A 00 32 00 00 00 | 00 00 01 01 01 01 01 1C     ....2...........
               000060 00 DD 01 B8 01 01 00 00 | 00 01 00 00 00 00 00 00     .Ý.¸............
               000070 00 00 00 00 00 73 CF 56 | 53 00 00 00 00 14 00 DD     .....sÏVS......Ý
               000080 01 CB 00 00 00 00 00 00 | 00 00 00 00 00 00 00 00     .Ë..............
               000090 00 00 00                                              ...
               -------------------------------------------------------------------------------
               Archeage: "ChangeState"                      size: 147    prot: 1  $001
               Addr:  Size:    Type:         Description:     Value:
               0000     2   word          psize             8          | $0008
               0002     2   word          type              733        | $02DD
               0004     2   word          ID                0          | $0000
               0006     4   integer       state             1          | $00000001
               */
            //1700DD020F00
            //ns.WriteHex("08006F5F74656D705F62000000000000000001");
            //          oc   level_len level                                          checksum         immersive
            //2600 DD02 0F00 1700      775F6461726B5F736964655F6F665F7468655F6D6F6F6E 0000000000000000 01

            const string level = "o_temp_c";
            ns.WriteUTF8Fixed(level, level.Length);  //записываем len, name
            ns.Write((long)0x00);
            ns.Write((byte)0x01);
        }
    }
}
