using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

using System.Numerics;
using SmartEngine.Network.Utils;

namespace SmartEngine.Network
{
    /// <summary>
    /// An encryption abstraction class that uses RSA-like algorithms to exchange keys must be inherited before it
    /// can be used
    /// </summary>
    public abstract class Encryption
    {
        static Encryption impl = new AESEncryption();
        static EncryptionKeyExchange exchange = new DefaultEncryptionKeyExchange();
        EncryptionKeyExchange keyExchange;
        public EncryptionKeyExchange KeyExchange
        {
            get
            {
                if (keyExchange == null)
                    keyExchange = exchange.CreateNewInstance();
                return keyExchange;
            }
        }

        /// <summary>
        /// The specific implementation used for encryption, which defaults to AES encryption
        /// </summary>
        public static Encryption Implementation { get { return impl; } set { impl = value; } }

        public static EncryptionKeyExchange KeyExchangeImplementation { get { return exchange; } set { exchange = value; } }

        /// <summary>
        /// Create new instance
        /// </summary>
        /// <returns></returns>
        public abstract Encryption Create();

        /// <summary>
        /// Encrypted cache
        /// </summary>
        /// <param name="src">Buffer area</param>
        /// <param name="offset">Start encryption offset</param>
        public abstract void Encrypt(byte[] src, int offset, int len);

        /// <summary>
        /// Decrypt the cache
        /// </summary>
        /// <param name="src">Buffer area</param>
        /// <param name="offset">Offset to start decryption</param>
        public abstract void Decrypt(byte[] src, int offset, int len);
    }
}
