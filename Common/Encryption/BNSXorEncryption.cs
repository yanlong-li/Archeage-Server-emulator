using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaBNS.Common.Encryption
{
    public class BNSXorEncryption : SmartEngine.Network.Encryption
    {
        byte[] encKey, decKey;
        int encCounter, decCounter, encSum, decSum;
        public override SmartEngine.Network.Encryption Create()
        {
            return new BNSXorEncryption();
        }

        public override void Encrypt(byte[] src, int offset, int len)
        {
            if (encKey == null)
            {
                if (KeyExchange.IsReady)
                {
                    encKey = new byte[KeyExchange.Key.Length];
                    KeyExchange.Key.CopyTo(encKey, 0);
                    encCounter = 0;
                }
                else
                    return;
            }
            BlockEncrypt(src, encKey, ref encCounter, ref encSum);    
        }

        public override void Decrypt(byte[] src, int offset, int len)
        {
            if (decKey == null)
            {
                if (KeyExchange.IsReady)
                {
                    decKey = new byte[KeyExchange.Key.Length];
                    KeyExchange.Key.CopyTo(decKey, 0);
                    decCounter = 0;
                }
                else
                    return;
            }
            BlockEncrypt(src, decKey, ref decCounter, ref decSum);
        }

        byte[] BlockEncrypt(byte[] src, byte[] key, ref int counter, ref int sum)
        {
            for (int i = 0; i < src.Length; i++)
            {
                int index = (counter + 1) & 0xFF;
                counter = index;
                int v11 = (sum + key[index]) & 0xFF;
                sum = v11;
                int v12 = key[index];
                key[index] = key[v11];
                key[v11] = (byte)v12;
                src[i] = (byte)(src[i] ^ key[(key[sum] + key[counter]) & 0xFF]);
            }
            return src;
        }
    }
}
