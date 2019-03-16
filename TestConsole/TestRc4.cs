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
            var seceret = rc4.Encrypt(e.GetBytes(raw));
            Console.WriteLine(e.GetString(seceret));
            rc4.SetKeyAndInit(key);
            var real = rc4.Encrypt(seceret);
            Console.WriteLine(e.GetString(real));
        }
    }
}
