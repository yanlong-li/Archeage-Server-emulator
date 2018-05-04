using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using SmartEngine.Network.Utils;
using System.Security.Cryptography;

namespace SmartEngine.Network
{
    public class DefaultEncryptionKeyExchange : EncryptionKeyExchange
    {
        static BigInteger Two = new BigInteger((uint)2);
        static BigInteger Module = new BigInteger(Conversions.HexStr2Bytes("f488fd584e49dbcd20b49de49107366b336c380d451d0f7c88b31c7c5b2d8ef6f3c923c043f0a55b188d8ebb558cb85d38d334fd7c175743a31d186cde33212cb52aff3ce1b1294018118d7c84a70a72d686c40319c807297aca950cd9969fabd00a509b0246d3083d66a45d419f9c7cbd894b221926baaba25ec355e92f78c7"));
        BigInteger privateKey = Two;

        /// <summary>
        /// Exchanged encryption and decryption keys
        /// </summary>
        public byte[] Key { get { return key; } }

        public override EncryptionKeyExchange CreateNewInstance()
        {
            return new DefaultEncryptionKeyExchange();
        }
        
        public override void MakePrivateKey()
        {
            SHA1 sha = SHA1.Create();
            byte[] tmp = new byte[40];
            sha.TransformBlock(System.Text.Encoding.ASCII.GetBytes(DateTime.Now.ToString() + DateTime.Now.ToUniversalTime() + DateTime.Now.ToLongDateString()), 0, 40, tmp, 0);
            privateKey = new BigInteger(tmp);
        }

        public override byte[] GetKeyExchangeBytes(Mode mode)
        {
            if (privateKey == Two)
                return null;
            byte[] res = BigInteger.ModPow(Two, privateKey, Module).ToByteArray();
            return res;
        }

        public override void MakeKey(Mode mode, byte[] keyExchangeBytes)
        {
            BigInteger A = new BigInteger(keyExchangeBytes);
            byte[] R = BigInteger.ModPow(A, privateKey, Module).ToByteArray();
            key = new byte[16];
            Array.Copy(R, key, 16);
            for (int i = 0; i < 16; i++)
            {
                byte tmp = (byte)(key[i] >> 4);
                byte tmp2 = (byte)(key[i] & 0xF);
                if (tmp > 9)
                    tmp = (byte)(tmp - 9);
                if (tmp2 > 9)
                    tmp2 = (byte)(tmp2 - 9);
                key[i] = (byte)(tmp << 4 | tmp2);
            }
        }

        public override bool IsReady
        {
            get { return key != null; }
        }
    }
}
