using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using SmartEngine.Network.Utils;

namespace SmartEngine.Network
{
    public abstract class EncryptionKeyExchange
    {
        public static BigInteger Module = new BigInteger(Conversions.HexStr2Bytes("f488fd584e49dbcd20b49de49107366b336c380d451d0f7c88b31c7c5b2d8ef6f3c923c043f0a55b188d8ebb558cb85d38d334fd7c175743a31d186cde33212cb52aff3ce1b1294018118d7c84a70a72d686c40319c807297aca950cd9969fabd00a509b0246d3083d66a45d419f9c7cbd894b221926baaba25ec355e92f78c7"));

        /// <summary>
        /// Exchanged encryption and decryption keys
        /// </summary>
        protected byte[] key;

        /// <summary>
        /// Exchanged encryption and decryption keys
        /// </summary>
        public byte[] Key { get { return key; } }

        public abstract EncryptionKeyExchange CreateNewInstance();

        /// <summary>
        /// Generate private key
        /// </summary>
        public abstract void MakePrivateKey();

        /// <summary>
        /// Generate key exchange data
        /// </summary>
        /// <returns></returns>
        public abstract byte[] GetKeyExchangeBytes(Mode mode);

        /// <summary>
        /// Generate final key based on public and private keys
        /// </summary>
        /// <param name="keyExchangeBytes"></param>
        /// <param name="mode">Whether the currently generated key is a client or a server</param>
        public abstract void MakeKey(Mode mode, byte[] keyExchangeBytes);

        /// <summary>
        /// Is the encryption algorithm ready?
        /// </summary>
        public abstract bool IsReady
        {
            get;
        }
    }
}
