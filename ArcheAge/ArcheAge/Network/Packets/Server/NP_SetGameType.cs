using LocalCommons.Network;

namespace ArcheAge.ArcheAge.Net
{
    public sealed class NP_SetGameType : NetPacket
    {
        public NP_SetGameType() : base(02, 0x000F)
        {
            /*
            [10]            S>c             0ms.            23:56:47 .221      10.03.18
               -------------------------------------------------------------------------------
               TType: ArcheageServer: undef   Parse: 6           EnCode: off         
               ------- 0  1  2  3  4  5  6  7 -  8  9  A  B  C  D  E  F    -------------------
               000000 17 00 DD 02 0F 00 08 00 | 6F 5F 74 65 6D 70 5F 62     ..Э.....o_temp_b
               000010 00 00 00 00 00 00 00 00 | 01                          .........
               -------------------------------------------------------------------------------
               Archeage: "SetGameType"                      size: 25     prot: 2  $002
               Addr:  Size:    Type:         Description:     Value:
               0000     2   word          psize             23         | $0017
               0002     2   word          type              733        | $02DD
               0004     2   word          ID                15         | $000F
               0006    10   WideStr[byte] name              o_temp_b  ($)
               0010     8   int64         checkSum          0          | $00000000
               0018     1   byte          immersive         1          | $01
             */
            //1700DD020F00
            //ns.WriteHex("08006F5F74656D705F62000000000000000001");
            const string name = "w_the_carcass_2";
            ns.WriteUTF8Fixed(name, name.Length);  //записываем len, name
            ns.Write((long)0x00);
            ns.Write((byte)0x01);
        }
    }
}
