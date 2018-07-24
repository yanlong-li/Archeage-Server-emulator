using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Windows;

namespace LocalCommons.Cryptography
{
    public class Crypt
    {
        /// <summary>
        /// вот аккуратный и чистый код для понимания алгоритма AES 256, реализованного в C #
        /// call Encrypt function as encryptedstring = cryptObj.Encrypt(username, "AGARAMUDHALA", "EZHUTHELLAM", "SHA1", 3, "@1B2c3D4e5F6g7H8", 256);
        /// </summary>
        /// <param name="passtext"></param>
        /// <param name="passPhrase"></param>
        /// <param name="saltV"></param>
        /// <param name="hashstring"></param>
        /// <param name="Iterations"></param>
        /// <param name="initVect"></param>
        /// <param name="keysize"></param>
        /// <returns></returns>
        public string Encrypt(string passtext, string passPhrase, string saltV, string hashstring, int Iterations, string initVect, int keysize)
        {
            string functionReturnValue = null;
            // Convert strings into byte arrays.
            // Let us assume that strings only contain ASCII codes.
            // If strings include Unicode characters, use Unicode, UTF7, or UTF8
            // encoding.
            byte[] initVectorBytes = null;
            initVectorBytes = Encoding.ASCII.GetBytes(initVect);
            byte[] saltValueBytes = null;
            saltValueBytes = Encoding.ASCII.GetBytes(saltV);

            // Convert our plaintext into a byte array.
            // Let us assume that plaintext contains UTF8-encoded characters.
            byte[] plainTextBytes = null;
            plainTextBytes = Encoding.UTF8.GetBytes(passtext);
            // First, we must create a password, from which the key will be derived.
            // This password will be generated from the specified passphrase and
            // salt value. The password will be created using the specified hash
            // algorithm. Password creation can be done in several iterations.
            PasswordDeriveBytes password = default(PasswordDeriveBytes);
            password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashstring, Iterations);
            // Use the password to generate pseudo-random bytes for the encryption
            // key. Specify the size of the key in bytes (instead of bits).
            byte[] keyBytes = null;
            keyBytes = password.GetBytes(keysize / 8);
            // Create uninitialized Rijndael encryption object.
            RijndaelManaged symmetricKey = default(RijndaelManaged);
            symmetricKey = new RijndaelManaged();

            // It is reasonable to set encryption mode to Cipher Block Chaining
            // (CBC). Use default options for other symmetric key parameters.
            symmetricKey.Mode = CipherMode.CBC;
            // Generate encryptor from the existing key bytes and initialization
            // vector. Key size will be defined based on the number of the key
            // bytes.
            ICryptoTransform encryptor = default(ICryptoTransform);
            encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);

            // Define memory stream which will be used to hold encrypted data.
            MemoryStream memoryStream = default(MemoryStream);
            memoryStream = new MemoryStream();

            // Define cryptographic stream (always use Write mode for encryption).
            CryptoStream cryptoStream = default(CryptoStream);
            cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            // Start encrypting.
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

            // Finish encrypting.
            cryptoStream.FlushFinalBlock();
            // Convert our encrypted data from a memory stream into a byte array.
            byte[] cipherTextBytes = null;
            cipherTextBytes = memoryStream.ToArray();

            // Close both streams.
            memoryStream.Close();
            cryptoStream.Close();

            // Convert encrypted data into a base64-encoded string.
            string cipherText = null;
            cipherText = Convert.ToBase64String(cipherTextBytes);

