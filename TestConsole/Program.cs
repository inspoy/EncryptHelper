using System;

namespace TestConsole
{
    public class Program
    {
        const string Raw = "Apple Banana 1234567 中文试试能不能用";
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //RunAes();
            //RunRc4();
            RunRsa();
        }

        private static void RunAes()
        {
            Console.WriteLine("=====  AES  =====");
            var runner = new TestAes();
            runner.Run(Raw);
            Console.WriteLine("=====END AES=====");
        }

        private static void RunRc4()
        {
            Console.WriteLine("=====  RC4  =====");
            var runner = new TestRc4();
            runner.Run(Raw);
            Console.WriteLine("=====END RC4=====");
        }

        private static void RunRsa()
        {
            Console.WriteLine("=====  RSA  =====");
            var runner = new TestRsa();
            runner.Run(Raw);
            Console.WriteLine("=====END RSA=====");
        }
    }

    public static class Utils
    {
        public static void WriteByteArray(byte[] src, string title = null)
        {
            Console.WriteLine($"{(string.IsNullOrEmpty(title) ? "" : title)}[{src?.Length ?? 0}]{(src == null ? "NULL" : Convert.ToBase64String(src))}");
        }
    }
}
