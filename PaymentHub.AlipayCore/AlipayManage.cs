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

        public string DirectPay(DirectPay payData)
        {
            return this.PayAndRedirect(payData.BuildRequestData());
        }

        private string PayAndRedirect(SortedDictionary<string, string> sParaTemp)
        {
            return AlipaySubmit.BuildRequest(sParaTemp, "get", "确认");
            //PaymentHubAlipay.HttpContext.Response.WriteAsync(s);
        }

        public string TradeCreateByBuyer(TradeCreateByBuyer payData)
        {
            return this.PayAndRedirect(payData.BuildRequestData());
        }
    }

}
