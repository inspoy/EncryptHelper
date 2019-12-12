using System;
using System.Text;
using Instech.EncryptHelper;

namespace TestConsole
{
    public class TestAes
    {
        public void Run(string raw)
        {
            var aes = new Aes();
            var e = Encoding.UTF8;
            var key = aes.Init();
            Utils.WriteByteArray(key, "key");
            Utils.WriteByteArray(e.GetBytes(raw), "raw");
            var secret = aes.Encrypt(e.GetBytes(raw));
            Utils.WriteByteArray(secret, "secret");
            var real = aes.Decrypt(secret);
            Console.WriteLine("Real: " + e.GetString(real));
            aes.UnInit();
        }
    }
}
