using PaymentHub.AlipayCore.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentHub.AlipayCore.RequestData
{
    public class PayBaseInfo
    {
        // Fields
        private string _service = AlipayConfig.DefaultDirectPayService;
        protected SortedDictionary<string, string> sParaTemp = null;
    //    private bool IsMobilePay = false;
    //    private string SellerEmail;
    //public string PaymentType = "1";
    //    private string NotifyUrl;
    //    private string ReturnUrl;
    //    private string OutTradeNo;
    //    private string Subject;
    //    private string TotalFee;
    //    private string Body;
    //    private string ShowUrl;

    // Methods
    public virtual SortedDictionary<string, string> BuildRequestData()
        {
            this.sParaTemp = new SortedDictionary<string, string>();
            this.sParaTemp.Add("service", this.Service);
            this.sParaTemp.Add("body", this.Body);
            this.sParaTemp.Add("partner", AlipayConfig.Partner);
            this.sParaTemp.Add("_input_charset", AlipayConfig.Input_charset.ToLower());
            this.sParaTemp.Add("payment_type", "1");
            this.sParaTemp.Add("notify_url", this.NotifyUrl);
            this.sParaTemp.Add("return_url", this.ReturnUrl);
            this.sParaTemp.Add("out_trade_no", this.OutTradeNo);
            this.sParaTemp.Add("subject", this.Subject);
            this.sParaTemp.Add("total_fee", this.TotalFee);
            this.sParaTemp.Add("show_url", this.ShowUrl);
            return this.sParaTemp;
        }

        // Properties
        public bool IsMobilePay { get; set; }

        public string SellerEmail { get; set; }

        public string Partner =>
            AlipayConfig.Partner;

        public static string Input_charset =>
            AlipayConfig.Input_charset.ToLower();

        public string NotifyUrl { get; set; }

        public string ReturnUrl { get; set; }

        public string OutTradeNo { get; set; }

        public string Subject { get; set; }

        public string TotalFee { get; set; }

        public string Body { get; set; }

        protected string Service
        {
            get
            {
                if (this.IsMobilePay && (AlipayConfig.WapDirectPayService != this._service))
                {
                    this._service = AlipayConfig.WapDirectPayService;
                }
                return this._service;
            }
        }

        public string ShowUrl { get; set; }
    }

}
