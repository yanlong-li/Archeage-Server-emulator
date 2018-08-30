using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace LocalCommons.Cryptography
{
    public class Encrypt
    {
        /// <summary>
        /// Подсчет контрольной суммы пакета, используется в шифровании пакетов DD05 и 0005
        /// </summary>
        public static byte Crc8(byte[] data, int size)
        {
            var len = size;
            uint checksum = 0;
            for (var i = 0; i <= len - 1; i++)
            {
                checksum *= 0x13;
                checksum += data[i];
            }
            return (byte)(checksum);
        }
        public static byte Crc8(byte[] data)
        {
            var len = data.Length;
            uint checksum = 0;
            for (var i = 0; i <= len - 1; i++)
            {
                checksum = checksum * 0x13;
                checksum += data[i];
            }
            return (byte)(checksum);
        }
        //--------------------------------------------------------------------------------------
        /// <summary>
        /// вспомогательная подпрограмма для encode/decode серверных/клиентских пакетов
        /// </summary>
        /// <param name="cry"></param>
        /// <returns></returns>
        private static byte Inline(ref uint cry)
        {
            cry += 0x2FCBD5U;
            var n = (byte)(cry >> 0x10);
            n = (byte)(n & 0x0F7);
            return (byte)(n == 0 ? 0x0FE : n);
        }
        //--------------------------------------------------------------------------------------
        /// <summary>
        /// подпрограмма для encode/decode серверных пакетов, правильно шифрует и расшифровывает серверные пакеты DD05 для версии 3.0.3.0
        /// </summary>
        /// <param name="bodyPacket">адрес начиная с байта за DD05</param>
        /// <returns>возвращает адрес на подготовленные данные</returns>
        public static byte[] StoCEncrypt(byte[] bodyPacket)
        {
            var array = new byte[bodyPacket.Length];
            var cry = (uint)(bodyPacket.Length ^ 0x1F2175A0);
            var n = 4 * (bodyPacket.Length / 4);
            for (var i = n - 1; i >= 0; i--)
            {
                array[i] = (byte)(bodyPacket[i] ^ (uint)Inline(ref cry));
            }

            for (var i = n; i < bodyPacket.Length; i++)
            {
                array[i] = (byte)(bodyPacket[i] ^ (uint)Inline(ref cry));
            }

            return array;
        }
        //--------------------------------------------------------------------------------------
        //internal static uint XorKey; // XorKey = XorKey * XorKey & 0xffffffff; TODO: найти откуда берется!!!;
        internal static int Num = -1;
        //--------------------------------------------------------------------------------------
        /// <summary>
        /// DeXORing packet
        /// </summary>
        /// <param name="bodyPacket">packet data, starting after message-key byte</param>
        /// <param name="msgKey">unique key for each message</param>
        /// <param name="xorKey">xor key </param>
        /// <param name="offset">xor decryption can start from some offset (don't know the rule yet)</param>
        /// <returns>xor decrypted packet</returns>
        private static byte[] DecryptXor(byte[] bodyPacket, uint msgKey, uint xorKey, int offset = 0)
        {
            var length = bodyPacket.Length;
            var array = new byte[length];

            var mul = xorKey * msgKey;
            var key = (0x75a024a4 ^ mul) ^ 0xC3903b6a;
            var n = 4 * (length / 4);
            for (var i = n - offset - 1; i >= 0; i--)
            {
                array[i] = (byte)(bodyPacket[i] ^ (uint)Inline(ref key));
            }
            for (var i = n - offset; i < length; i++)
            {
                array[i] = (byte)(bodyPacket[i] ^ (uint)Inline(ref key));
            }
            return array;
        }
        //--------------------------------------------------------------------------------------
        public static byte[] CtoSEncrypt(byte[] bodyPacket, uint xorKey)
        {
            uint msgKey = 0;
            var length = bodyPacket.Length;
            var mBodyPacket = new byte[length - 5];
            Buffer.BlockCopy(bodyPacket, 5, mBodyPacket, 0, length - 5);
            byte[] packet = new byte[mBodyPacket.Length];

            int caseSwitch = bodyPacket[4];
            switch (caseSwitch)
            {
                case 0x30: //вроде бы, нас интересует только первая цифра
                    msgKey = 0x01; //0X11; //0x2F
                    break;
                case 0x31:
                    msgKey = 0x02; //0x02; //? нет
                    break;
                case 0x32:
                    msgKey = 0x03; //0x03; //? нет
                    break;
                case 0x33:
                    msgKey = 0x04; //0x04; //да, проверено
                    break;
                case 0x34:
                    msgKey = 0x05; //0x15; //? есть
                    break;
                case 0x35:
                    msgKey = 0x06; //0x16; //да, проверено
                    break;
                case 0x36:
                    msgKey = 0x07; //0x27; //да, проверено 0x17 - на выходе из игры, 0x07
                    break;
                case 0x37:
                    msgKey = 0x08; //0x08; //да, проверено 0x0D88 - на выходе из игры
                    break;
                case 0x38:
                    msgKey = 0x09; //
                    break;
                case 0x39:
                    msgKey = 0x0A; //да, проверено
                    break;
                case 0x3A:
                    msgKey = 0x0b; //0x1B; //? нет
                    break;
                case 0x3B:
                    msgKey = 0x0c; //0x1C; //
                    break;
                case 0x3C:
                    msgKey = 0x0d; //0x0D; //да, проверено
                    break;
                case 0x3D:
                    msgKey = 0x0e; //0x1E; //? нет
                    break;
                case 0x3E:
                    msgKey = 0x0f; //0x1F; //
                    break;
                case 0x3F:
                    msgKey = 0x10; //0x10; //да, проверено
                    break;
            }
            Num += 1; //глобальный подсчет клиентских пакетов
            // Hardcoded offset rules
            switch (Num)
            {
                case 0:
                    packet = DecryptXor(mBodyPacket, xorKey, msgKey, 7);
                    break;
                case 1:
                case 5:
                case 6:
                case 7:
                case 9:
                case 19:
                case 21:
                case 28:
                case 29:
                case 30:
                case 34:
                case 35:
                case 46:
                case 48:
                case 50:
                case 52:
                    packet = DecryptXor(mBodyPacket, xorKey, msgKey, 1);
                    break;
                case 15:
                case 41:
                    packet = DecryptXor(mBodyPacket, xorKey, msgKey, 5);
                    break;
                case 16:
                case 25:
                case 31:
                case 37:
                case 44:
                case 51:
                    packet = DecryptXor(mBodyPacket, xorKey, msgKey, 2);
                    break;
                //case ??:
                //    packet = Decryptxor(mBodyPacket, xorKey, msgKey, 3);
                //    break;
                default:
                    packet = DecryptXor(mBodyPacket, xorKey, msgKey);
                    break;
            }
            return packet;
        }

        /*
         * У вас есть два варианта (и здесь я описываю только процесс шифрования, но дешифрование аналогично):
        Использовать потоковый шифр (например, AES-CTR)
        Вы инициализируете шифр с 16-байтовым ключом и по-настоящему случайным 16-байтным nonce, записываете nonce,
        загружаете первую часть, шифруете ее, записываете результат, загружаете вторую часть и так далее.
        Обратите внимание, что вы должны инициализировать шифр только один раз. Размер куска может быть произвольным;
        это даже не обязательно должно быть одинаковым каждый раз.
        
        Используйте блок-шифр с режимом цепочки с одним проходом, например AES128-CBC
        Вы инициализируете шифр с 16-байтовым ключом, генерируете случайный 16-байтовый IV, записываете IV, записываете общую длину файла,
        загружаете первую часть, шифруете ее вместе с IV, записываете результат, загружаете второй фрагмент, зашифруйте с использованием
        последних 16 байтов предыдущего зашифрованного блока как IV, запишите результат и т.д.
        Размер фрагмента должен быть кратным 16 байтам; опять же, это не обязательно должно быть одинаковым каждый раз.
        Возможно, вам понадобится заполнить последний блок нулями.
        
        В обоих случаях
        Вы должны вычислить криптографический хеш исходного незашифрованного файла (например, используя SHA-256) и записать его,
        когда шифрование завершено. Это довольно просто: вы инициализируете хеш в самом начале и загружаете каждый блок в него,
        как только он загружается (в том числе nonce/IV и, возможно, поле длины). На стороне расшифровки вы делаете то же самое.
        В конце концов, вы должны убедиться, что вычисленный дайджест соответствует тому, который пришел с зашифрованным файлом.
        EDIT: nonce/IV и длина также хэшируются.
         */

        private const int Size = 16;

        private static RijndaelManaged GetRijndaelManaged(byte[] key, byte[] iv)
        {
            var rm = new RijndaelManaged
            {
                KeySize = 128,
                BlockSize = 128,
                Padding = PaddingMode.None,
                Mode = CipherMode.CBC
            };
            rm.Key = key;
            rm.IV = iv;

            return rm;
        }
        public static byte[] DecryptAes(byte[] cipherData, byte[] key, byte[] iv)
        {
            byte[] mIv = new byte[16];
            Buffer.BlockCopy(iv, 0, mIv, 0, Size);
            var len = cipherData.Length / Size;
            Buffer.BlockCopy(cipherData, (len - 1) * Size, iv, 0, Size);
            //Buffer.BlockCopy(cipherData, 0, iv, 0, Size);
            // Create a MemoryStream that is going to accept the decrypted bytes
            using (var memoryStream = new MemoryStream())
            {
                // Create a symmetric algorithm.
                // We are going to use RijndaelRijndael because it is strong and available on all platforms.
                // You can use other algorithms, to do so substitute the next line with something like
                // TripleDES alg = TripleDES.Create();
                using (var alg = GetRijndaelManaged(key, mIv))
                {
                    // Create a CryptoStream through which we are going to be pumping our data.
                    // CryptoStreamMode.Write means that we are going to be writing data to the stream
                    // and the output will be written in the MemoryStream we have provided.
                    using (var cs = new CryptoStream(memoryStream, alg.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        // Write the data and make it do the decryption
                        cs.Write(cipherData, 0, cipherData.Length);

                        // Close the crypto stream (or do FlushFinalBlock).
                        // This will tell it that we have done our decryption and there is no more data coming in,
                        // and it is now a good time to remove the padding and finalize the decryption process.
                        cs.FlushFinalBlock();
                        cs.Close();
                    }
                }
                // Now get the decrypted data from the MemoryStream.
                // Some people make a mistake of using GetBuffer() here, which is not the right way.
                var decryptedData = memoryStream.ToArray();
                return decryptedData;
            }
        }
    }

    /*
    В целом, классический алгоритм Диффи-Хеллмана выглядит так:

    Обе стороны выбирают одинаковое больше целое число.
    Обе стороны выбирают, так называемый, генератор (обычно AES), который будет использоваться для вычислений.
    Независимо друг от друга, каждая сторона выбирает еще одно число, которое хранится в секрете.
    Оно используется в качестве private ключа для данной операции.
    Сгенерированный приватный ключ, генератор AES и общее число (п.1) используются для создания публичного ключа,
    который, в итоге, связан с приватным, но может быть передан другой стороне.
    Обе стороны меняются только что созданными публичными ключами.
    Каждая сторона использует свой приватный ключ, публичный ключ другой стороны и число из п.1,
    для вычисления общего секретного ключа. Хотя, процесс вычисления независимый для каждой из сторон,
    в итоге получаются одинаковые ключи. В этом суть алгоритма Диффи-Хеллмана.
    Затем, полученный секретный ключ используется для симметричного шифрования соединения.

    Симметричное шифрование, которое используется с этого момента на протяжении всего соединения,
    называется бинарным пакетным протоколом (binary packet protocol). Вышеизложенный процесс,
    позволяет каждой стороне принимать участие в генерировании общего секретного ключа.
    Не позволяет одной из сторон контролировать его. Но самое главное, появляется возможность создать одинаковый ключ на каждой из машин,
    без его передачи через незащищенное соединение.

    Получившийся ключ – симметричный. То есть используется для шифрации и дешифрации.
    Его назначение защитить все передаваемые данные между клиентом и сервером, создать, своего рода, туннель,
    содержимое которого не смогут читать третьи лица
    */

    class Alice
    {
        public static byte[] alicePublicKey;

        public static void Main(string[] args)
        {
            using (ECDiffieHellmanCng alice = new ECDiffieHellmanCng())
            {

                alice.KeyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hash;
                alice.HashAlgorithm = CngAlgorithm.Sha256;
                alicePublicKey = alice.PublicKey.ToByteArray();
                Bob bob = new Bob();
                CngKey k = CngKey.Import(bob.bobPublicKey, CngKeyBlobFormat.EccPublicBlob);
                byte[] aliceKey = alice.DeriveKeyMaterial(CngKey.Import(bob.bobPublicKey, CngKeyBlobFormat.EccPublicBlob));
                byte[] encryptedMessage = null;
                byte[] iv = null;
                Send(aliceKey, "Secret message", out encryptedMessage, out iv);
                bob.Receive(encryptedMessage, iv);
            }

        }

        private static void Send(byte[] key, string secretMessage, out byte[] encryptedMessage, out byte[] iv)
        {
            using (Aes aes = new AesCryptoServiceProvider())
            {
                aes.Key = key;
                iv = aes.IV;

                // Encrypt the message
                using (MemoryStream ciphertext = new MemoryStream())
                using (CryptoStream cs = new CryptoStream(ciphertext, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    byte[] plaintextMessage = Encoding.UTF8.GetBytes(secretMessage);
                    cs.Write(plaintextMessage, 0, plaintextMessage.Length);
                    cs.Close();
                    encryptedMessage = ciphertext.ToArray();
                }
            }
        }

    }

    public class Bob
    {
        public byte[] bobPublicKey;
        private byte[] bobKey;
        public Bob()
        {
            using (ECDiffieHellmanCng bob = new ECDiffieHellmanCng())
            {

                bob.KeyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hash;
                bob.HashAlgorithm = CngAlgorithm.Sha256;
                bobPublicKey = bob.PublicKey.ToByteArray();
                bobKey = bob.DeriveKeyMaterial(CngKey.Import(Alice.alicePublicKey, CngKeyBlobFormat.EccPublicBlob));

            }
        }

        public void Receive(byte[] encryptedMessage, byte[] iv)
        {

            using (Aes aes = new AesCryptoServiceProvider())
            {
                aes.Key = bobKey;
                aes.IV = iv;
                // Decrypt the message
                using (MemoryStream plaintext = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(plaintext, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(encryptedMessage, 0, encryptedMessage.Length);
                        cs.Close();
                        string message = Encoding.UTF8.GetString(plaintext.ToArray());
                        Console.WriteLine(message);
                    }
                }
            }
        }

    }
}