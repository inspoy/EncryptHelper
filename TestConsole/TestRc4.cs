using System;
using System.Text;
using Instech.CryptHelper;

namespace TestConsole
{
    public class TestRc4
    {
        public void Run()
        {
            var rc4 = new Rc4();
            var raw = "Apple Banana 1234567";
            var key = "HelloWorld!";
            var e = Encoding.UTF8;
            rc4.SetKeyAndInit(key);
            var secret = rc4.Encrypt(e.GetBytes(raw));
            Console.WriteLine(e.GetString(secret));
            rc4.SetKeyAndInit(key);
            var real = rc4.Encrypt(secret);
            Console.WriteLine(e.GetString(real));
        }
    }
}
