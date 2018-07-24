using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace LocalCommons.Cryptography
{
    public static class Cryptography
    {
        /*
            1)
            Мне недавно приходилось сталкиваться с этим снова в моем собственном проекте,
            и мне хотелось поделиться более простым кодом, который я использовал, поскольку
            этот вопрос и серия ответов продолжались в моих поисках.
            Я не буду беспокоиться о проблемах безопасности, связанных с тем, как часто обновлять
            такие вещи, как « Соль» и « Инициализация вектора», - это тема для форума безопасности,
            и есть некоторые интересные ресурсы, на которые нужно обратить внимание.
            Это просто блок кода для реализации AesManaged в C #.
            Код очень прост в использовании. Это буквально просто требует следующего:

            string encrypted = Cryptography.Encrypt(data, "testpass");
            string decrypted = Cryptography.Decrypt(encrypted, "testpass");
            
            По умолчанию реализация использует AesManaged - но вы также можете вставить любой
            другой SymmetricAlgorithm . Список доступных наследователей SymmetricAlgorithm для
            .NET 4.5 можно найти по адресу:
            http://msdn.microsoft.com/en-us/library/system.security.cryptography.symmetricalgorithm.aspx
            На момент публикации этого сообщения текущий список включает:
            AesManaged
            RijndaelManaged
            DESCryptoServiceProvider
            RC2CryptoServiceProvider
            TripleDESCryptoServiceProvider
            Чтобы использовать RijndaelManaged с приведенным выше кодом, в качестве примера вы должны использовать:

            string encrypted = Cryptography.Encrypt<RijndaelManaged>(dataToEncrypt, password);
            string decrypted = Cryptography.Decrypt<RijndaelManaged>(encrypted, password);

            Надеюсь, это поможет кому-то там.
        */
        #region Settings
        private static int _iterations = 2;
        private static int _keySize = 256;
        private static string _hash = "SHA1";
        private static string _salt = "aselrias38490a32"; // Random
        private static string _vector = "8947az34awl34kjq"; // Random
        #endregion

        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="value"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string Encrypt(string value, string password)
        {
            return Encrypt<AesManaged>(value, password);
        }
        /// <summary>
        /// Encrypt
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string Encrypt<T>(string value, string password) where T : SymmetricAlgorithm, new()
        {
            byte[] vectorBytes = Encoding.ASCII.GetBytes(_vector);
            byte[] saltBytes = Encoding.ASCII.GetBytes(_salt);
            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            byte[] encrypted = null;
            using (T cipher = new T())
            {
                PasswordDeriveBytes _passwordBytes = new PasswordDeriveBytes(password, saltBytes, _hash, _iterations);
                byte[] keyBytes = _passwordBytes.GetBytes(_keySize / 8);
                cipher.Mode = CipherMode.CBC;
                using (ICryptoTransform encryptor = cipher.CreateEncryptor(keyBytes, vectorBytes))
                {
                    using (MemoryStream to = new MemoryStream())
                    {
                        using (CryptoStream writer = new CryptoStream(to, encryptor, CryptoStreamMode.Write))
                        {
                            writer.Write(valueBytes, 0, valueBytes.Length);
                            writer.FlushFinalBlock();
                            encrypted = to.ToArray();
                        }
                    }
                }
                cipher.Clear();
            }
            return Convert.ToBase64String(encrypted);
        }
        /// <summary>
        /// Decrypt
        /// </summary>
        /// <param name="value"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string Decrypt(string value, string password)
        {
            return Decrypt<AesManaged>(value, password);
        }
        /// <summary>
        /// Decrypt
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string Decrypt<T>(string value, string password) where T : SymmetricAlgorithm, new()
        {
            byte[] vectorBytes = Encoding.ASCII.GetBytes(_vector);
            byte[] saltBytes = Encoding.ASCII.GetBytes(_salt);
            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            byte[] decrypted = null;
            int decryptedByteCount = 0;
            using (T cipher = new T())
            {
                PasswordDeriveBytes _passwordBytes = new PasswordDeriveBytes(password, saltBytes, _hash, _iterations);
                byte[] keyBytes = _passwordBytes.GetBytes(_keySize / 8);
                cipher.Mode = CipherMode.CBC;
                try
                {
                    using (ICryptoTransform decryptor = cipher.CreateDecryptor(keyBytes, vectorBytes))
                    {
                        using (MemoryStream from = new MemoryStream(valueBytes))
                        {
                            using (CryptoStream reader = new CryptoStream(from, decryptor, CryptoStreamMode.Read))
                            {
                                decrypted = new byte[valueBytes.Length];
                                decryptedByteCount = reader.Read(decrypted, 0, decrypted.Length);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    return String.Empty;
                }

                cipher.Clear();
            }
            return Encoding.UTF8.GetString(decrypted, 0, decryptedByteCount);
        }
    }
}
