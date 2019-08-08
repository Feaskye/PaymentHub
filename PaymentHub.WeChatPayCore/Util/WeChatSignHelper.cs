using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace PaymentHub.WeChatPayCore.Util
{
    internal static class WeChatSignHelper
    {
        // Fields
        private static string _RandomString = "0123456789abcdefghijklmnopqrstuvwxzyABCDEFGHIJKLMNOPQRSTUVWXZY";

        // Methods
        private static string ComputeMD5StringHashValue(string input)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            MD5 md1 = MD5.Create();
            md1.ComputeHash(bytes);
            byte[] hash = md1.Hash;
            StringBuilder builder = new StringBuilder(0x20);
            for (int i = 0; i < hash.Length; i++)
            {
                builder.AppendFormat("{0:X2}", hash[i]);
            }
            return builder.ToString();
        }

        internal static string CreateRandomString()
        {
            int capacity = 0x20;
            Random random = new Random();
            StringBuilder builder = new StringBuilder(capacity);
            int length = _RandomString.Length;
            for (int i = 0; i < capacity; i++)
            {
                int num4 = random.Next(0, length);
                builder.Append(_RandomString[num4]);
            }
            return builder.ToString();
        }

        private static string CreateSign(string apiKey, SortedDictionary<string, object> values)
        {
            StringBuilder builder = new StringBuilder();
            foreach (KeyValuePair<string, object> pair in values)
            {
                if (pair.Value == null)
                {
                    continue;
                }
                if (!pair.Key.Equals("sign", StringComparison.OrdinalIgnoreCase) && (pair.Value.ToString() != ""))
                {
                    builder.AppendFormat("{0}={1}&", pair.Key, pair.Value);
                }
            }
            string str = builder.ToString(0, builder.Length - 1);
            return ComputeMD5StringHashValue($"{str}&key={apiKey}");
        }

        internal static string CreateSign<T>(string apiKey, T value) where T : class
        {
            SortedDictionary<string, object> values = new SortedDictionary<string, object>();
            foreach (PropertyInfo info in typeof(T).GetProperties())
            {
                XmlElementAttribute attribute = info.GetCustomAttributes(typeof(XmlElementAttribute), false).OfType<XmlElementAttribute>().FirstOrDefault<XmlElementAttribute>();
                if (attribute != null)
                {
                    values.Add(attribute.ElementName, info.GetValue(value));
                }
            }
            return CreateSign(apiKey, values);
        }
    }

}
