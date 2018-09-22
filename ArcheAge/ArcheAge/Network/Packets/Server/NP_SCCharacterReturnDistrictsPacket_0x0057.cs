using ArcheAge.ArcheAge.Network.Connections;
using LocalCommons.Network;

namespace ArcheAge.ArcheAge.Network
{
    public sealed class NP_SCCharacterReturnDistrictsPacket_0x0057 : NetPacket
    {
        public NP_SCCharacterReturnDistrictsPacket_0x0057(ClientConnection net) : base(01, 0x0057)
        {
            //1.0.1406
            //SCCharacterReturnDistrictsPacket
            //7900 DD01 5700 02000000
            //6B600800 2900 D09ED0BAD180D0B5D181D182D0BDD0BED181D182D0B820D0A5D0BED183D0BFD184D0BED180D0B4D0B0 B3000000 C40F9B44 BD650145 F47DFC42 8B6C3940
            //31750800 1000 D0A5D0BED183D0BFD184D0BED180D0B4                                                   B3000000 2D73A544 78DBE544 79E9F542 51A00B40
            //6B600800
            
            //02000000
            int count = 2;
            ns.Write((int)count); //count d
            //for (int i = 0; i < count; i++) //- <for id="0">
            {
                //(0)
                //6B600800
                ns.Write((int)0x08606b);   //DistrictId d
                //2900
                //D09E D0BA D180 D0B5 D181 D182 D0BD D0BE D181 D182 D0B8 20 D0A5 D0BE D183 D0BF D184 D0BE D180 D0B4 D0B0
                string msg = "Что то по корейски: зона 1";
                ns.WriteUTF8Fixed(msg, msg.Length); //name SS
                //B3000000
                ns.Write((int) 0xb3);    //zoneId d
                //C40F9B44   
                ns.Write((float)1240.5); //pos[0] f 0x449b0fc4
                //BD650145   
                ns.Write((float)2070.4); //pos[1] f 0x450165bd
                //F47DFC42   
                ns.Write((float)126.2);  //pos[2] f 0x42fc7df4
                //8B6C3940   
                ns.Write((float)2.897);  //zRot f 0x40396c8b
                //31750800
                //(1)
                ns.Write((int)0x087531);   //DistrictId d
                //1000
                //D0A5D0BED183D0BFD184D0BED180D0B4
                msg = "Что то по корейски: зона 2";
                ns.WriteUTF8Fixed(msg, msg.Length); //name SS
                //B3000000
                ns.Write((int)0xb3);     //zoneId d
                //2D73A544
                ns.Write((float)1323.6); //pos[0] f 0x44a5732d
                //78DBE544
                ns.Write((float)1838.9); //pos[1] f 0x44e5db78
                //79E9F542
                ns.Write((float)122.9); //pos[2] f 0x42f5e979
                //51A00B40
                ns.Write((float)2.182); //zRot f 0x400ba021
            } //</for>
            //B600800
            ns.Write((int)0x08606b); //returnDistrictId d
        }
    }
}