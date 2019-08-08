using Microsoft.AspNetCore.Http;
using PaymentHub.AlipayCore.Common;
using PaymentHub.AlipayCore.RequestData;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentHub.AlipayCore
{

    public sealed class AlipayManage
    {
        public AlipayManage(string partner, string seller_email, string key)
        {
            AlipayConfig.Partner = partner;
            AlipayConfig.Seller_email = seller_email;
            AlipayConfig.Key = key;
        }

        public void DirectPay(DirectPay payData)
        {
            this.PayAndRedirect(payData.BuildRequestData());
        }

        private void PayAndRedirect(SortedDictionary<string, string> sParaTemp)
        {
            string s = AlipaySubmit.BuildRequest(sParaTemp, "get", "确认");
            PaymentHubAlipay.HttpContext.Response.WriteAsync(s);
        }

        public void TradeCreateByBuyer(TradeCreateByBuyer payData)
        {
            this.PayAndRedirect(payData.BuildRequestData());
        }
    }

}
