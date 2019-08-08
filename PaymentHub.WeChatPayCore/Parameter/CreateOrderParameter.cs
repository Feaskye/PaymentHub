using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace PaymentHub.WeChatPayCore.Parameter
{
    public sealed class CreateOrderParameter
    {
        // Fields
        private string m_TradeType;
        

        // Properties
        [Required]
        public string MerchantId { get;  set; }
        [Required]
        public string Body { get;  set; }
        public string Detail { get;  set; }
        public string Attach { get;  set; }
        [Required]
        public string OutTradeNo { get;  set; }
        [Required]
        public double TotalFee { get;  set; }
        [Required]
        public string IP { get;  set; }
        public string GoodsTag { get;  set; }
        [Required]
        public string NotifyUrl { get;  set; }
        [Required]
        public string TradeType { get; set; }
        [Required]
        public string ProductId { get;  set; }
        [XmlElement(ElementName = "limit_pay")]
        public string LimitPay { get;  set; }
        [XmlElement(ElementName = "openid")]
        public string OpenId { get;  set; }
    }

    public sealed class CreateWxPayToUserParameter
    {
 
        [Required]
        public string MerchantId { get;  set; }
        [Required]
        public string OutTradeNo { get;  set; }
        [Required]
        public double Amount { get;  set; }
        [Required]
        public string IP { get;  set; }
        [Required]
        public string OpenId { get;  set; }
        [Required]
        public string Desc { get;  set; }
        public string ReUserName { get;  set; }
    }

    public sealed class QueryOrderParameter
    {
        
        [Required]
        public string MerchantId { get;  set; }
        [Required]
        public string OutTradeNo { get;  set; }
    }

}
