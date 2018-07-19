using System;
using System.Linq;
using System.Security.Cryptography;
/*
   Author:Sagara
*/
namespace Commons.Networking.Cryptography
{
    public class BdoTransformer
    {

        private static readonly byte[] TempKey =
        {
            0x41,
            0x88, 0x92, 0x80, 0x0e, 0xf0, 0x50, 0xe1, 0x27,
            0x5b, 0x40, 0x90, 0x37, 0x30, 0x38, 0xae, 0x5e,
            0x20, 0x4b, 0xef, 0x72, 0x4a, 0x83, 0x1c, 0xc7,
            0x7c, 0x2e, 0x89, 0x50, 0xad, 0x34, 0x33, 0x03,
            0xce, 0x62, 0xdb, 0x2a, 0x87, 0x96, 0x7b, 0xc8,
            0x16, 0x23, 0x42, 0x77, 0xd7, 0x5e, 0x95, 0x5c,
            0xf0, 0xc1, 0x5f, 0xa4, 0xdf, 0x91, 0x6a, 0x4d,
            0x69, 0x05, 0x60, 0xfe, 0x10, 0x5e, 0x9a, 0xed,
            0x44, 0x5a, 0x77, 0x00, 0x91, 0xc0, 0x94, 0x00,
            0x1b, 0x8b, 0x21, 0x04, 0x61, 0x46, 0x23, 0xb0,
            0xed, 0x1b, 0x9d, 0xa4, 0xcf, 0x04, 0xec, 0x73,
            0xa4, 0x9f, 0x6f, 0xc1, 0x1b, 0xcd, 0x7e, 0xe6,
            0xfc, 0xa0, 0x73, 0x81, 0x19, 0x84, 0x43, 0x77,
            0x7d, 0xb0, 0xbd, 0x6b, 0xdf, 0x34, 0x4d, 0x46,
            0x22, 0xa1, 0x6d, 0x59, 0xec, 0x23, 0x82, 0x22,
            0xdd
        };

        public byte[] TransformerContext
        {
            get { return TempKey; }
        }

        private readonly byte[] _sessionKey = new byte[80];
        private readonly byte[] _stateKey =
        {
            0x10, 0x1C, 0x36, 0x02, 0x01, 0x0, 0x0, 0x0,
            0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0,
            0x0, 0x0
        };
        public BdoTransformer(byte[] key, short opCode) 
            : this(BitConverter.GetBytes(opCode).Concat(key).ToArray())
        {
            
        }

        public BdoTransformer()
            : this(new byte[] { 0xeb, 0x03 }.Concat(TempKey).ToArray())
        {

        }

        private BdoTransformer(byte[] initKey)
        {
            var provider = new RijndaelManaged
            {
                Mode = CipherMode.CBC,
                KeySize = 128,
                BlockSize = 128,
                Padding = PaddingMode.None
            };

            using (provider)
            {
                byte[] key = GetAesKey(initKey);

                using (var transformer = provider.CreateDecryptor(key, new byte[16]))
                {
                    byte[] res = transformer.TransformFinalBlock(initKey, 84, 16);
                    Array.Copy(res, 0, _sessionKey, 0, 16);

                    res = transformer.TransformFinalBlock(initKey, 40, 16);
                    Array.Copy(res, 0, _sessionKey, 16, 16);

                    res = transformer.TransformFinalBlock(initKey, 16, 16);
                    Array.Copy(res, 0, _sessionKey, 32, 16);

                    res = transformer.TransformFinalBlock(initKey, 57, 16);
                    Array.Copy(res, 0, _sessionKey, 48, 16);

                    res = transformer.TransformFinalBlock(initKey, 104, 16);
                    Array.Copy(res, 0, _sessionKey, 64, 16);
                }
            }

            _stateKey[5] = _sessionKey[32];
            _stateKey[6] = _sessionKey[48];
            _stateKey[7] = _sessionKey[64];
            Array.Copy(_sessionKey, 16, _stateKey, 8, 11);
        }

        public void Transform(ref byte[] packetData)
        {
            int offSet = 1;
            int a3 = packetData.Length - offSet;
            int v3; // esi@1
            int v4; // ebp@2
            int v5; // edx@3
            byte result; // al@4

            v3 = 0;
            if (a3 > 0)
            {
                v4 = _stateKey[7];
                do
                {
                    v5 = v3++ % v4;
                    packetData[v3 - 1 + offSet] ^= _stateKey[v5 + 8];
                }
                while (v3 < a3);
            }
            ++_stateKey[7];
            result = _stateKey[7];
            if (_stateKey[6] <= result)
            {
                result = _stateKey[8];
                _stateKey[7] = _stateKey[5];
                _stateKey[8] = _stateKey[9];
                _stateKey[9] = _stateKey[10];
                _stateKey[10] = _stateKey[11];
                _stateKey[11] = _stateKey[12];
                _stateKey[12] = _stateKey[13];
                _stateKey[13] = _stateKey[14];
                _stateKey[14] = _stateKey[15];
                _stateKey[15] = _stateKey[16];
                _stateKey[16] = _stateKey[17];
                _stateKey[17] = _stateKey[18];
                _stateKey[18] = result;
            }
        }

