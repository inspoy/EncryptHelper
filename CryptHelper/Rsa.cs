/**
 * == Inspoy Technology ==
 * Assembly: Instech.EncryptHelper
 * FileName: Rsa.cs
 * Created on 2019/12/12 by inspoy
 * All rights reserved.
 */

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Instech.EncryptHelper
{
    public static class Rsa
    {
        /// <summary>
        /// 生成一对新的公钥和私钥
        /// </summary>
        /// <param name="size">字节长度，必须是2的整数次幂(大于63)</param>
        /// <param name="pk"></param>
        /// <param name="sk"></param>
        public static void GenerateKey(int size, out byte[] pk, out byte[] sk)
        {
            if (size < 64 || (size & size - 1) != 0)
            {
                throw new ArgumentOutOfRangeException(nameof(size), size, "Size must be power of 2 and greater than 63");
            }
            var p = new RSACryptoServiceProvider(size * 8).ExportParameters(true);

            pk = p.Modulus;
            sk = new byte[size / 2 * 7];
            p.D.CopyTo(sk, 0);
            p.DP.CopyTo(sk, size / 2 * 2);
            p.DQ.CopyTo(sk, size / 2 * 3);
            p.P.CopyTo(sk, size / 2 * 4);
            p.Q.CopyTo(sk, size / 2 * 5);
            p.InverseQ.CopyTo(sk, size / 2 * 6);
        }

        public static byte[] Encrypt(string src, byte[] pk)
        {
            return Encrypt(Encoding.UTF8.GetBytes(src), pk);
        }

        public static byte[] Encrypt(byte[] src, byte[] pk)
        {
            var maxLength = pk.Length - 11;
            if (src.Length > maxLength)
            {
                // 内容过长，分块压缩
                var ret = new byte[(src.Length / maxLength + 1) * pk.Length];
                for (var i = 0; i < src.Length / maxLength + 1; ++i)
                {
                    var block = new byte[i < src.Length / maxLength ? maxLength : (src.Length - 1) % maxLength + 1];
                    Array.Copy(src, i * maxLength, block, 0, block.Length);
                    Encrypt(block, pk).CopyTo(ret, i * pk.Length);
                }
                return ret;
            }
            var param = new RSAParameters
            {
                Modulus = pk,
                Exponent = new byte[] { 1, 0, 1 }
            };
            var p = new RSACryptoServiceProvider(8 * pk.Length);
            p.ImportParameters(param);
            var ans = p.Encrypt(src, false);
            p.Dispose();
            return ans;
        }

        public static byte[] Decrypt(byte[] src, byte[] pk, byte[] sk)
        {
            if (sk == null)
            {
                throw new ArgumentNullException(nameof(sk));
            }
            if (sk.Length % 7 != 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sk), sk.Length, "Length of sk is invalid");
            }
            if (src.Length % pk.Length != 0)
            {
                throw new ArgumentOutOfRangeException(nameof(src), src.Length, "Length of src is invalid");
            }
            if (src.Length > pk.Length)
            {
                // 内容过长，需要分块
                var ret = new List<byte>(src.Length);
                var block = new byte[pk.Length];
                for (var i = 0; i < src.Length / pk.Length; ++i)
                {
                    Array.Copy(src, i * pk.Length, block, 0, pk.Length);
                    ret.AddRange(Decrypt(block, pk, sk));
                }
                return ret.ToArray();
            }
            var size = pk.Length;
            var param = new RSAParameters
            {
                Modulus = pk,
                D = new byte[size],
                DP = new byte[size / 2],
                DQ = new byte[size / 2],
                P = new byte[size / 2],
                Q = new byte[size / 2],
                InverseQ = new byte[size / 2],
                Exponent = new byte[] { 1, 0, 1 }
            };
            Array.Copy(sk, 0, param.D, 0, size);
            Array.Copy(sk, size, param.DP, 0, size / 2);
            Array.Copy(sk, size / 2 * 3, param.DQ, 0, size / 2);
            Array.Copy(sk, size / 2 * 4, param.P, 0, size / 2);
            Array.Copy(sk, size / 2 * 5, param.Q, 0, size / 2);
            Array.Copy(sk, size / 2 * 6, param.InverseQ, 0, size / 2);
            var p = new RSACryptoServiceProvider(size * 8);
            p.ImportParameters(param);
            var ans = p.Decrypt(src, false);
            p.Dispose();
            return ans;
        }
    }
}