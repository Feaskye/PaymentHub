using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace PaymentHub.AlipayCore.Common
{
    public class AlipaySubmit
    {
        // Fields
        private static string GATEWAY_NEW = "https://mapi.alipay.com/gateway.do?";
        private static string _key = "";
        private static string _input_charset = "";
        private static string _sign_type = "";

        // Methods
        static AlipaySubmit()
        {
            _key = AlipayConfig.Key.Trim();
            _input_charset = AlipayConfig.Input_charset.Trim().ToLower();
            _sign_type = AlipayConfig.Sign_type.Trim().ToUpper();
        }

        public static string BuildRequest(SortedDictionary<string, string> sParaTemp)
        {
            Encoding code = Encoding.GetEncoding(_input_charset);
            byte[] bytes = code.GetBytes(BuildRequestParaToString(sParaTemp, code));
            string requestUriString = GATEWAY_NEW + "_input_charset=" + _input_charset;
            string str3 = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUriString);
                request.Method = "post";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = bytes.Length;
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
                Stream responseStream = ((HttpWebResponse)request.GetResponse()).GetResponseStream();
                StreamReader reader = new StreamReader(responseStream, code);
                StringBuilder builder = new StringBuilder();
                while (true)
                {
                    string str4 = reader.ReadLine();
                    if (str4 == null)
                    {
                        responseStream.Close();
                        str3 = builder.ToString();
                        break;
                    }
                    builder.Append(str4);
                }
            }
            catch (Exception exception)
            {
                str3 = "报错：" + exception.Message;
            }
            return str3;
        }

        public static string BuildRequest(SortedDictionary<string, string> sParaTemp, string strMethod, string strButtonValue)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary = BuildRequestPara(sParaTemp);
            StringBuilder builder = new StringBuilder();
            string[] textArray1 = new string[] { "<form id='alipaysubmit' name='alipaysubmit' action='", GATEWAY_NEW, "_input_charset=", _input_charset, "' method='", strMethod.ToLower().Trim(), "'>" };
            builder.Append(string.Concat(textArray1));
            foreach (KeyValuePair<string, string> pair in dictionary)
            {
                string[] textArray2 = new string[] { "<input type='hidden' name='", pair.Key, "' value='", pair.Value, "'/>" };
                builder.Append(string.Concat(textArray2));
            }
            builder.Append("<input type='submit' value='" + strButtonValue + "' style='display:none;'></form>");
            builder.Append("<script>document.forms['alipaysubmit'].submit();</script>");
            return builder.ToString();
        }

        public static string BuildRequest(SortedDictionary<string, string> sParaTemp, string strMethod, string fileName, byte[] data, string contentType, int lengthFile)
        {
            Stream responseStream;
            string str6;
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(GATEWAY_NEW + "_input_charset=" + _input_charset);
            request.Method = strMethod;
            string str2 = DateTime.Now.Ticks.ToString("x");
            string str3 = "--" + str2;
            request.ContentType = "\r\nmultipart/form-data; boundary=" + str2;
            request.KeepAlive = true;
            StringBuilder builder = new StringBuilder();
            foreach (KeyValuePair<string, string> pair in BuildRequestPara(sParaTemp))
            {
                string[] textArray1 = new string[] { str3, "\r\nContent-Disposition: form-data; name=\"", pair.Key, "\"\r\n\r\n", pair.Value, "\r\n" };
                builder.Append(string.Concat(textArray1));
            }
            builder.Append(str3 + "\r\nContent-Disposition: form-data; name=\"withhold_file\"; filename=\"");
            builder.Append(fileName);
            builder.Append("\"\r\nContent-Type: " + contentType + "\r\n\r\n");
            Encoding encoding = Encoding.GetEncoding(_input_charset);
            byte[] bytes = encoding.GetBytes(builder.ToString());
            byte[] buffer = Encoding.ASCII.GetBytes("\r\n" + str3 + "--\r\n");
            request.ContentLength = (bytes.Length + lengthFile) + buffer.Length;
            Stream requestStream = request.GetRequestStream();
            try
            {
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Write(data, 0, lengthFile);
                requestStream.Write(buffer, 0, buffer.Length);
                responseStream = ((HttpWebResponse)request.GetResponse()).GetResponseStream();
            }
            catch (WebException exception1)
            {
                return exception1.ToString();
            }
            finally
            {
                if (requestStream != null)
                {
                    requestStream.Close();
                }
            }
            StreamReader reader = new StreamReader(responseStream, encoding);
            StringBuilder builder2 = new StringBuilder();
            while (true)
            {
                string str5 = reader.ReadLine();
                if (str5 == null)
                {
                    responseStream.Close();
                    str6 = builder2.ToString();
                    break;
                }
                builder2.Append(str5);
            }
            return str6;
        }

        private static string BuildRequestMysign(Dictionary<string, string> sPara)
        {
            string prestr = AlipayCore.CreateLinkString(sPara);
            return ((_sign_type == "MD5") ? AlipayMD5.Sign(prestr, _key, _input_charset) : "");
        }

        private static Dictionary<string, string> BuildRequestPara(SortedDictionary<string, string> sParaTemp)
        {
            Dictionary<string, string> sPara = new Dictionary<string, string>();
            string str = "";
            sPara = AlipayCore.FilterPara(sParaTemp);
            str = BuildRequestMysign(sPara);
            sPara.Add("sign", str);
            sPara.Add("sign_type", _sign_type);
            return sPara;
        }

        private static string BuildRequestParaToString(SortedDictionary<string, string> sParaTemp, Encoding code)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            return AlipayCore.CreateLinkStringUrlencode(BuildRequestPara(sParaTemp), code);
        }

        public static string Query_timestamp()
        {
            string[] textArray1 = new string[] { GATEWAY_NEW, "service=query_timestamp&partner=", AlipayConfig.Partner, "&_input_charset=", AlipayConfig.Input_charset };
            XmlDocument document = new XmlDocument();
            document.Load(new XmlTextReader(string.Concat(textArray1)));
            return document.SelectSingleNode("/alipay/response/timestamp/encrypt_key").InnerText;
        }
    }

}
