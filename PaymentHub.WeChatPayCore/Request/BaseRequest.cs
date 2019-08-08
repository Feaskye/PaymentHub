using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace PaymentHub.WeChatPayCore.Request
{
    [XmlRoot("xml")]
    public class BaseRequest : OBaseRequest
    {

        [XmlElement(ElementName = "appid"), Required]
        public string AppId { get;  set; }
        [XmlElement(ElementName = "mch_id"), Required]
        public string MerchantId { get;  set; }
    }

    [XmlRoot("xml")]
    public class OBaseRequest
    {

        [XmlElement(ElementName = "nonce_str"), Required]
        public string NonceString { get;  set; }
        [XmlElement(ElementName = "sign"), Required]
        public string Sign { get;  set; }
    }

    [XmlRoot("xml")]
    public class OrderQueryRequest : BaseRequest
    {

        [XmlElement(ElementName = "out_trade_no"), Required]
        public string OutTradeNo { get;  set; }
    }

    [XmlRoot(ElementName = "xml")]
    public sealed class UnifiedOrderRequest : BaseRequest
    {

        [XmlElement(ElementName = "device_info")]
        public string DeviceInfo { get;  set; }
        [XmlElement(ElementName = "body"), Required]
        public string Body { get;  set; }
        [XmlElement(ElementName = "detail")]
        public string Detail { get;  set; }
        [XmlElement(ElementName = "attach")]
        public string Attach { get;  set; }
        [XmlElement(ElementName = "out_trade_no"), Required]
        public string OutTradeNo { get;  set; }
        [XmlElement(ElementName = "fee_type")]
        public string FeeType { get;  set; }
        [XmlElement(ElementName = "total_fee"), Required]
        public int TotalFee { get;  set; }
        [XmlElement(ElementName = "spbill_create_ip"), Required]
        public string SpbllCreateIP { get;  set; }
        [XmlElement(ElementName = "time_start")]
        public string TimeStart { get;  set; }
        [XmlElement(ElementName = "time_expire")]
        public string TimeExpire { get;  set; }
        [XmlElement(ElementName = "goods_tag")]
        public string GoodsTag { get;  set; }
        [XmlElement(ElementName = "notify_url"), Required]
        public string NotifyUrl { get;  set; }
        [XmlElement(ElementName = "trade_type"), Required]
        public string TradeType { get;  set; }
        [XmlElement(ElementName = "product_id")]
        public string ProductId { get;  set; }
        [XmlElement(ElementName = "limit_pay")]
        public string LimitPay { get;  set; }
        [XmlElement(ElementName = "openid")]
        public string OpenId { get;  set; }
    }

    [XmlRoot(ElementName = "xml")]
    public sealed class WxPayToUserRequest : OBaseRequest
    {

        [XmlElement(ElementName = "mch_appid"), Required]
        public string AppId { get;  set; }
        [XmlElement(ElementName = "mchid"), Required]
        public string MerchantId { get;  set; }
        [XmlElement(ElementName = "device_info")]
        public string DeviceInfo { get;  set; }
        [XmlElement(ElementName = "partner_trade_no"), Required]
        public string OutTradeNo { get;  set; }
        [XmlElement(ElementName = "openid"), Required]
        public string OpenId { get;  set; }
        [XmlElement(ElementName = "check_name"), Required]
        public string CheckName { get;  set; }
        [XmlElement(ElementName = "re_user_name")]
        public string ReUserName { get;  set; }
        [XmlElement(ElementName = "amount"), Required]
        public int Amount { get;  set; }
        [XmlElement(ElementName = "desc"), Required]
        public string Desc { get;  set; }
        [XmlElement(ElementName = "spbill_create_ip"), Required]
        public string SpbllCreateIP { get;  set; }
    }
}
