using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;


namespace Sipay
{
    class Helper
    {
        public static string GenerateHashKey(string total, string installment, string currency_code,
            string merchant_key, string invoice_id, string app_secret)
        {
            var data = total + '|' + installment + '|' + currency_code + '|' + merchant_key + '|' + invoice_id;
            var sha1 = new SHA1Managed();
            Random rnd = new Random();
            var iv = sha1.ComputeHash(Encoding.UTF8.GetBytes(rnd.ToString()));
            var password = sha1.ComputeHash(Encoding.UTF8.GetBytes(app_secret)).ToString();
            rnd = new Random();
            var shaSalt = sha1.ComputeHash(Encoding.UTF8.GetBytes(rnd.ToString())).ToString();
            var salt = shaSalt.Substring(0, 4);

            var sha256 = new SHA256Managed();
            var saltWithPassword = sha256.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
            //var encrypted = EncryptString(data,saltWithPassword,iv);
            var encrypted = "asdasdasd";
            var msg_encrypted_bundle = iv + ":" + salt + ":" + encrypted;
            msg_encrypted_bundle = msg_encrypted_bundle.Replace("/", "__");
            return msg_encrypted_bundle;
        }

        public static string EncryptString(string plainText, byte[] key, byte[] iv)
        {
            Aes encryptor = Aes.Create();
            encryptor.Mode = CipherMode.CBC;
            encryptor.Key = key;
            encryptor.IV = iv;
            MemoryStream memoryStream = new MemoryStream();
            ICryptoTransform aesEncryptor = encryptor.CreateEncryptor();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, aesEncryptor, CryptoStreamMode.Write);
            byte[] plainBytes = Encoding.ASCII.GetBytes(plainText);
            cryptoStream.Write(plainBytes, 0, plainBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] cipherBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            string cipherText = Convert.ToBase64String(cipherBytes, 0, cipherBytes.Length);
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