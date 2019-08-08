using PaymentHub.AlipayCore.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentHub.AlipayCore.RequestData
{
    public class TradeCreateByBuyer : PayBaseInfo
    {
        // Methods
        public override SortedDictionary<string, string> BuildRequestData()
        {
            base.BuildRequestData();
            base.sParaTemp.Add("seller_id", AlipayConfig.Partner);
            return base.sParaTemp;
        }
    }

}
