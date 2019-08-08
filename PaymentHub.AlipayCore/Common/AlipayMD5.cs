using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace PaymentHub.AlipayCore.Common
{


    public sealed class AlipayMD5
    {
        // Methods
        public static string Sign(string prestr, string key, string _input_charset)
        {
            StringBuilder builder = new StringBuilder(0x20);
            prestr = prestr + key;
            byte[] buffer = new MD5CryptoServiceProvider().ComputeHash(Encoding.GetEncoding(_input_charset).GetBytes(prestr));
            for (int i = 0; i < buffer.Length; i++)
            {
                builder.Append(buffer[i].ToString("x").PadLeft(2, '0'));
            }
            return builder.ToString();
        }

        public static bool Verify(string prestr, string sign, string key, string _input_charset) =>
            (Sign(prestr, key, _input_charset) == sign);
    }



}
