using ProgrammersToolKit.Services.Interfaces;
using System;
using System.Security.Cryptography;
using System.Text;

namespace ProgrammersToolKit.Services
{
    public class EncryptionToolService : IEncryptionToolService
    {
        public string Encrypt(string input, string key, string algorithm)
        {
            switch (algorithm.ToLower())
            {
                case "base64":
                    return Convert.ToBase64String(Encoding.UTF8.GetBytes(input));
                case "aes":
                    return EncryptAes(input, key);
                case "des":
                    return EncryptDes(input, key);
                default:
                    throw new NotSupportedException($"Algorithm '{algorithm}' is not supported.");
            }
        }

        public string Decrypt(string input, string key, string algorithm)
        {
            switch (algorithm.ToLower())
            {
                case "base64":
                    var bytes = Convert.FromBase64String(input);
                    return Encoding.UTF8.GetString(bytes);
                case "aes":
                    return DecryptAes(input, key);
                case "des":
                    return DecryptDes(input, key);
                default:
                    throw new NotSupportedException($"Algorithm '{algorithm}' is not supported.");
            }
        }

        private string EncryptAes(string plainText, string key)
        {
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(key.PadRight(32).Substring(0, 32));
            aes.IV = new byte[16];
            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            var plainBytes = Encoding.UTF8.GetBytes(plainText);
            var cipherBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
            return Convert.ToBase64String(cipherBytes);
        }

        private string DecryptAes(string cipherText, string key)
        {
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(key.PadRight(32).Substring(0, 32));
            aes.IV = new byte[16];
            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            var cipherBytes = Convert.FromBase64String(cipherText);
            var plainBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
            return Encoding.UTF8.GetString(plainBytes);
        }

        private string EncryptDes(string plainText, string key)
        {
            using var des = DES.Create();
            des.Key = Encoding.UTF8.GetBytes(key.PadRight(8).Substring(0, 8));
            des.IV = new byte[8];
            var encryptor = des.CreateEncryptor(des.Key, des.IV);
            var plainBytes = Encoding.UTF8.GetBytes(plainText);
            var cipherBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
            return Convert.ToBase64String(cipherBytes);
        }

        private string DecryptDes(string cipherText, string key)
        {
            using var des = DES.Create();
            des.Key = Encoding.UTF8.GetBytes(key.PadRight(8).Substring(0, 8));
            des.IV = new byte[8];
            var decryptor = des.CreateDecryptor(des.Key, des.IV);
            var cipherBytes = Convert.FromBase64String(cipherText);
            var plainBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
            return Encoding.UTF8.GetString(plainBytes);
        }
    }
}
