using System;
using System.Text;
using Instech.CryptHelper;

namespace TestConsole
{
    public class TestRsa
    {
        public void Run(string raw)
        {
            const int byteSize = 512 / 8;
            raw = raw + raw;
            Rsa.GenerateKey(byteSize, out var pk, out var sk);
            Utils.WriteByteArray(pk, "pk");
            Utils.WriteByteArray(sk, "sk");

            var e = Encoding.UTF8;
            Utils.WriteByteArray(e.GetBytes(raw), "raw");
            var secret = Rsa.Encrypt(e.GetBytes(raw), pk);
            Utils.WriteByteArray(secret, "secret");
            var real = Rsa.Decrypt(secret, pk, sk);
            Console.WriteLine("Real: " + e.GetString(real));
        }
    }
}
