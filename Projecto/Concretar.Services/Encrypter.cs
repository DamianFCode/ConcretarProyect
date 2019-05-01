using System;
using System.Security.Cryptography;
using System.Text;

namespace Concretar.Services
{
    public static class Encrypter
    {
        public static string Encryption(string textPass, string desKey)
        {
            TripleDES des = CreateDES(desKey);
            ICryptoTransform ct = des.CreateEncryptor();
            byte[] input = Encoding.Unicode.GetBytes(textPass);
            var bytePass = ct.TransformFinalBlock(input, 0, input.Length);
            des.Clear();
            return Convert.ToBase64String(bytePass, 0, bytePass.Length);
        }

        static string Decryption(string CypherText, string deskey)
        {

            byte[] b = Convert.FromBase64String(CypherText);
            TripleDES des = CreateDES(deskey);
            ICryptoTransform ct = des.CreateDecryptor();
            byte[] output = ct.TransformFinalBlock(b, 0, b.Length);
            return Encoding.Unicode.GetString(output);
        }
        static TripleDES CreateDES(string key)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            TripleDES des = new TripleDESCryptoServiceProvider();
            des.Key = md5.ComputeHash(Encoding.Unicode.GetBytes(key));
            des.IV = new byte[des.BlockSize / 8];
            return des;
        }
    }
}
