using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace PaymentHub.AlipayCore.Common
{
    public class AlipayNotify
    {
        // Fields
        private string _partner = "";
        private string _key = "";
        private string _input_charset = "";
        private string _sign_type = "";
        private string Https_veryfy_url = "https://mapi.alipay.com/gateway.do?service=notify_verify&";

        // Methods
        public AlipayNotify()
        {
            this._partner = AlipayConfig.Partner.Trim();
            this._key = AlipayConfig.Key.Trim();
            this._input_charset = AlipayConfig.Input_charset.Trim().ToLower();
            this._sign_type = AlipayConfig.Sign_type.Trim().ToUpper();
        }

        private string Get_Http(string strUrl, int timeout)
        {
            string str;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strUrl);
                request.Timeout = timeout;
                StreamReader reader = new StreamReader(((HttpWebResponse)request.GetResponse()).GetResponseStream(), Encoding.Default);
                StringBuilder builder = new StringBuilder();
                while (true)
                {
                    if (-1 == reader.Peek())
                    {
                        str = builder.ToString();
                        break;
                    }
                    builder.Append(reader.ReadLine());
                }
            }
            catch (Exception exception)
            {
                str = "错误：" + exception.Message;
            }
            return str;
        }

        private string GetPreSignStr(SortedDictionary<string, string> inputPara)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            return AlipayCore.CreateLinkString(AlipayCore.FilterPara(inputPara));
        }

        private string GetResponseTxt(string notify_id)
        {
            string[] textArray1 = new string[] { this.Https_veryfy_url, "partner=", this._partner, "&notify_id=", notify_id };
            string strUrl = string.Concat(textArray1);
            return this.Get_Http(strUrl, 0x1_d4c0);
        }

        private bool GetSignVeryfy(SortedDictionary<string, string> inputPara, string sign)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            string prestr = AlipayCore.CreateLinkString(AlipayCore.FilterPara(inputPara));
            bool flag = false;
            if (((sign != null) && (sign != "")) && (this._sign_type == "MD5"))
            {
                flag = AlipayMD5.Verify(prestr, sign, this._key, this._input_charset);
            }
            return flag;
        }

        public bool Verify(SortedDictionary<string, string> inputPara, string notify_id, string sign)
        {
            bool signVeryfy = this.GetSignVeryfy(inputPara, sign);
            string responseTxt = "true";
            if ((notify_id != null) && (notify_id != ""))
            {
                responseTxt = this.GetResponseTxt(notify_id);
            }
            return ((responseTxt == "true") & signVeryfy);
        }
    }

}
