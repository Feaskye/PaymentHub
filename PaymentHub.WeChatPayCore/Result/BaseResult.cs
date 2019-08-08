using PaymentHub.WeChatPayCore.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentHub.WeChatPayCore.Result
{
    public abstract class BaseResult
    {

        public bool IsSuccess { get;  set; }
        public string ErrorMessage { get;  set; }
        public string ErrorCode { get;  set; }
    }

    public sealed class CreateOrderResult : BaseResult
    {

        public string PrepayId { get;  set; }
        public string CodeUrl { get;  set; }
    }

    public sealed class CreateWxPayToUserResult : BaseResult
    {
    
    }

    public sealed class NotifyResult : BaseResult
    {

        public double TotalFee { get;  set; }
        public string TransactionId { get;  set; }
        public string OutTradeNo { get;  set; }
        public string Attach { get;  set; }
        public string TimeEnd { get;  set; }
        public string Bank { get;  set; }
    }

    public sealed class QueryOrderResult : BaseResult
    {

        // Properties
        public TradeState TradeState { get;  set; }
        public double TotalFee { get;  set; }
        public string EndTime { get;  set; }
        public string TradeStateDescription { get;  set; }
        public string OutTradeNo { get;  set; }
        public string TransactionId { get;  set; }
        public string Bank { get;  set; }
    }

}
