using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Sipay
{
    class Helper
    {
        public static string GenerateHashKey(string total, string installment, string currencyCode,
            string merchantKey, string invoiceId, string appSecret)
        {
            var data = total + '|' + installment + '|' + currencyCode + '|' + merchantKey + '|' + invoiceId;
            var sha1 = new SHA1Managed();
            
            var rnd = new Random();
            var iv = sha1.ComputeHash(Encoding.UTF8.GetBytes(rnd.ToString()));
            var password = sha1.ComputeHash(Encoding.UTF8.GetBytes(appSecret)).ToString();
            
            rnd = new Random();
            var shaSalt = sha1.ComputeHash(Encoding.UTF8.GetBytes(rnd.ToString())).ToString();
            var salt = shaSalt.Substring(0, 4);

            var sha256 = new SHA256Managed();
            var saltWithPassword = sha256.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
            var encrypted = EncryptString(data,saltWithPassword,iv);
            
            var msgEncryptedBundle = iv + ":" + salt + ":" + encrypted;
            msgEncryptedBundle = msgEncryptedBundle.Replace("/", "__");
            
            return msgEncryptedBundle;
        }

        public static string EncryptString(string plainText, byte[] key, byte[] iv)
        {
            var encryptor = Aes.Create();
            encryptor.Mode = CipherMode.CBC;
            encryptor.Key = key;
            encryptor.IV = iv;

            var memoryStream = new MemoryStream();
            var aesEncryptor = encryptor.CreateEncryptor();
            var cryptoStream = new CryptoStream(memoryStream, aesEncryptor, CryptoStreamMode.Write);
            var plainBytes = Encoding.ASCII.GetBytes(plainText);
            
            cryptoStream.Write(plainBytes, 0, plainBytes.Length);
            cryptoStream.FlushFinalBlock();
            
            var cipherBytes = memoryStream.ToArray();
            
            memoryStream.Close();
            cryptoStream.Close();
            
            var cipherText = Convert.ToBase64String(cipherBytes, 0, cipherBytes.Length);
            return cipherText;
        }

        public static string ToKeyValueURL(object obj)
        {
            var keyvalues = obj.GetType().GetProperties()
                .ToList()
                .Select(p => $"{p.Name} = {p.GetValue(obj)}")
                .ToArray();

            return string.Join("&", keyvalues);
        }
    }
}