            functionReturnValue = cipherText;
            return functionReturnValue;
        }
        /// <summary>
        /// Decrypt
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="passPhrase"></param>
        /// <param name="saltValue"></param>
        /// <param name="hashAlgorithm"></param>
        /// <param name="passwordIterations"></param>
        /// <param name="initVector"></param>
        /// <param name="keySize"></param>
        /// <returns></returns>
        public string Decrypt(string cipherText, string passPhrase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector, int keySize)
        {
            string functionReturnValue = null;

            // Convert strings defining encryption key characteristics into byte
            // arrays. Let us assume that strings only contain ASCII codes.
            // If strings include Unicode characters, use Unicode, UTF7, or UTF8
            // encoding.
            byte[] initVectorBytes = null;
            initVectorBytes = Encoding.ASCII.GetBytes(initVector);

            byte[] saltValueBytes = null;
            saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

            // Convert our ciphertext into a byte array.
            byte[] cipherTextBytes = null;
            cipherTextBytes = Convert.FromBase64String(cipherText);

            // First, we must create a password, from which the key will be
            // derived. This password will be generated from the specified
            // passphrase and salt value. The password will be created using
            // the specified hash algorithm. Password creation can be done in
            // several iterations.
            PasswordDeriveBytes password = default(PasswordDeriveBytes);
            password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);

            // Use the password to generate pseudo-random bytes for the encryption
            // key. Specify the size of the key in bytes (instead of bits).
            byte[] keyBytes = null;
            keyBytes = password.GetBytes(keySize / 8);

            // Create uninitialized Rijndael encryption object.
            RijndaelManaged symmetricKey = default(RijndaelManaged);
            symmetricKey = new RijndaelManaged();

            // It is reasonable to set encryption mode to Cipher Block Chaining
            // (CBC). Use default options for other symmetric key parameters.
            symmetricKey.Mode = CipherMode.CBC;

            // Generate decryptor from the existing key bytes and initialization
            // vector. Key size will be defined based on the number of the key
            // bytes.
            ICryptoTransform decryptor = default(ICryptoTransform);
            decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);

            // Define memory stream which will be used to hold encrypted data.
            MemoryStream memoryStream = default(MemoryStream);
            memoryStream = new MemoryStream(cipherTextBytes);

            // Define memory stream which will be used to hold encrypted data.
            CryptoStream cryptoStream = default(CryptoStream);
            cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

            // Since at this point we don't know what the size of decrypted data
            // will be, allocate the buffer long enough to hold ciphertext;
            // plaintext is never longer than ciphertext.
            byte[] plainTextBytes = null;
            plainTextBytes = new byte[cipherTextBytes.Length + 1];

            // Start decrypting.
            int decryptedByteCount = 0;
            decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

            // Close both streams.
            memoryStream.Close();
            cryptoStream.Close();

            // Convert decrypted data into a string.
            // Let us assume that the original plaintext string was UTF8-encoded.
            string plainText = null;
            plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);

            // Return decrypted string.
            functionReturnValue = plainText;
            return functionReturnValue;
        }
        /// <summary>
        /// Code to encrypt Data :
        /// </summary>
        /// <param name="bytearraytoencrypt"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public byte[] encryptdata(byte[] bytearraytoencrypt, string key, string iv)
        {
            AesCryptoServiceProvider dataencrypt = new AesCryptoServiceProvider();

            //Block size : Gets or sets the block size, in bits, of the cryptographic operation.  
            dataencrypt.BlockSize = 128;

            //KeySize: Gets or sets the size, in bits, of the secret key  
            dataencrypt.KeySize = 128;

            //Key: Gets or sets the symmetric key that is used for encryption and decryption.  
            dataencrypt.Key = System.Text.Encoding.UTF8.GetBytes(key);

            //IV : Gets or sets the initialization vector (IV) for the symmetric algorithm  
            dataencrypt.IV = System.Text.Encoding.UTF8.GetBytes(iv);

            //Padding: Gets or sets the padding mode used in the symmetric algorithm  
            dataencrypt.Padding = PaddingMode.PKCS7;

            //Mode: Gets or sets the mode for operation of the symmetric algorithm  
            dataencrypt.Mode = CipherMode.CBC;

            //Creates a symmetric AES encryptor object using the current key and initialization vector (IV).  
            ICryptoTransform crypto1 = dataencrypt.CreateEncryptor(dataencrypt.Key, dataencrypt.IV);

            //TransformFinalBlock is a special function for transforming the last block or a partial block in the stream.   
            //It returns a new array that contains the remaining transformed bytes. A new array is returned, because the amount of   
            //information returned at the end might be larger than a single block when padding is added.  
            byte[] encrypteddata = crypto1.TransformFinalBlock(bytearraytoencrypt, 0, bytearraytoencrypt.Length);
            crypto1.Dispose();

            //return the encrypted data  
            return encrypteddata;
        }
        /// <summary>
        /// code to decrypt data
        /// </summary>
        /// <param name="bytearraytodecrypt"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        private byte[] decryptdata(byte[] bytearraytodecrypt, string key, string iv)
        {

            AesCryptoServiceProvider keydecrypt = new AesCryptoServiceProvider();
            keydecrypt.BlockSize = 128;
            keydecrypt.KeySize = 128;
            keydecrypt.Key = System.Text.Encoding.UTF8.GetBytes(key);
            keydecrypt.IV = System.Text.Encoding.UTF8.GetBytes(iv);
            keydecrypt.Padding = PaddingMode.PKCS7;
            keydecrypt.Mode = CipherMode.CBC;
            ICryptoTransform crypto1 = keydecrypt.CreateDecryptor(keydecrypt.Key, keydecrypt.IV);

            byte[] returnbytearray = crypto1.TransformFinalBlock(bytearraytodecrypt, 0, bytearraytodecrypt.Length);
            crypto1.Dispose();
            return returnbytearray;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class clsCrypto
    {
        private string _KEY = string.Empty;
        protected internal string KEY
        {
            get
            {
                return _KEY;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _KEY = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private string _IV = string.Empty;
        protected internal string IV
        {
            get
            {
                return _IV;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _IV = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strInput"></param>
        /// <returns></returns>
        private string CalcMD5(string strInput)
        {
            string strOutput = string.Empty;
            if (!string.IsNullOrEmpty(strInput))
            {
                try
                {
                    StringBuilder strHex = new StringBuilder();
                    using (MD5 md5 = MD5.Create())
                    {
                        byte[] bytArText = Encoding.Default.GetBytes(strInput);
                        byte[] bytArHash = md5.ComputeHash(bytArText);
                        for (int i = 0; i < bytArHash.Length; i++)
                        {
                            strHex.Append(bytArHash[i].ToString("X2"));
                        }
                        strOutput = strHex.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return strOutput;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strInput"></param>
        /// <returns></returns>
        private byte[] GetBytesFromHexString(string strInput)
        {
            byte[] bytArOutput = new byte[] { };
            if ((!string.IsNullOrEmpty(strInput)) && strInput.Length % 2 == 0)
            {
                SoapHexBinary hexBinary = null;
                try
                {
                    hexBinary = SoapHexBinary.Parse(strInput);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                bytArOutput = hexBinary.Value;
            }
            return bytArOutput;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private byte[] GenerateIV()
        {
            byte[] bytArOutput = new byte[] { };
            try
            {
                string strIV = CalcMD5(IV);
                bytArOutput = GetBytesFromHexString(strIV);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return bytArOutput;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private byte[] GenerateKey()
        {
            byte[] bytArOutput = new byte[] { };
            try
            {
                string strKey = CalcMD5(KEY);
                bytArOutput = GetBytesFromHexString(strKey);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return bytArOutput;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strInput"></param>
        /// <param name="cipherMode"></param>
        /// <returns></returns>
        protected internal string Encrypt(string strInput, CipherMode cipherMode)
        {
            string strOutput = string.Empty;
            if (!string.IsNullOrEmpty(strInput))
            {
                try
                {
                    byte[] bytePlainText = Encoding.Default.GetBytes(strInput);
                    using (RijndaelManaged rijManaged = new RijndaelManaged())
                    {
                        rijManaged.Mode = cipherMode;
                        rijManaged.BlockSize = 128;
                        rijManaged.KeySize = 128;
                        rijManaged.IV = GenerateIV();
                        rijManaged.Key = GenerateKey();
                        rijManaged.Padding = PaddingMode.Zeros;
                        ICryptoTransform icpoTransform = rijManaged.CreateEncryptor(rijManaged.Key, rijManaged.IV);
                        using (MemoryStream memStream = new MemoryStream())
                        {
                            using (CryptoStream cpoStream = new CryptoStream(memStream, icpoTransform, CryptoStreamMode.Write))
                            {
                                cpoStream.Write(bytePlainText, 0, bytePlainText.Length);
                                cpoStream.FlushFinalBlock();
                            }
                            strOutput = Encoding.Default.GetString(memStream.ToArray());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return strOutput;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strInput"></param>
        /// <param name="cipherMode"></param>
        /// <returns></returns>
        protected internal string Decrypt(string strInput, CipherMode cipherMode)
        {
            string strOutput = string.Empty;
            if (!string.IsNullOrEmpty(strInput))
            {
                try
                {
                    byte[] byteCipherText = Encoding.Default.GetBytes(strInput);
                    byte[] byteBuffer = new byte[strInput.Length];
                    using (RijndaelManaged rijManaged = new RijndaelManaged())
                    {
                        rijManaged.Mode = cipherMode;
                        rijManaged.BlockSize = 128;
                        rijManaged.KeySize = 128;
                        rijManaged.IV = GenerateIV();
                        rijManaged.Key = GenerateKey();
                        rijManaged.Padding = PaddingMode.Zeros;
                        ICryptoTransform icpoTransform = rijManaged.CreateDecryptor(rijManaged.Key, rijManaged.IV);
                        using (MemoryStream memStream = new MemoryStream(byteCipherText))
                        {
                            using (CryptoStream cpoStream = new CryptoStream(memStream, icpoTransform, CryptoStreamMode.Read))
                            {
                                cpoStream.Read(byteBuffer, 0, byteBuffer.Length);
                            }
                            strOutput = Encoding.Default.GetString(byteBuffer);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return strOutput;
        }

    }
}