        private byte[] GetAesKey(byte[] initialKey)
        {
            byte[] aeskey = new byte[16];
            /*signed*/
            int v2; // ebp@1
            byte v3 = 0; // eax@4
            /*unsigned __int8*/
            uint v4; // bl@20
            int v5; // esi@20
            byte v6; // al@22
            byte v7; // bl@22
            int v8; // al@23
            int v9; // dl@25
            int v10; // cl@26
            /* unsigned __int8 */
            uint v11; // al@29
            byte result; // al@31
            int v13; // cl@32
            byte v14; // dl@34
            byte v15; // al@34
            int v16; // cl@35
            /*signed */
            int v17; // [sp+Ch] [bp-4h]@1

            v2 = 0;
            v17 = 16;
            do
            {
                if ((uint)(v2 - 1) > 0xE)
                {
                    v3 = initialKey[15];
                }
                else
                {
                    switch (v2)
                    {
                        case 1:
                            v3 = initialKey[3];
                            break;
                        case 2:
                            v3 = initialKey[77];
                            break;
                        case 3:
                            v3 = initialKey[74];
                            break;
                        case 4:
                            v3 = initialKey[56];
                            break;
                        case 5:
                            v3 = initialKey[8];
                            break;
                        case 6:
                            v3 = initialKey[34];
                            break;
                        case 7:
                            v3 = initialKey[6];
                            break;
                        case 8:
                            v3 = initialKey[5];
                            break;
                        case 9:
                            v3 = initialKey[38];
                            break;
                        case 10:
                            v3 = initialKey[121];
                            break;
                        case 11:
                            v3 = initialKey[82];
                            break;
                        case 12:
                            v3 = initialKey[12];
                            break;
                        case 13:
                            v3 = initialKey[100];
                            break;
                        case 14:
                            v3 = initialKey[13];
                            break;
                        case 15:
                            v3 = initialKey[39];
                            break;
                    }
                }
                v4 = v3;
                v5 = v2 % 3;
                if (v5 != 0)
                {
                    if (v2 % 3 == 1)
                    {
                        v8 = v2 % 8;
                        if (v8 < 1)
                            v8 = 1;
                        v9 = v8;
                        v6 = (byte)(v4 << v8);
                        v7 = (byte)(v4 >> (8 - v9));
                    }
                    else
                    {
                        v6 = (byte)(v4 >> 4);
                        v7 = (byte)(16 * v4);
                    }
                }
                else
                {
                    v10 = v2 % 8;
                    if (v10 < 1)
                        v10 = 1;
                    v6 = (byte)(v4 >> v10);
                    v7 = (byte)(v4 << (8 - v10));
                }
                v11 = sub_B8C620(v6 | v7, initialKey);
                if (v5 != 0)
                {
                    if (v5 != 1)
                    {
                        result = (byte)((v11 >> 4) | 16 * v11);
                        aeskey[v2++] = result;
                        --v17;
                        continue;
                        // goto LABEL_39;
                    }
                    v13 = v2 % 8;
                    if (v13 < 1)
                        v13 = 1;
                    v14 = (byte)(v11 << v13);
                    v15 = (byte)(v11 >> (8 - v13));
                }
                else
                {
                    v16 = v2 % 8;
                    if (v16 < 1)
                        v16 = 1;
                    v14 = (byte)(v11 >> v16);
                    v15 = (byte)(v11 << (8 - v16));
                }
                result = (byte)(v14 | v15);
                //LABEL_39:
                aeskey[v2++] = result;
                --v17;
            }
            while (v17 > 0);
            return aeskey;
        }

        public byte sub_B8C620(int a2, byte[] initialKey)
        {
            byte result; // eax@2

            switch (a2)
            {
                case 1:
                    result = initialKey[80];
                    break;
                case 2:
                    result = initialKey[123];
                    break;
                case 3:
                    result = initialKey[9];
                    break;
                case 4:
                    result = initialKey[37];
                    break;
                case 5:
                    result = initialKey[10];
                    break;
                case 6:
                    result = initialKey[14];
                    break;
                case 7:
                    result = initialKey[32];
                    break;
                case 8:
                    result = initialKey[35];
                    break;
                case 9:
                    result = initialKey[73];
                    break;
                case 0xA:
                    result = initialKey[76];
                    break;
                case 0xB:
                    result = initialKey[11];
                    break;
                case 0xC:
                    result = initialKey[101];
                    break;
                case 0xD:
                    result = initialKey[2];
                    break;
                case 0xE:
                    result = initialKey[7];
                    break;
                case 0xF:
                    result = initialKey[33];
                    break;
                case 0x10:
                    result = initialKey[75];
                    break;
                case 0x11:
                    result = initialKey[103];
                    break;
                case 0x12:
                    result = initialKey[78];
                    break;
                case 0x13:
                    result = initialKey[36];
                    break;
                case 0x14:
                    result = initialKey[79];
                    break;
                case 0x15:
                    result = initialKey[120];
                    break;
                case 0x16:
                    result = initialKey[83];
                    break;
                case 0x17:
                    result = initialKey[102];
                    break;
                case 0x18:
                    result = initialKey[122];
                    break;
                case 0x19:
                    result = initialKey[81];
                    break;
                default:
                    result = initialKey[4];
                    break;
            }
            return result;
        }
    }
}
