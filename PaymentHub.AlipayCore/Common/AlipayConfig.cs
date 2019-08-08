using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentHub.AlipayCore.Common
{


    public class AlipayConfig
    {
        // Fields
        private static string partner = "";
        private static string seller_email = "";
        private static string key = "";
        private static string input_charset = "";
        private static string sign_type = "";
        public static string DefaultDirectPayService = "create_direct_pay_by_user";
        public static string WapDirectPayService = "alipay.wap.create.direct.pay.by.user";
        public static Dictionary<string, string> AliBankCodes;

        // Methods
        static AlipayConfig()
        {
            Dictionary<string, string> dictionary1 = new Dictionary<string, string>();
            dictionary1.Add("ICBCBTB", "中国工商银行");
            dictionary1.Add("ABCBTB", "中国农业银行");
            dictionary1.Add("CCBBTB", "中国建设银行");
            dictionary1.Add("SPDBB2B", "上海浦东发展银行");
            dictionary1.Add("BOCBTB", "中国银行");
            dictionary1.Add("CMBBTB", "招商银行");
            dictionary1.Add("BOCB2C", "中国银行");
            dictionary1.Add("ICBCB2C", "中国工商银行");
            dictionary1.Add("CMB", "招商银行");
            dictionary1.Add("CCB", "中国建设银行");
            dictionary1.Add("ABC", "中国农业银行");
            dictionary1.Add("SPDB", "上海浦东发展银行");
            dictionary1.Add("CIB", "兴业银行");
            dictionary1.Add("GDB", "广发银行");
            dictionary1.Add("FDB", "富滇银行");
            dictionary1.Add("HZCBB2C", "杭州银行");
            dictionary1.Add("SHBANK", "上海银行");
            dictionary1.Add("NBBANK", "宁波银行");
            dictionary1.Add("SPABANK", "平安银行");
            dictionary1.Add("POSTGC", "中国邮政储蓄银行");
            dictionary1.Add("abc1003", "visa");
            dictionary1.Add("abc1004", "master");
            AliBankCodes = dictionary1;
            partner = "";
            seller_email = "";
            key = "";
            input_charset = "utf-8";
            sign_type = "MD5";
        }

        // Properties
        public static string Partner { get; set; }

        public static string Seller_email { get; set; }

        public static string Key { get; set; }

        public static string Input_charset =>
            input_charset;

        public static string Sign_type =>
            sign_type;
    }


}
