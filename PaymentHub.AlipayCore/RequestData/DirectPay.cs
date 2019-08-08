using PaymentHub.AlipayCore.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentHub.AlipayCore.RequestData
{
    public class DirectPay : PayBaseInfo
    {

        public override SortedDictionary<string, string> BuildRequestData()
        {
            base.BuildRequestData();
            base.sParaTemp.Add("seller_email", AlipayConfig.Seller_email);
            base.sParaTemp.Add("paymethod", "bankPay");
            base.sParaTemp.Add("defaultbank", this.DefaultBank);
            base.sParaTemp.Add("anti_phishing_key", "");
            base.sParaTemp.Add("exter_invoke_ip", "");
            return base.sParaTemp;
        }

        public string DefaultBank { get; set; }
    }

}
