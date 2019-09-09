using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace PaymentHub.WeChatPayCore.Response
{
    [XmlRoot("xml")]
    public abstract class BaseResponse : OBaseResponse
    {

        [XmlElement(ElementName = "appid")]
        public string AppId { get;  set; }
        [XmlElement(ElementName = "mch_id")]
        public string MerchantId { get;  set; }
    }

    [XmlRoot("xml")]
    public class NotifyResponse : BaseResponse
    {

        [XmlElement(ElementName = "device_info")]
        public string DeviceInfo { get;  set; }
        [XmlElement(ElementName = "trade_type")]
        public string TradeType { get;  set; }
        [XmlElement(ElementName = "openid")]
        public string OpenId { get;  set; }
        [XmlElement(ElementName = "is_subscribe")]
        public string IsSubscribe { get;  set; }
        [XmlElement(ElementName = "bank_type")]
        public string BankType { get;  set; }
        [XmlElement(ElementName = "total_fee")]
        public int? TotalFee { get;  set; }
        [XmlElement(ElementName = "fee_type")]
        public string FeeType { get;  set; }
        [XmlElement(ElementName = "cash_fee")]
        public int? CashFee { get;  set; }
        [XmlElement(ElementName = "cash_fee_type")]
        public string CashFeeType { get;  set; }
        [XmlElement(ElementName = "transaction_id")]
        public string TransactionId { get;  set; }
        [XmlElement(ElementName = "out_trade_no")]
        public string OutTradeNo { get;  set; }
        [XmlElement(ElementName = "attach")]
        public string Attach { get;  set; }
        [XmlElement(ElementName = "time_end")]
        public string TimeEnd { get;  set; }
    }

    [XmlRoot("xml")]
    public abstract class OBaseResponse
    {
        // Fields
        private static string _SuccessCode;
        
        [XmlElement(ElementName = "return_code")]
        public string ReturnCode { get;  set; }
        [XmlElement(ElementName = "return_msg")]
        public string ReturnMsg { get;  set; }
        [XmlElement(ElementName = "nonce_str")]
        public string NonceString { get;  set; }
        [XmlElement(ElementName = "sign")]
        public string Sign { get;  set; }
        [XmlElement(ElementName = "result_code")]
        public string ResultCode { get;  set; }
        [XmlElement(ElementName = "err_code")]
        public string ErrCode { get;  set; }
        [XmlElement(ElementName = "err_code_des")]
        public string ErrCodeDesc { get;  set; }
        [XmlIgnore]
        public bool IsSuccess {
            get
            {
                return ResultCode == "SUCCESS" && ReturnCode == "SUCCESS";
            }
        }
    }

    [XmlRoot("xml")]
    public class OrderQueryResponse : BaseResponse
    {

        [XmlElement(ElementName = "device_info")]
        public string DeviceInfo { get;  set; }
        [XmlElement(ElementName = "trade_type")]
        public string TradeType { get;  set; }
        [XmlElement(ElementName = "openid")]
        public string OpenId { get;  set; }
        [XmlElement(ElementName = "is_subscribe")]
        public string IsSubscribe { get;  set; }
        [XmlElement(ElementName = "trade_state")]
        public string TradeState { get;  set; }
        [XmlElement(ElementName = "bank_type")]
        public string BankType { get;  set; }
        [XmlElement(ElementName = "total_fee")]
        public int? TotalFee { get;  set; }
        [XmlElement(ElementName = "fee_type")]
        public string FeeType { get;  set; }
        [XmlElement(ElementName = "cash_fee")]
        public int? CashFee { get;  set; }
        [XmlElement(ElementName = "cash_fee_type")]
        public string CashFeeType { get;  set; }
        [XmlElement(ElementName = "transaction_id")]
        public string TransactionId { get;  set; }
        [XmlElement(ElementName = "out_trade_no")]
        public string OutTradeNo { get;  set; }
        [XmlElement(ElementName = "attach")]
        public string Attach { get;  set; }
        [XmlElement(ElementName = "time_end")]
        public string TimeEnd { get;  set; }
        [XmlElement(ElementName = "trade_state_desc")]
        public string TradeStateDesc { get;  set; }
        [XmlElement(ElementName = "coupon_fee")]
        public string CouponFee { get;  set; }
        [XmlElement(ElementName = "coupon_count")]
        public string CouponCount { get;  set; }
        [XmlElement(ElementName = "coupon_id_0")]
        public string CouponId0 { get;  set; }
        [XmlElement(ElementName = "coupon_fee_0")]
        public string CouponFee0 { get;  set; }
        [XmlElement(ElementName = "coupon_id_1")]
        public string CouponId1 { get;  set; }
        [XmlElement(ElementName = "coupon_fee_1")]
        public string CouponFee1 { get;  set; }
        [XmlElement(ElementName = "coupon_id_2")]
        public string CouponId2 { get;  set; }
        [XmlElement(ElementName = "coupon_fee_2")]
        public string CouponFee2 { get;  set; }
    }

    public enum TradeState
    {
        SUCCESS = 1,
        REFUND = 2,
        NOTPAY = 3,
        CLOSED = 4,
        REVOKED = 5,
        USERPAYING = 6,
        PAYERROR = 7
    }

    [XmlRoot("xml")]
    public sealed class UnifiedOrderResponse : BaseResponse
    {
 
        [XmlElement(ElementName = "device_info")]
        public string DeviceInfo { get;  set; }
        [XmlElement(ElementName = "trade_type")]
        public string TradeType { get;  set; }
        [XmlElement(ElementName = "prepay_id")]
        public string PrepayId { get;  set; }
        [XmlElement(ElementName = "code_url")]
        public string CodeUrl { get;  set; }
    }

    [XmlRoot("xml")]
    public sealed class WxPayToUserResponse : OBaseResponse
    {

        // Properties
        [XmlElement(ElementName = "mch_appid")]
        public string AppId { get;  set; }
        [XmlElement(ElementName = "mchid")]
        public string MerchantId { get;  set; }
        [XmlElement(ElementName = "device_info")]
        public string DeviceInfo { get;  set; }
        [XmlElement(ElementName = "partner_trade_no")]
        public string TradeNo { get;  set; }
        [XmlElement(ElementName = "payment_no")]
        public string PaymentNo { get;  set; }
        [XmlElement(ElementName = "payment_time")]
        public string PaymentTime { get;  set; }
    }

}
