using System;
using System.Text;
using Instech.EncryptHelper;

namespace TestConsole
{
    public class TestRc4
    {
        public void Run(string raw)
        {
            var rc4 = new Rc4();
            const string key = "HelloWorld!";
            var e = Encoding.UTF8;
            rc4.SetKeyAndInit(key);
            Utils.WriteByteArray(e.GetBytes(raw), "raw");
            var secret = rc4.Encrypt(e.GetBytes(raw));
            Utils.WriteByteArray(secret, "secret");
            rc4.SetKeyAndInit(key);
            var real = rc4.Encrypt(secret);
            Console.WriteLine("Real: " + e.GetString(real));
            if (!raw.Equals(e.GetString(real)))
            {
                throw new Exception("Rc4 Test Failed");
            }
        }
    }
}
