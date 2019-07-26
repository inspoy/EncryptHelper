using System;
using System.Text;
using Instech.CryptHelper;

namespace TestConsole
{
    public class TestRsa
    {
        public void Run(string raw)
        {
            const int byteSize = 2048 / 8;
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

        public static void GenKeyPair()
        {
            const int byteSize = 2048 / 8;
            Rsa.GenerateKey(byteSize, out var pk, out var sk);
            var pks = "public readonly static byte[] PkData = new byte[]{\n    ";
            for (var i = 0; i < pk.Length; ++i)
            {
                pks += $"{pk[i]},";
                if (i % 20 == 19)
                {
                    pks += "\n    ";
                }
            }
            pks += "\n};";
            var sks = "public readonly static byte[] SkData = new byte[]{\n    ";
            for (var i = 0; i < sk.Length; ++i)
            {
                sks += $"{sk[i]},";
                if (i % 20 == 19)
                {
                    sks += "\n    ";
                }
            }
            sks += "\n};";
            Console.WriteLine(pks);
            Console.WriteLine(sks);
        }
    }
}
