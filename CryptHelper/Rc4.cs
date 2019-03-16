using System;
using System.Text;

namespace Instech.CryptHelper
{
    public class Rc4
    {
        public byte[] State;
        public byte[] Temp;

        public Rc4()
        {
            State = new byte[256];
            Temp = new byte[256];
        }

        public void SetKeyAndInit(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            var bytes = Encoding.UTF8.GetBytes(key);
            SetKeyAndInit(TrimKey(bytes));
        }

        public void SetKeyAndInit(byte[] key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (key.Length <= 0 || key.Length > 256)
            {
                throw new ArgumentOutOfRangeException(nameof(key), $"Length of key out of range(expected: 1~256): {key.Length}");
            }
            for (var i = 0; i < 256; ++i)
            {
                State[i] = (byte)i;
                Temp[i] = key[i % key.Length];
            }

            var j = 0;
            for (var i = 0; i < 256; ++i)
            {
                j = (j + State[i] + Temp[i]) % 256;
                var t = State[i];
                State[i] = Temp[i];
                Temp[i] = t;
            }
        }

        private byte[] TrimKey(byte[] key)
        {
            if (key.Length > 256)
            {
                var bytes = new byte[256];
                Array.Copy(key, bytes, 256);
                return bytes;
            }
            return key;
        }

        private byte[] GetKey(int len)
        {
            var ret = new byte[len];
            var i = 0;
            var j = 0;
            for (var k = 0; k < len; ++k)
            {
                i = (i + 1) % 256;
                j = (j + State[i]) % 256;
                var t = State[i];
                State[i] = State[j];
                State[j] = t;
                ret[k] = State[State[i] + State[j] % 256];
            }
            return ret;
        }

        public byte[] Encrypt(byte[] src)
        {
            if (State == null)
            {
                throw new InvalidOperationException("Instance has not been inited.");
            }
            if (src == null)
            {
                throw new ArgumentNullException(nameof(src));
            }
            if (src.Length == 0)
            {
                return Array.Empty<byte>();
            }
            var keys = GetKey(src.Length);
            var ret = new byte[src.Length];
            for (var i = 0; i < src.Length; ++i)
            {
                ret[i] = (byte)(src[i] ^ keys[i]);
            }
            return ret;
        }
    }
}
