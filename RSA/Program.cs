using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;


namespace RSA
{
    class Program
    {     
            private static RSAParameters publickey;
            private static RSAParameters privatekey;
            static string CONTAINER_NAME="MycontainerName";
          public enum KeySizes
        {
            SIZE_512=512,
            SIZE_1024=1024,
            SIZE_2048=2048,
            SIZE_952=952,
            SIZE_1369=1369,
        };
     
        static void Main(string[] args)
        {
            Console.Write("Mesajı giriniz: ");
            string mesaj = Console.ReadLine();
            GenerateKeys();
            byte[] encrypted = Encrypt(Encoding.UTF8.GetBytes(mesaj));
            byte[] decrypted = Decrypt(encrypted);
            Console.WriteLine("Orjinal Metin :" + mesaj+"\n");
            Console.WriteLine("Sifrelenmis Hali ");
            Console.ForegroundColor = ConsoleColor.DarkCyan; Console.WriteLine("" + BitConverter.ToString(encrypted).Replace(".", "" + "\n")+"\n"); Console.ResetColor();
            Console.WriteLine("Sifre Cozulmus Hali:\n" + Encoding.UTF8.GetString(decrypted));
            Console.Read();
        }
        static void GenerateKeys()
        {
            using (var rsa =new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                publickey = rsa.ExportParameters(false);
                privatekey = rsa.ExportParameters(true);
            }
        }
        static byte[] Encrypt(byte[] input)
        {
            byte[] encrypted;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(publickey);
                encrypted = rsa.Encrypt(input, true);
            }
            return encrypted;
        }
        static byte[] Decrypt(byte[] input)
        {
            byte[] decrypted;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(privatekey);
                decrypted = rsa.Decrypt(input, true);
            }
            return decrypted;
        }
    }
    }
