using ArcheAge.Properties;
using LocalCommons.Native.Network;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;

namespace ArcheAge.ArcheAge.Net
{

    public sealed class NP_Undefined : NetPacket
    {
        public NP_Undefined() : base(1, 0x1AB)
        {
            ns.Write((int)0x00);
            ns.Write((byte)0x00);
            ns.Write((int)0x00);
        }
    }

    /// <summary>
    /// Undefined Packet.
    /// </summary>
    public sealed class NP_ClientConnected : NetPacket
    {
        public NP_ClientConnected() : base(1, 0x00)
        {
            ns.Write((short)0x00);
            ns.Write((byte)0x00);
            //主地址
            ns.Write((byte)0x01);
            ns.Write((byte)0x00);
            ns.Write((byte)0x00);
            ns.Write((byte)0x7f); //Undefined IP ??? Web Ip?
            ns.Write((short)0x4e2); //Undefined Port ??? Web Port ?
                                    //主地址
            ns.Write((byte)0x01);
            ns.Write((byte)0x00);
            ns.Write((byte)0x00);
            ns.Write((byte)0x7f);
            ns.Write((int)0x0); //Undefined
        }
    }
    //2.9na op1
    public sealed class NP_ClientConnected2 : NetPacket
    {
        public NP_ClientConnected2() : base(5, 0xb610)
        {
            ns.WriteHex("875727f7c7e620960e33a5573274b8b2825222f7c297623300d4a4cb5332f66c15dc3664010c9367d1ef27877dd58c482d9664393e8582e371754b32ee95c397653f0f9c33c4e528ccfcabb90835673f1c1eb8694e0a285cbb2354b934b9aeb068c1183abb07f52b3dc0b687b8780bd837978cfb719a6c81ec4cba974ddc9ba118698b69c28eca8f82f4ded8675e40729c6671ed3898929557782996663707d7a7774010e0b0815121f1c192623202d2a3734313e3b4845424f4c595653505d6a6764616e7b7875727fed0a0704011e1b1815122f2c292623303d3a3744414e4b4855525f5c596663606d6a7774717e7b0805020f0c191613101d2a2724212e3b3835323f4c494643405d5a5754516e6b6865727f7c797704110e15fc300733586");

         }
    }
    public sealed class NP_Hex : NetPacket
    {
        public NP_Hex(string value) : base(0, 0x0)
        {
            ns.WriteHex(value);
        }
    }
    public sealed class NP_Client01 : NetPacket
    {
        /**
         * 
         * 3540
         * 3557
         * 3528
         * 
         * */
        public NP_Client01() : base(05, 0x3540)
        {
            #region 历史返回结果  失败
            //ns.Write((byte)0xc4); //未知随机数
            //ns.Write((int)0x3505d5a5);//3505d5a576
            //ns.Write((byte)0x76);
            //ns.Write(0x1a2047956553ce0c);//未知随机数
            //ns.Write((long)0x0fc96030fed1a171);//0fc96030fed1a1714111e6b3865322f7c393
            //ns.Write((long)0x4111e6b3865322f7);
            //ns.Write((byte)0xC3);
            //ns.Write((byte)0x93);
            //ns.Writec(0xa2e7edb8bed66c36,true);//a2e7edb8bed66c3621da1c9298dbb7b6bb7509d6a94091f952a2d2540429696e28b45e387092520eb5c89bb3f9d3a419fb08c32689c6074425a4d46783a6472cbabef22f678e3cb42595c0250b29daefd008b29063bc88089d12551a7c283c4198ebe5e2233f1cb66ebc6458fd5b41dc02f9cb5a265e425fc159dfd817b9d9c2
            //ns.Writec(0x21da1c9298dbb7b6, true);
            //ns.Writec(0xbb7509d6a94091f9, true);
            //ns.Writec(0x52a2d2540429696e, true);
            //ns.Writec(0x28b45e387092520e, true);
            //ns.Writec(0xb5c89bb3f9d3a419, true);
            //ns.Writec(0xfb08c32689c60744, true);
            //ns.Writec(0x25a4d46783a6472c, true);
            //ns.Writec(0xbabef22f678e3cb4, true);
            //ns.Writec(0x2595c0250b29daef, true);
            //ns.Writec(0xd008b29063bc8808, true);
            //ns.Writec(0x9d12551a7c283c41, true);
            //ns.Writec(0x98ebe5e2233f1cb6, true);
            //ns.Writec(0x6ebc6458fd5b41dc, true);
            //ns.Writec(0x02f9cb5a265e425f, true);
            //ns.Writec(0xc159dfd817b9d9c2, true);
            //ns.Writec(0x855526f6c6966637, true);//855526f6c696663707d7a7774010e0b0815121f1c192623202d2a3734313e3b4845424f4c595653505d6a6764616e7b7875727fed0a0704011e1b1815122f2c292623303d3a3744414e4b4855525f5c596663606d6a7774717e7b0805020f0c191613101d2a2724212e3b3835323f4c494643405d5a5754516e6b6865726f7c6e0351175
            //ns.Writec(0x07d7a7774010e0b0, true);
            //ns.Writec(0x815121f1c1926232, true);
            //ns.Writec(0x02d2a3734313e3b4, true);
            //ns.Writec(0x845424f4c5956535, true);
            //ns.Writec(0x05d6a6764616e7b7, true);
            //ns.Writec(0x875727fed0a07040, true);
            //ns.Writec(0x11e1b1815122f2c2, true);
            //ns.Writec(0x92623303d3a37444, true);
            //ns.Writec(0x14e4b4855525f5c5, true);
            //ns.Writec(0x96663606d6a77747, true);
            //ns.Writec(0x17e7b0805020f0c1, true);
            //ns.Writec(0x91613101d2a27242, true);
            //ns.Writec(0x12e3b3835323f4c4, true);
            //ns.Writec(0x94643405d5a57545, true);
            //ns.Writec(0x16e6b6865726f7c6, true);
            //ns.Write(0xe0351175);
            //ns.Write((byte)0xe9);
            //ns.Write((byte)0xef);
            //ns.Write(0x805194c4);
            #endregion

            #region 模拟返回结果集
            //ns.Write((byte)0x2c);
            //ns.Write((byte)0x01);
            //ns.Write((byte)0xdd);
            //ns.Write((byte)0x05);
            //ns.Write((byte)0xc4);
            //ns.Write((byte)0x35);
            //ns.Write((byte)0x05);
            //ns.Write((byte)0xd5);
            //ns.Write((byte)0xa5);
            //ns.Write((byte)0x76);
            //ns.Write((byte)0x37);
            //ns.Write((byte)0xdd);
            //ns.Write((byte)0x0a);
            //ns.Write((byte)0xc3);
            //ns.Write((byte)0x65);
            //ns.Write((byte)0x53);
            //ns.Write((byte)0xce);
            //ns.Write((byte)0x0c);
            //ns.Write((byte)0x0f);
            //ns.Write((byte)0xc9);
            //ns.Write((byte)0x60);
            //ns.Write((byte)0x30);
            //ns.Write((byte)0xfe);
            //ns.Write((byte)0xd1);
            //ns.Write((byte)0xa1);
            //ns.Write((byte)0x71);
            //ns.Write((byte)0x41);
            //ns.Write((byte)0x11);
            //ns.Write((byte)0xe6);
            //ns.Write((byte)0xb3);
            //ns.Write((byte)0x86);
            //ns.Write((byte)0x53);
            //ns.Write((byte)0x22);
            //ns.Write((byte)0xf7);
            //ns.Write((byte)0xc3);
            //ns.Write((byte)0x93);
            //ns.Write((byte)0xa2);
            //ns.Write((byte)0xe7);
            //ns.Write((byte)0xed);
            //ns.Write((byte)0xb8);
            //ns.Write((byte)0xbe);
            //ns.Write((byte)0xd6);
            //ns.Write((byte)0x6c);
            //ns.Write((byte)0x36);
            //ns.Write((byte)0x21);
            //ns.Write((byte)0xda);
            //ns.Write((byte)0x1c);
            //ns.Write((byte)0x92);
            //ns.Write((byte)0x98);
            //ns.Write((byte)0xdb);
            //ns.Write((byte)0xb7);
            //ns.Write((byte)0xb6);
            //ns.Write((byte)0xbb);
            //ns.Write((byte)0x75);
            //ns.Write((byte)0x09);
            //ns.Write((byte)0xd6);
            //ns.Write((byte)0xa9);
            //ns.Write((byte)0x40);
            //ns.Write((byte)0x91);
            //ns.Write((byte)0xf9);
            //ns.Write((byte)0x52);
            //ns.Write((byte)0xa2);
            //ns.Write((byte)0xd2);
            //ns.Write((byte)0x54);
            //ns.Write((byte)0x04);
            //ns.Write((byte)0x29);
            //ns.Write((byte)0x69);
            //ns.Write((byte)0x6e);
            //ns.Write((byte)0x28);
            //ns.Write((byte)0xb4);
            //ns.Write((byte)0x5e);
            //ns.Write((byte)0x38);
            //ns.Write((byte)0x70);
            //ns.Write((byte)0x92);
            //ns.Write((byte)0x52);
            //ns.Write((byte)0x0e);
            //ns.Write((byte)0xb5);
            //ns.Write((byte)0xc8);
            //ns.Write((byte)0x9b);
            //ns.Write((byte)0xb3);
            //ns.Write((byte)0xf9);
            //ns.Write((byte)0xd3);
            //ns.Write((byte)0xa4);
            //ns.Write((byte)0x19);
            //ns.Write((byte)0xfb);
            //ns.Write((byte)0x08);
            //ns.Write((byte)0xc3);
            //ns.Write((byte)0x26);
            //ns.Write((byte)0x89);
            //ns.Write((byte)0xc6);
            //ns.Write((byte)0x07);
            //ns.Write((byte)0x44);
            //ns.Write((byte)0x25);
            //ns.Write((byte)0xa4);
            //ns.Write((byte)0xd4);
            //ns.Write((byte)0x67);
            //ns.Write((byte)0x83);
            //ns.Write((byte)0xa6);
            //ns.Write((byte)0x47);
            //ns.Write((byte)0x2c);
            //ns.Write((byte)0xba);
            //ns.Write((byte)0xbe);
            //ns.Write((byte)0xf2);
            //ns.Write((byte)0x2f);
            //ns.Write((byte)0x67);
            //ns.Write((byte)0x8e);
            //ns.Write((byte)0x3c);
            //ns.Write((byte)0xb4);
            //ns.Write((byte)0x25);
            //ns.Write((byte)0x95);
            //ns.Write((byte)0xc0);
            //ns.Write((byte)0x25);
            //ns.Write((byte)0x0b);
            //ns.Write((byte)0x29);
            //ns.Write((byte)0xda);
            //ns.Write((byte)0xef);
            //ns.Write((byte)0xd0);
            //ns.Write((byte)0x08);
            //ns.Write((byte)0xb2);
            //ns.Write((byte)0x90);
            //ns.Write((byte)0x63);
            //ns.Write((byte)0xbc);
            //ns.Write((byte)0x88);
            //ns.Write((byte)0x08);
            //ns.Write((byte)0x9d);
            //ns.Write((byte)0x12);
            //ns.Write((byte)0x55);
            //ns.Write((byte)0x1a);
            //ns.Write((byte)0x7c);
            //ns.Write((byte)0x28);
            //ns.Write((byte)0x3c);
            //ns.Write((byte)0x41);
            //ns.Write((byte)0x98);
            //ns.Write((byte)0xeb);
            //ns.Write((byte)0xe5);
            //ns.Write((byte)0xe2);
            //ns.Write((byte)0x23);
            //ns.Write((byte)0x3f);
            //ns.Write((byte)0x1c);
            //ns.Write((byte)0xb6);
            //ns.Write((byte)0x6e);
            //ns.Write((byte)0xbc);
            //ns.Write((byte)0x64);
            //ns.Write((byte)0x58);
            //ns.Write((byte)0xfd);
            //ns.Write((byte)0x5b);
            //ns.Write((byte)0x41);
            //ns.Write((byte)0xdc);
            //ns.Write((byte)0x02);
            //ns.Write((byte)0xf9);
            //ns.Write((byte)0xcb);
            //ns.Write((byte)0x5a);
            //ns.Write((byte)0x26);
            //ns.Write((byte)0x5e);
            //ns.Write((byte)0x42);
            //ns.Write((byte)0x5f);
            //ns.Write((byte)0xc1);
            //ns.Write((byte)0x59);
            //ns.Write((byte)0xdf);
            //ns.Write((byte)0xd8);
            //ns.Write((byte)0x17);
            //ns.Write((byte)0xb9);
            //ns.Write((byte)0xd9);
            //ns.Write((byte)0xc2);
            //ns.Write((byte)0x85);
            //ns.Write((byte)0x55);
            //ns.Write((byte)0x26);
            //ns.Write((byte)0xf6);
            //ns.Write((byte)0xc6);
            //ns.Write((byte)0x96);
            //ns.Write((byte)0x66);
            //ns.Write((byte)0x37);
            //ns.Write((byte)0x07);
            //ns.Write((byte)0xd7);
            //ns.Write((byte)0xa7);
            //ns.Write((byte)0x77);
            //ns.Write((byte)0x40);
            //ns.Write((byte)0x10);
            //ns.Write((byte)0xe0);
            //ns.Write((byte)0xb0);
            //ns.Write((byte)0x81);
            //ns.Write((byte)0x51);
            //ns.Write((byte)0x21);
            //ns.Write((byte)0xf1);
            //ns.Write((byte)0xc1);
            //ns.Write((byte)0x92);
            //ns.Write((byte)0x62);
            //ns.Write((byte)0x32);
            //ns.Write((byte)0x02);
            //ns.Write((byte)0xd2);
            //ns.Write((byte)0xa3);
            //ns.Write((byte)0x73);
            //ns.Write((byte)0x43);
            //ns.Write((byte)0x13);
            //ns.Write((byte)0xe3);
            //ns.Write((byte)0xb4);
            //ns.Write((byte)0x84);
            //ns.Write((byte)0x54);
            //ns.Write((byte)0x24);
            //ns.Write((byte)0xf4);
            //ns.Write((byte)0xc5);
            //ns.Write((byte)0x95);
            //ns.Write((byte)0x65);
            //ns.Write((byte)0x35);
            //ns.Write((byte)0x05);
            //ns.Write((byte)0xd6);
            //ns.Write((byte)0xa6);
            //ns.Write((byte)0x76);
            //ns.Write((byte)0x46);
            //ns.Write((byte)0x16);
            //ns.Write((byte)0xe7);
            //ns.Write((byte)0xb7);
            //ns.Write((byte)0x87);
            //ns.Write((byte)0x57);
            //ns.Write((byte)0x27);
            //ns.Write((byte)0xfe);
            //ns.Write((byte)0xd0);
            //ns.Write((byte)0xa0);
            //ns.Write((byte)0x70);
            //ns.Write((byte)0x40);
            //ns.Write((byte)0x11);
            //ns.Write((byte)0xe1);
            //ns.Write((byte)0xb1);
            //ns.Write((byte)0x81);
            //ns.Write((byte)0x51);
            //ns.Write((byte)0x22);
            //ns.Write((byte)0xf2);
            //ns.Write((byte)0xc2);
            //ns.Write((byte)0x92);
            //ns.Write((byte)0x62);
            //ns.Write((byte)0x33);
            //ns.Write((byte)0x03);
            //ns.Write((byte)0xd3);
            //ns.Write((byte)0xa3);
            //ns.Write((byte)0x74);
            //ns.Write((byte)0x44);
            //ns.Write((byte)0x14);
            //ns.Write((byte)0xe4);
            //ns.Write((byte)0xb4);
            //ns.Write((byte)0x85);
            //ns.Write((byte)0x55);
            //ns.Write((byte)0x25);
            //ns.Write((byte)0xf5);
            //ns.Write((byte)0xc5);
            //ns.Write((byte)0x96);
            //ns.Write((byte)0x66);
            //ns.Write((byte)0x36);
            //ns.Write((byte)0x06);
            //ns.Write((byte)0xd6);
            //ns.Write((byte)0xa7);
            //ns.Write((byte)0x77);
            //ns.Write((byte)0x47);
            //ns.Write((byte)0x17);
            //ns.Write((byte)0xe7);
            //ns.Write((byte)0xb0);
            //ns.Write((byte)0x80);
            //ns.Write((byte)0x50);
            //ns.Write((byte)0x20);
            //ns.Write((byte)0xf0);
            //ns.Write((byte)0xc1);
            //ns.Write((byte)0x91);
            //ns.Write((byte)0x61);
            //ns.Write((byte)0x31);
            //ns.Write((byte)0x01);
            //ns.Write((byte)0xd2);
            //ns.Write((byte)0xa2);
            //ns.Write((byte)0x72);
            //ns.Write((byte)0x42);
            //ns.Write((byte)0x12);
            //ns.Write((byte)0xe3);
            //ns.Write((byte)0xb3);
            //ns.Write((byte)0x83);
            //ns.Write((byte)0x53);
            //ns.Write((byte)0x23);
            //ns.Write((byte)0xf4);
            //ns.Write((byte)0xc4);
            //ns.Write((byte)0x94);
            //ns.Write((byte)0x64);
            //ns.Write((byte)0x34);
            //ns.Write((byte)0x05);
            //ns.Write((byte)0xd5);
            //ns.Write((byte)0xa5);
            //ns.Write((byte)0x75);
            //ns.Write((byte)0x45);
            //ns.Write((byte)0x16);
            //ns.Write((byte)0xe6);
            //ns.Write((byte)0xb6);
            //ns.Write((byte)0x86);
            //ns.Write((byte)0x57);
            //ns.Write((byte)0x26);
            //ns.Write((byte)0xf7);
            //ns.Write((byte)0xc6);
            //ns.Write((byte)0x1b);
            //ns.Write((byte)0x2a);
            //ns.Write((byte)0xa7);
            //ns.Write((byte)0x75);
            //ns.Write((byte)0xe9);
            //ns.Write((byte)0xef);
            //ns.Write((byte)0x80);
            //ns.Write((byte)0x51);
            //ns.Write((byte)0x94);
            //ns.Write((byte)0xc4);
            #endregion
            #region 中国区固定编码测试
            ns.Write((byte)0x05);
            ns.Write((byte)0xd5);
            ns.Write((byte)0xa5);
            ns.Write((byte)0x76);
            ns.Write((byte)0x37);//
            ns.Write((byte)0xdd);//
            ns.Write((byte)0x0a);//
            ns.Write((byte)0xc3);//
            ns.Write((byte)0x65);
            ns.Write((byte)0x53);
            ns.Write((byte)0x61);//
            ns.Write((byte)0x3e);//
            ns.Write((byte)0x39);//
            ns.Write((byte)0xc9);//
            ns.Write((byte)0x60);
            ns.Write((byte)0x30);
            ns.Write((byte)0xfe);
            ns.Write((byte)0xd1);
            ns.Write((byte)0x81);
            ns.Write((byte)0x8f);
            ns.Write((byte)0xbe);
            ns.Write((byte)0xee);
            ns.Write((byte)0xe6);
            ns.Write((byte)0xb3);
            ns.Write((byte)0x86);
            ns.Write((byte)0x53);
            ns.Write((byte)0x22);
            ns.Write((byte)0xf7);
            ns.Write((byte)0xc3);
            ns.Write((byte)0x93);
            ns.Write((byte)0x94);
            ns.Write((byte)0x43);
            ns.Write((byte)0x68);
            ns.Write((byte)0x8a);
            ns.Write((byte)0x49);
            ns.Write((byte)0xbc);
            ns.Write((byte)0x35);
            ns.Write((byte)0x39);
            ns.Write((byte)0x6f);
            ns.Write((byte)0x73);
            ns.Write((byte)0xce);
            ns.Write((byte)0x23);
            ns.Write((byte)0xee);
            ns.Write((byte)0x39);
            ns.Write((byte)0xea);
            ns.Write((byte)0x8d);
            ns.Write((byte)0x68);
            ns.Write((byte)0x4e);
            ns.Write((byte)0x03);
            ns.Write((byte)0xe3);
            ns.Write((byte)0xdc);
            ns.Write((byte)0xd5);
            ns.Write((byte)0x72);
            ns.Write((byte)0x92);
            ns.Write((byte)0xa0);
            ns.Write((byte)0xa9);
            ns.Write((byte)0xb7);
            ns.Write((byte)0x28);
            ns.Write((byte)0xcd);
            ns.Write((byte)0xb1);
            ns.Write((byte)0x51);
            ns.Write((byte)0x38);
            ns.Write((byte)0xd3);
            ns.Write((byte)0x33);
            ns.Write((byte)0x8d);
            ns.Write((byte)0xb3);
            ns.Write((byte)0xee);
            ns.Write((byte)0x8f);
            ns.Write((byte)0x25);
            ns.Write((byte)0xa4);
            ns.Write((byte)0xdb);
            ns.Write((byte)0xb2);
            ns.Write((byte)0x30);
            ns.Write((byte)0x0d);
            ns.Write((byte)0xad);
            ns.Write((byte)0x7e);
            ns.Write((byte)0xe1);
            ns.Write((byte)0x47);
            ns.Write((byte)0x42);
            ns.Write((byte)0xda);
            ns.Write((byte)0x15);
            ns.Write((byte)0xe3);
            ns.Write((byte)0xdc);
            ns.Write((byte)0xe0);
            ns.Write((byte)0x6e);
            ns.Write((byte)0x47);
            ns.Write((byte)0xef);
            ns.Write((byte)0xdd);
            ns.Write((byte)0xaa);
            ns.Write((byte)0x08);
            ns.Write((byte)0x0d);
            ns.Write((byte)0x3e);
            ns.Write((byte)0x23);
            ns.Write((byte)0xf5);
            ns.Write((byte)0xbd);
            ns.Write((byte)0x53);
            ns.Write((byte)0xd3);
            ns.Write((byte)0x4d);
            ns.Write((byte)0x15);
            ns.Write((byte)0x06);
            ns.Write((byte)0x64);
            ns.Write((byte)0x3f);
            ns.Write((byte)0x63);
            ns.Write((byte)0xf1);
            ns.Write((byte)0xaf);
            ns.Write((byte)0x1b);
            ns.Write((byte)0x9e);
            ns.Write((byte)0x02);
            ns.Write((byte)0xda);
            ns.Write((byte)0x31);
            ns.Write((byte)0x0b);
            ns.Write((byte)0xa6);
            ns.Write((byte)0xc0);
            ns.Write((byte)0x18);
            ns.Write((byte)0x06);
            ns.Write((byte)0xe0);
            ns.Write((byte)0x63);
            ns.Write((byte)0x3d);
            ns.Write((byte)0x71);
            ns.Write((byte)0xa0);
            ns.Write((byte)0x41);
            ns.Write((byte)0xdf);
            ns.Write((byte)0xdf);
            ns.Write((byte)0x6e);
            ns.Write((byte)0x97);
            ns.Write((byte)0xc5);
            ns.Write((byte)0x63);
            ns.Write((byte)0x37);
            ns.Write((byte)0xb2);
            ns.Write((byte)0x24);
            ns.Write((byte)0xc1);
            ns.Write((byte)0x6b);
            ns.Write((byte)0xcd);
            ns.Write((byte)0xda);
            ns.Write((byte)0x95);
            ns.Write((byte)0xa5);
            ns.Write((byte)0x60);
            ns.Write((byte)0x70);
            ns.Write((byte)0xbc);
            ns.Write((byte)0x8e);
            ns.Write((byte)0x79);
            ns.Write((byte)0x97);
            ns.Write((byte)0x8b);
            ns.Write((byte)0x98);
            ns.Write((byte)0xdb);
            ns.Write((byte)0x8f);
            ns.Write((byte)0x9b);
            ns.Write((byte)0xa7);
            ns.Write((byte)0x3c);
            ns.Write((byte)0xc6);
            ns.Write((byte)0xc8);
            ns.Write((byte)0xc5);
            ns.Write((byte)0x49);
            ns.Write((byte)0xca);
            ns.Write((byte)0x54);
            ns.Write((byte)0x3f);
            ns.Write((byte)0x6d);
            ns.Write((byte)0x6e);
            ns.Write((byte)0x85);
            ns.Write((byte)0x55);
            ns.Write((byte)0x26);
            ns.Write((byte)0xf6);
            ns.Write((byte)0xc6);
            ns.Write((byte)0x96);
            ns.Write((byte)0x66);
            ns.Write((byte)0x37);
            ns.Write((byte)0x07);
            ns.Write((byte)0xd7);
            ns.Write((byte)0xa7);
            ns.Write((byte)0x77);
            ns.Write((byte)0x40);
            ns.Write((byte)0x10);
            ns.Write((byte)0xe0);
            ns.Write((byte)0xb0);
            ns.Write((byte)0x81);
            ns.Write((byte)0x51);
            ns.Write((byte)0x21);
            ns.Write((byte)0xf1);
            ns.Write((byte)0xc1);
            ns.Write((byte)0x92);
            ns.Write((byte)0x62);
            ns.Write((byte)0x32);
            ns.Write((byte)0x02);
            ns.Write((byte)0xd2);
            ns.Write((byte)0xa3);
            ns.Write((byte)0x73);
            ns.Write((byte)0x43);
            ns.Write((byte)0x13);
            ns.Write((byte)0xe3);
            ns.Write((byte)0xb4);
            ns.Write((byte)0x84);
            ns.Write((byte)0x54);
            ns.Write((byte)0x24);
            ns.Write((byte)0xf4);
            ns.Write((byte)0xc5);
            ns.Write((byte)0x95);
            ns.Write((byte)0x65);
            ns.Write((byte)0x35);
            ns.Write((byte)0x05);
            ns.Write((byte)0xd6);
            ns.Write((byte)0xa6);
            ns.Write((byte)0x76);
            ns.Write((byte)0x46);
            ns.Write((byte)0x16);
            ns.Write((byte)0xe7);
            ns.Write((byte)0xb7);
            ns.Write((byte)0x87);
            ns.Write((byte)0x57);
            ns.Write((byte)0x27);
            ns.Write((byte)0xfe);
            ns.Write((byte)0xd0);
            ns.Write((byte)0xa0);
            ns.Write((byte)0x70);
            ns.Write((byte)0x40);
            ns.Write((byte)0x11);
            ns.Write((byte)0xe1);
            ns.Write((byte)0xb1);
            ns.Write((byte)0x81);
            ns.Write((byte)0x51);
            ns.Write((byte)0x22);
            ns.Write((byte)0xf2);
            ns.Write((byte)0xc2);
            ns.Write((byte)0x92);
            ns.Write((byte)0x62);
            ns.Write((byte)0x33);
            ns.Write((byte)0x03);
            ns.Write((byte)0xd3);
            ns.Write((byte)0xa3);
            ns.Write((byte)0x74);
            ns.Write((byte)0x44);
            ns.Write((byte)0x14);
            ns.Write((byte)0xe4);
            ns.Write((byte)0xb4);
            ns.Write((byte)0x85);
            ns.Write((byte)0x55);
            ns.Write((byte)0x25);
            ns.Write((byte)0xf5);
            ns.Write((byte)0xc5);
            ns.Write((byte)0x96);
            ns.Write((byte)0x66);
            ns.Write((byte)0x36);
            ns.Write((byte)0x06);
            ns.Write((byte)0xd6);
            ns.Write((byte)0xa7);
            ns.Write((byte)0x77);
            ns.Write((byte)0x47);
            ns.Write((byte)0x17);
            ns.Write((byte)0xe7);
            ns.Write((byte)0xb0);
            ns.Write((byte)0x80);
            ns.Write((byte)0x50);
            ns.Write((byte)0x20);
            ns.Write((byte)0xf0);
            ns.Write((byte)0xc1);
            ns.Write((byte)0x91);
            ns.Write((byte)0x61);
            ns.Write((byte)0x31);
            ns.Write((byte)0x01);
            ns.Write((byte)0xd2);
            ns.Write((byte)0xa2);
            ns.Write((byte)0x72);
            ns.Write((byte)0x42);
            ns.Write((byte)0x12);
            ns.Write((byte)0xe3);
            ns.Write((byte)0xb3);
            ns.Write((byte)0x83);
            ns.Write((byte)0x53);
            ns.Write((byte)0x23);
            ns.Write((byte)0xf4);
            ns.Write((byte)0xc4);
            ns.Write((byte)0x94);
            ns.Write((byte)0x64);
            ns.Write((byte)0x34);
            ns.Write((byte)0x05);
            ns.Write((byte)0xd5);
            ns.Write((byte)0xa5);
            ns.Write((byte)0x75);
            ns.Write((byte)0x45);
            ns.Write((byte)0x16);
            ns.Write((byte)0xe6);
            ns.Write((byte)0xb6);
            ns.Write((byte)0x86);
            ns.Write((byte)0x57);
            ns.Write((byte)0x26);
            ns.Write((byte)0xf7);
            ns.Write((byte)0xc6);
            ns.Write((byte)0x1b);//
            ns.Write((byte)0x2a);//
            ns.Write((byte)0xa7);//
            ns.Write((byte)0x64);//
            ns.Write((byte)0x7f);//
            ns.Write((byte)0x75);//
            ns.Write((byte)0x80);
            ns.Write((byte)0x51);
            ns.Write((byte)0x94);
            ns.Write((byte)0xc4);
            //ns.Write((short)0x08);
            //ns.Write((byte)0xdd);
            //ns.Write((byte)0x02);
            //ns.Write((short)0x00);
            //ns.Write((int)0x00);
            #endregion

        }
    }
    public sealed class NP_Client02 : NetPacket
    {
        public NP_Client02() : base(02, 0x0)
        {
            ns.Write((int)0x0);
        }
    }
    public sealed class NP_Client0200 : NetPacket
    {
        public NP_Client0200() : base(02, 0x0)
        {
            ns.Write((int)0x1);
        }
    }
    public sealed class NP_Client02002 : NetPacket
    {
        public NP_Client02002() : base(02, 0x0)
        {
            ns.Write((int)0x2);
        }
    }
    public sealed class NP_Client0212 : NetPacket
    {
        public NP_Client0212(int n1, int n2, int n3, int n4, int n5) : base(02, 0x13)
        {
            ns.Write((int)n1);
            ns.Write((int)n2);
            ns.Write((int)n3);
            ns.Write((int)n4);
            ns.Write((int)0x00);
            ns.Write((int)0x00);
            ns.Write((int)0x121eb478);
            ns.Write((int)0x43);
            ns.Write((int)n5);
            ns.Write((int)0x112b8cfb);
        }
    }
}
