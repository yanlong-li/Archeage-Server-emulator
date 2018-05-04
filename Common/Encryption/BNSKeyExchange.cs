using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using Mono.Math;

using SmartEngine.Network;

namespace SagaBNS.Common.Encryption
{
    public unsafe class BNSKeyExchange : EncryptionKeyExchange
    {
        public static BigInteger N = new BigInteger("E306EBC02F1DC69F5B437683FE3851FD9AAA6E97F4CBD42FC06C72053CBCED68EC570E6666F529C58518CF7B299B5582495DB169ADF48ECEB6D65461B4D7C75DD1DA89601D5C498EE48BB950E2D8D5E0E0C692D613483B38D381EA9674DF74D67665259C4C31A29E0B3CFF7587617260E8C58FFA0AF8339CD68DB3ADB90AAFEE");
        public static BigInteger P = new BigInteger("7A39FF57BCBFAA521DCE9C7DEFAB520640AC493E1B6024B95A28390E8F05787D");
        public static byte[] staticKey = Conversions.HexStr2Bytes("AC34F3070DC0E52302C2E8DA0E3F7B3E63223697555DF54E7122A14DBC99A3E8");
        public static BigInteger Two = new BigInteger(2);
        BigInteger privateKey;
        BigInteger exchangeKey = Two;
        BigInteger exchangeKeyServer;
        SHA256 sha = SHA256.Create();
        BigInteger session = new BigInteger((ulong)Global.Random.Next() << 32 | (uint)Global.Random.Next());
        byte[] passwordHash, usernameHash;
        string user, password, authentication;

        /// <summary>
        /// username
        /// </summary>
        public string Username { get { return user; } set { user = value; } }

        /// <summary>
        /// password
        /// </summary>
        public string Password { get { return password; } set { password = value; } }

        /// <summary>
        /// Verification code (including validating username, password)
        /// </summary>
        public string Authentication { get { return authentication; } }

        /// <summary>
        /// Conversation ID
        /// </summary>
        public BigInteger Session { get { return session; } set { session = value; } }

        public override EncryptionKeyExchange CreateNewInstance()
        {
            return new BNSKeyExchange();
        }

        public override void MakePrivateKey()
        {
            privateKey = new BigInteger(sha.ComputeHash(Encoding.ASCII.GetBytes(DateTime.Now.Ticks.ToString())));
        }

        BigInteger GetKeyExchange()
        {
            if (exchangeKey == Two)
                exchangeKey = Two.modPow(privateKey, N);
            return exchangeKey;
        }

        public BigInteger GetKeyExchangeClient()
        {
            return GetKeyExchange();
        }

        BigInteger GetKeyExchangeServer()
        {
            if (exchangeKey == Two)
            {
                try
                {
                    exchangeKey = Two.modPow(privateKey, N);
                    string username = user + "@plaync.co.kr";
                    passwordHash = sha.ComputeHash(Encoding.ASCII.GetBytes(username + ":" + password));
                    usernameHash = sha.ComputeHash(Encoding.ASCII.GetBytes(username));
                    BigInteger hash2 = SHA256Hash2ArrayInverse(session.getBytes(), passwordHash);
                    BigInteger v25 = Two.modPow(hash2, N);
                    v25 = (v25 * P) % N;
                    exchangeKeyServer = (exchangeKey + v25) % N;
                }
                catch
                {
                }
            }
            return exchangeKeyServer;
        }

        public override byte[] GetKeyExchangeBytes(Mode mode)
        {
            switch (mode)
            {
                case Mode.Client:
                    return GetKeyExchangeClient().getBytes();
                case Mode.Server:
                    return GetKeyExchangeServer().getBytes();
            }
            return null;
        }

        public byte[][] GenerateKeyClient(BigInteger exchangeKey)
        {
            string username = user + "@plaync.co.kr";
            byte[] passwordHash = sha.ComputeHash(Encoding.ASCII.GetBytes(username + ":" + password));

            BigInteger hash1 = SHA256Hash2ArrayInverse(this.GetKeyExchange().getBytes(), exchangeKey.getBytes());

            BigInteger hash2 = SHA256Hash2ArrayInverse(session.getBytes(), passwordHash);

            BigInteger v27 = new BigInteger(exchangeKey.getBytes());
            BigInteger v25 = Two.modPow(hash2, N);
            v25 = (v25 * P) % N;
            while (v27 < v25)
                v27 += N;
            v27 -= v25;

            BigInteger v24 = ((hash1 * hash2) + privateKey) % N;
            BigInteger v21 = v27.modPow(v24, N);

            key = GenerateEncryptionKeyRoot(v21.getBytes());
            byte[] chash1 = sha.ComputeHash(CombineBuffers(staticKey, sha.ComputeHash(Encoding.ASCII.GetBytes(username)), session.getBytes(), this.GetKeyExchange().getBytes(), exchangeKey.getBytes(), key));
            byte[] chash2 = sha.ComputeHash(CombineBuffers(this.GetKeyExchange().getBytes(), chash1, key));
            key = Generate256BytesKey(key);
            
            return new byte[][] { chash1, chash2 };
        }

