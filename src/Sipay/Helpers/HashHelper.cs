using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Sipay.Helpers
{
    public class HashHelper
    {
        public static string GenerateHashKey(string total, string installment, string currencyCode,
            string merchantKey, string invoiceId, string appSecret)
        {
            var data = total + "|" + installment + "|" + currencyCode + "|" + merchantKey + "|" + invoiceId;

            var mtRand = new Random();

            var iv = Sha1Hash(mtRand.Next().ToString()).Substring(0, 16);
            var password = Sha1Hash(appSecret);
            var salt = Sha1Hash(mtRand.Next().ToString()).Substring(0, 4);

            var saltWithPassword = "";
            using (var sha256Hash = SHA256.Create())
            {
                saltWithPassword = GetHash(sha256Hash, password + salt);
            }

            var encrypted = Encryptor(data, saltWithPassword.Substring(0, 32), iv);

            var msgEncryptedBundle = iv + ":" + salt + ":" + encrypted;
            msgEncryptedBundle = msgEncryptedBundle.Replace("/", "__");

            return msgEncryptedBundle;
        }

        public static IList<string> ValidateHashKey(string hashKey, string appSecret)
        {
            hashKey = hashKey.Replace("__", "/");

            var password = Sha1Hash(appSecret);

            IList<string> mainStringArray = hashKey.Split(':').ToList();

            if (mainStringArray.Count == 3)
            {
                var iv = mainStringArray[0];
                var salt = mainStringArray[1];
                var mainKey = mainStringArray[2];

                var saltWithPassword = "";
                using (var sha256Hash = SHA256.Create())
                {
                    saltWithPassword = GetHash(sha256Hash, password + salt);
                }

                var orginalValues = Decryptor(mainKey, saltWithPassword.Substring(0, 32), iv);
                IList<string> valueList = orginalValues.Split('|').ToList();

                return valueList;
            }

            return new List<string>();
        }

        private static string GetHash(HashAlgorithm hashAlgorithm, string input)
        {
            var data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));
            var sBuilder = new StringBuilder();
            for (var i = 0; i < data.Length; i++) sBuilder.Append(data[i].ToString("x2"));

            return sBuilder.ToString();
        }

        private static string Sha1Hash(string password)
        {
            return string.Join("",
                SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(password))
                    .Select(x => x.ToString("x2")));
        }

        private static string Encryptor(string textToEncrypt, string strKey, string strIV)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(textToEncrypt);
            var aesProvider = new AesCryptoServiceProvider();

            aesProvider.BlockSize = 128;
            aesProvider.KeySize = 256;
            aesProvider.Key = Encoding.UTF8.GetBytes(strKey);
            aesProvider.IV = Encoding.UTF8.GetBytes(strIV);
            aesProvider.Padding = PaddingMode.PKCS7;
            aesProvider.Mode = CipherMode.CBC;

            var cryptoTransform = aesProvider.CreateEncryptor(aesProvider.Key, aesProvider.IV);
            var encryptedBytes = cryptoTransform.TransformFinalBlock(plainTextBytes, 0, plainTextBytes.Length);
            return Convert.ToBase64String(encryptedBytes);
        }

        private static string Decryptor(string textToDecrypt, string strKey, string strIv)
        {
            var encryptedBytes = Convert.FromBase64String(textToDecrypt);

            var aesProvider = new AesCryptoServiceProvider();
            aesProvider.BlockSize = 128;
            aesProvider.KeySize = 256;
            aesProvider.Key = Encoding.ASCII.GetBytes(strKey);
            aesProvider.IV = Encoding.ASCII.GetBytes(strIv);
            aesProvider.Padding = PaddingMode.PKCS7;
            aesProvider.Mode = CipherMode.CBC;

            var cryptoTransform = aesProvider.CreateDecryptor(aesProvider.Key, aesProvider.IV);
            var decryptedBytes = cryptoTransform.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
            return Encoding.ASCII.GetString(decryptedBytes);
        }
    }
}