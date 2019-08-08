using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace PaymentHub.AlipayCore.Common
{

    public class AlipayCore
    {
  
        public static string CreateLinkString(Dictionary<string, string> dicArray)
        {
            StringBuilder builder = new StringBuilder();
            foreach (KeyValuePair<string, string> pair in dicArray)
            {
                builder.Append(pair.Key + "=" + pair.Value + "&");
            }
            builder.Remove(builder.Length - 1, 1);
            return builder.ToString();
        }

        public static string CreateLinkStringUrlencode(Dictionary<string, string> dicArray, Encoding code)
        {
            StringBuilder builder = new StringBuilder();
            foreach (KeyValuePair<string, string> pair in dicArray)
            {
                builder.Append(pair.Key + "=" + HttpUtility.UrlEncode(pair.Value, code) + "&");
            }
            builder.Remove(builder.Length - 1, 1);
            return builder.ToString();
        }

        public static Dictionary<string, string> FilterPara(SortedDictionary<string, string> dicArrayPre)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> pair in dicArrayPre)
            {
                int num1;
                if (((pair.Key.ToLower() == "sign") || (pair.Key.ToLower() == "sign_type")) || (pair.Value == ""))
                {
                    num1 = 0;
                }
                else
                {
                    num1 = pair.Value!=null?1:0;
                }
                if (num1 != 0)
                {
                    dictionary.Add(pair.Key, pair.Value);
                }
            }
            return dictionary;
        }

        public static string GetAbstractToMD5(Stream sFile)
        {
            byte[] buffer = new MD5CryptoServiceProvider().ComputeHash(sFile);
            StringBuilder builder = new StringBuilder(0x20);
            for (int i = 0; i < buffer.Length; i++)
            {
                builder.Append(buffer[i].ToString("x").PadLeft(2, '0'));
            }
            return builder.ToString();
        }

        public static string GetAbstractToMD5(byte[] dataFile)
        {
            byte[] buffer = new MD5CryptoServiceProvider().ComputeHash(dataFile);
            StringBuilder builder = new StringBuilder(0x20);
            for (int i = 0; i < buffer.Length; i++)
            {
                builder.Append(buffer[i].ToString("x").PadLeft(2, '0'));
            }
            return builder.ToString();
        }
        
    }
    
}