        public byte[][] GenerateKeyServer(BigInteger exchangeKey)
        {
            BigInteger hash1 = SHA256Hash2ArrayInverse(exchangeKey.getBytes(), this.GetKeyExchangeServer().getBytes());

            BigInteger hash2 = SHA256Hash2ArrayInverse(session.getBytes(), passwordHash);

            BigInteger v27 = new BigInteger(exchangeKey.getBytes());

            BigInteger v21 = (this.GetKeyExchange().modPow((hash1 * hash2), N) * v27.modPow(privateKey, N)) % N;
            key = GenerateEncryptionKeyRoot(v21.getBytes());
           
            byte[] chash1 = sha.ComputeHash(CombineBuffers(staticKey, usernameHash, session.getBytes(), exchangeKey.getBytes(), GetKeyExchangeServer().getBytes(), key));
            byte[] chash2 = sha.ComputeHash(CombineBuffers(exchangeKey.getBytes(), chash1, key));
            key = Generate256BytesKey(key);

            return new byte[][] { chash1, chash2 };
        }

        public override void MakeKey(Mode mode, byte[] keyExchangeBytes)
        {
            byte[][] checkHash = null;
            BigInteger exchange = new BigInteger(keyExchangeBytes);
            switch (mode)
            {
                case Mode.Client:
                    checkHash = GenerateKeyClient(exchange);
                    break;
                case Mode.Server:
                    checkHash = GenerateKeyServer(exchange);
                    break;
            }
            authentication = Convert.ToBase64String(checkHash[0]) + "," + Convert.ToBase64String(checkHash[1]);
        }

        public override bool IsReady
        {
            get { return key != null; }
        }

        BigInteger SHA256Hash2ArrayInverse(byte[] tmp1, byte[] tmp2)
        {
            BigInteger hash;
            byte[] combine = new byte[tmp1.Length + tmp2.Length];
            tmp1.CopyTo(combine, 0);
            tmp2.CopyTo(combine, tmp1.Length);
            byte[] buf = sha.ComputeHash(combine);
            byte[] res = IntegerReverse(buf);
            hash = new BigInteger(res);
            return hash;
        }

        byte[] IntegerReverse(byte[] buf)
        {
            byte[] res = new byte[buf.Length];
            for (int i = 0; i < res.Length / 4; i++)
            {
                fixed (byte* ptr = buf)
                {
                    fixed (byte* ptr2 = res)
                    {
                        int* src = (int*)ptr;
                        int* dst = (int*)ptr2;
                        dst[i] = src[res.Length / 4 - 1 - i];
                    }
                }
            }

            return res;
        }

        byte[] GenerateEncryptionKeyRoot(byte[] src)
        {
            int firstSize = src.Length;
            int startIndex = 0;
            byte[] half;
            byte[] dst = new byte[64];
            if (src.Length > 4)
            {
                do
                {
                    if (src[startIndex] == 0)
                        break;
                    firstSize--;
                    startIndex++;
                } while (firstSize > 4);

            }
            int size = firstSize >> 1;
            half = new byte[size];
            if (size > 0)
            {
                int index = startIndex + firstSize - 1;
                for (int i = 0; i < size; i++)
                {
                    half[i] = src[index];
                    index -= 2;
                }
            }
            byte[] hash = sha.ComputeHash(half, 0, size);
            for (int i = 0; i < 32; i++)
            {
                dst[2 * i] = hash[i];
            }
            if (size > 0)
            {
                int index = startIndex + firstSize - 2;
                for (int i = 0; i < size; i++)
                {
                    half[i] = src[index];
                    index -= 2;
                }
            }
            hash = sha.ComputeHash(half, 0, size);
            for (int i = 0; i < 32; i++)
            {
                dst[2 * i + 1] = hash[i];
            }
            return dst;
        }

        byte[] CombineBuffers(params byte[][] buffers)
        {
            int len = 0;
            foreach (byte[] i in buffers)
            {
                len += i.Length;
            }
            byte[] res = new byte[len];
            int index = 0;
            foreach (byte[] i in buffers)
            {
                i.CopyTo(res, index);
                index += i.Length;
            }
            return res;
        }

        byte[] Generate256BytesKey(byte[] src)
        {
            int v7 = 1;
            byte[] res = new byte[256];
            for (int i = 0; i < 256; i++)
                res[i] = (byte)i;
            int v6 = 0;
            int counter = 0;
            for (int i = 64; i > 0; i--)
            {
                int v9 = (v6 + src[counter] + res[v7 - 1]) & 0xFF;
                int v10 = res[v7 - 1];
                res[v7 - 1] = res[v9];
                int v8 = counter + 1;
                res[v9] = (byte)v10;
                if (v8 == src.Length)
                    v8 = 0;
                int v13 = v9 + src[v8];
                int v11 = v8 + 1;
                int v14 = v13 + res[v7];
                v13 = res[v7];
                int v12 = (byte)v14;
                res[v7] = res[v12];
                res[v12] = (byte)v13;
                if (v11 == src.Length)
                    v11 = 0;
                int v16 = (v12 + src[v11] + res[v7 + 1]) & 0xFF;
                int v17 = res[v7 + 1];
                res[v7 + 1] = res[v16];
                int v15 = v11 + 1;
                res[v16] = (byte)v17;
                if (v15 == src.Length)
                    v15 = 0;
                int v18 = v16 + src[v15];
                int v19 = res[v7 + 2];
                v6 = (v18 + res[v7 + 2]) & 0xFF;
                counter = v15 + 1;
                res[v7 + 2] = res[v6];
                res[v6] = (byte)v19;
                if (counter == src.Length)
                    counter = 0;
                v7 += 4;
            }
            return res;
        }
    }
}
