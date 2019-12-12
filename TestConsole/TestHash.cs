using System;
using System.Text;
using Instech.EncryptHelper;

namespace TestConsole
{
    public class TestHash
    {
        public void Run(string raw)
        {
            var hash = HashInterface.ComputeHash(Encoding.UTF8.GetBytes(raw));
            Console.WriteLine("Hash: " + hash);
        }
    }
}
