using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace SmartEngine.Network
{
    /// <summary>
    /// Encryption algorithm using AES algorithm
    /// </summary>
    class AESEncryption : Encryption
    {
        Rijndael aes;
        ICryptoTransform enc;
        ICryptoTransform dec;
        public AESEncryption()
        {
            aes = new RijndaelManaged();
            aes.Mode = CipherMode.CBC;
            aes.KeySize = 128;
            aes.BlockSize = 128;
            aes.Padding = PaddingMode.PKCS7;
        }

        public override Encryption Create()
        {
            return new AESEncryption();
        }

        public override void Encrypt(byte[] src, int offset, int len)
        {
            if (this.KeyExchange.Key == null) return;
            if (offset == src.Length) return;
            enc = aes.CreateEncryptor(this.KeyExchange.Key, new byte[16]);
            enc.TransformBlock(src, offset, len, src, offset);
        }

        public override void Decrypt(byte[] src, int offset, int len)
        {
            if (this.KeyExchange.Key == null) return;
            if (offset == src.Length) return;
            byte[] buf = new byte[len + 16];//more 16 bytes to ensure it decrypts completely
            dec = aes.CreateDecryptor(this.KeyExchange.Key, new byte[16]);
            src.CopyTo(buf, 0);
            dec.TransformBlock(buf, offset, len + 16, buf, offset);
            Array.Copy(buf, offset, src, offset, len); 
        }
    }

}
