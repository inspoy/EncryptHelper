/**
 * == Inspoy Technology ==
 * Assembly: Instech.EncryptHelper
 * FileName: Aes.cs
 * Created on 2019/12/12 by inspoy
 * All rights reserved.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Instech.EncryptHelper
{
    public class Aes
    {
        private AesCryptoServiceProvider _provider;

        public byte[] Init(byte[] key = null)
        {
            _provider = new AesCryptoServiceProvider();
            if (key != null && key.Length == 48)
            {
                var kk = new byte[32];
                var iv = new byte[16];
                Array.Copy(key, 0, kk, 0, 32);
                Array.Copy(key, 32, iv, 0, 16);
                _provider.Key = kk;
                _provider.IV = iv;
            }
            var ret = new byte[_provider.Key.Length + _provider.IV.Length];
            _provider.Key.CopyTo(ret, 0);
            _provider.IV.CopyTo(ret, _provider.Key.Length);
            return ret;
        }

        public byte[] Encrypt(string src)
        {
            if (_provider == null)
            {
                throw new InvalidOperationException("You must call Init() first.");
            }
            return Encrypt(Encoding.UTF8.GetBytes(src));
        }

        public byte[] Encrypt(byte[] src)
        {
            if (_provider == null)
            {
                throw new InvalidOperationException("You must call Init() first.");
            }
            var encryptor = _provider.CreateEncryptor();
            byte[] result;
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    using (var bw = new BinaryWriter(cs))
                    {
                        bw.Write(src);
                    }
                    result = ms.ToArray();
                }
            }
            return result;
        }

        public byte[] Decrypt(byte[] src)
        {
            if (_provider == null)
            {
                throw new InvalidOperationException("You must call Init() first.");
            }
            var decryptor = _provider.CreateDecryptor();
            var result = new List<byte>();
            using (var ms = new MemoryStream(src))
            {
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                {
                    while (true)
                    {
                        var b = cs.ReadByte();
                        if (b < 0) break;
                        result.Add((byte)b);
                    }
                    //using (var sr = new StreamReader(cs))
                    //{
                    //    result = Encoding.UTF8.GetBytes(sr.ReadToEnd());
                    //}
                }
            }
            return result.ToArray();
        }

        public void UnInit()
        {
            if (_provider != null)
            {
                _provider.Dispose();
                _provider = null;
            }
        }
    }
}
