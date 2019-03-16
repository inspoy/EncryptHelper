using System;

namespace TestConsole
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            RunAes();
            RunRc4();
        }

        private static void RunAes()
        {

        }

        private static void RunRc4()
        {
            var runner = new TestRc4();
            runner.Run();
        }
    }
}
