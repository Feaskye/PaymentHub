using PaymentHub.WeChatPayCore.Parameter;
using PaymentHub.WeChatPayCore.Request;
using PaymentHub.WeChatPayCore.Response;
using PaymentHub.WeChatPayCore.Result;
using PaymentHub.WeChatPayCore.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace PaymentHub.WeChatPayCore
{
    public class WeChatPayClient
    {

        // Fields
        private static readonly string _DeviceInfo = "WEB";
        private static int _OrderExpireHour = 2;
        private static string _OrderFeeType = "CNY";
        private static string _UnifiedorderUrl = "https://api.mch.weixin.qq.com/pay/unifiedorder";
        private static string _OrderQueryUrl = "https://api.mch.weixin.qq.com/pay/orderquery";
        private static string _WxPayToUser = "https://api.mch.weixin.qq.com/mmpaymkttransfers/promotion/transfers";
        private static string _HandlResponseFormat = "<xml><return_code>{0}</return_code><return_msg>{1}</return_msg></xml>";
        private string m_AppId;
        private string m_ApiKey;
        private string m_CertificatePath;
        private string m_CertificatePassword;

        // Methods
        public WeChatPayClient(string appId, string apiKey, string certificatePath = "", string certificatePassword = "")
        {
            this.m_AppId = appId;
            this.m_ApiKey = apiKey;
            this.m_CertificatePath = certificatePath;
            this.m_CertificatePassword = certificatePassword;
        }

        public CreateOrderResult CreateOrderAsync(CreateOrderParameter createOrderParameter)
        {
            string str;
            if (!this.ValidateParameter<CreateOrderParameter>(createOrderParameter, out str))
            {
                CreateOrderResult result1 = new CreateOrderResult();
                result1.ErrorMessage = str;
                return result1;
            }
            UnifiedOrderRequest request = new UnifiedOrderRequest
            {
                AppId = this.m_AppId,
                MerchantId = createOrderParameter.MerchantId,
                DeviceInfo = _DeviceInfo,
                NonceString = WeChatSignHelper.CreateRandomString(),
                Body = createOrderParameter.Body,
                Detail = createOrderParameter.Detail,
                Attach = createOrderParameter.Attach,
                OutTradeNo = createOrderParameter.OutTradeNo,
                FeeType = _OrderFeeType,
                TotalFee = (int)(createOrderParameter.TotalFee * 100.0),
                SpbllCreateIP = createOrderParameter.IP,
                TimeStart = DateTime.Now.ToString("yyyyMMddHHmmss"),
                TimeExpire = DateTime.Now.AddHours((double)_OrderExpireHour).ToString("yyyyMMddHHmmss"),
                NotifyUrl = createOrderParameter.NotifyUrl,
                TradeType = createOrderParameter.TradeType,
                ProductId = createOrderParameter.ProductId,
                OpenId = createOrderParameter.OpenId
            };
            UnifiedOrderResponse response = this.InvokeApiAsync<UnifiedOrderRequest, UnifiedOrderResponse>(_UnifiedorderUrl, request);
            if (!response.IsSuccess)
            {
                CreateOrderResult result3 = new CreateOrderResult();
                result3.ErrorCode = response.ErrCode;
                result3.ErrorMessage = response.ReturnMsg;
                return result3;
            }
            CreateOrderResult result2 = new CreateOrderResult();
            result2.IsSuccess = true;
            result2.CodeUrl = response.CodeUrl;
            result2.PrepayId = response.PrepayId;
            return result2;
        }

        public CreateWxPayToUserResult CreateWxPayToUser(CreateWxPayToUserParameter createOrderParameter)
        {
            string str;
            if (!this.ValidateParameter<CreateWxPayToUserParameter>(createOrderParameter, out str))
            {
                CreateWxPayToUserResult result1 = new CreateWxPayToUserResult();
                result1.ErrorMessage = str;
                return result1;
            }
            WxPayToUserRequest request = new WxPayToUserRequest
            {
                AppId = this.m_AppId,
                MerchantId = createOrderParameter.MerchantId,
                DeviceInfo = _DeviceInfo,
                NonceString = WeChatSignHelper.CreateRandomString(),
                OutTradeNo = createOrderParameter.OutTradeNo,
                Amount = (int)(createOrderParameter.Amount * 100.0),
                SpbllCreateIP = createOrderParameter.IP,
                Desc = createOrderParameter.Desc,
                CheckName = "NO_CHECK",
                ReUserName = createOrderParameter.ReUserName,
                OpenId = createOrderParameter.OpenId
            };
            if (!string.IsNullOrEmpty(request.ReUserName))
            {
                request.CheckName = "FORCE_CHECK";
            }
            WxPayToUserResponse response = this.InvokeApiWithCertificateAsync<WxPayToUserRequest, WxPayToUserResponse>(_WxPayToUser, request);
            if (response.IsSuccess)
            {
                CreateWxPayToUserResult result2 = new CreateWxPayToUserResult();
                result2.IsSuccess = true;
                return result2;
            }
            CreateWxPayToUserResult result3 = new CreateWxPayToUserResult();
            result3.ErrorCode = response.ErrCode;
            result3.ErrorMessage = response.ReturnMsg;
            return result3;
        }

        private string GetErrorMessage(OBaseResponse response, string url = "")
        {
            string str = $"ErrorCode:{response.ErrCode},ReturnMsg:{response.ReturnMsg},ErrCodeDesc:{response.ErrCodeDesc}";
            Trace.WriteLine($"WeChatPay_{url}:ErrorMessage:{str}");
            return str;
        }

        public static string GetHandleResponse(bool isSuccess, string messag = null) =>
            (!isSuccess ? string.Format(_HandlResponseFormat, "FAIL", messag) : string.Format(_HandlResponseFormat, "SUCCESS", "OK"));

        private X509Certificate2 GetMyX509Certificate(string certName)
        {
            X509Certificate2 certificate = null;
            X509Store store1 = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            store1.Open(OpenFlags.OpenExistingOnly);
            X509Certificate2Collection certificates = store1.Certificates.Find(X509FindType.FindBySubjectName, certName, false);
            if (certificates.Count > 0)
            {
                certificate = certificates[0];
            }
            return certificate;
        }

        private TResponse InvokeApiAsync<TRequest, TResponse>(string url, TRequest request) where TRequest : OBaseRequest where TResponse : OBaseResponse
        {
            using (HttpClient client = new HttpClient())
            {
                request.Sign = WeChatSignHelper.CreateSign<TRequest>(this.m_ApiKey, request);
                StringContent content = new StringContent(XmlSerializerHelper.GetXmlString<TRequest>(request));
                TResponse response = XmlSerializerHelper.FromXmlString<TResponse>(client.PostAsync(url, content).Result.Content.ReadAsStringAsync().Result);
                if (!response.IsSuccess)
                {
                    response.ReturnMsg = this.GetErrorMessage(response, url);
                }
                else if (!this.ValidateResponse<TResponse>(response))
                {
                    response.ResultCode = string.Empty;
                    response.ReturnMsg = "返回结果响应结果不正确";
                }
                return response;
            }
        }

        private TResponse InvokeApiWithCertificateAsync<TRequest, TResponse>(string url, TRequest request) where TRequest : OBaseRequest where TResponse : OBaseResponse
        {
            X509Certificate2 certificate = new X509Certificate2(this.m_CertificatePath, this.m_CertificatePassword, X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);
            var handler = new HttpClientHandler
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                SslProtocols = SslProtocols.Tls12,
                ServerCertificateCustomValidationCallback = (x, y, z, m) => true,
            };
            handler.ClientCertificates.Add(certificate);
            using (HttpClient client = new HttpClient(handler))
            {
                request.Sign = WeChatSignHelper.CreateSign<TRequest>(this.m_ApiKey, request);
                StringContent content = new StringContent(XmlSerializerHelper.GetXmlString<TRequest>(request));
                TResponse response = XmlSerializerHelper.FromXmlString<TResponse>(client.PostAsync(url, content).Result.Content.ReadAsStringAsync().Result);
                if (!response.IsSuccess)
                {
                    response.ReturnMsg = this.GetErrorMessage(response, url);
                }
                return response;
            }
        }

        public NotifyResult ParseNotifyResponse(string responseValue)
        {
            NotifyResponse response = XmlSerializerHelper.FromXmlString<NotifyResponse>(responseValue);
            if (!response.IsSuccess)
            {
                NotifyResult result1 = new NotifyResult();
                result1.ErrorCode = response.ErrCode;
                result1.ErrorMessage = this.GetErrorMessage(response, "ParseNotifyResponse");
                return result1;
            }
            if (!this.ValidateResponse<NotifyResponse>(response))
            {
                NotifyResult result2 = new NotifyResult();
                result2.ErrorMessage = "返回结果响应结果不正确";
                return result2;
            }
            NotifyResult result3 = new NotifyResult();
            result3.IsSuccess = true;
            result3.Attach = response.Attach;
            result3.OutTradeNo = response.OutTradeNo;
            result3.TimeEnd = response.TimeEnd;
            result3.TotalFee = ((double)response.TotalFee.GetValueOrDefault()) / 100.0;
            result3.TransactionId = response.TransactionId;
            result3.Bank = WeChatBankTypes.GetBank(response.BankType);
            return result3;
        }

        public QueryOrderResult QueryOrderAsync(QueryOrderParameter queryOrderParameter)
        {
            string str;
            if (!this.ValidateParameter<QueryOrderParameter>(queryOrderParameter, out str))
            {
                QueryOrderResult result1 = new QueryOrderResult();
                result1.ErrorMessage = str;
                return result1;
            }
            OrderQueryRequest request = new OrderQueryRequest
            {
                AppId = this.m_AppId,
                MerchantId = queryOrderParameter.MerchantId,
                NonceString = WeChatSignHelper.CreateRandomString(),
                OutTradeNo = queryOrderParameter.OutTradeNo
            };
            OrderQueryResponse response = this.InvokeApiAsync<OrderQueryRequest, OrderQueryResponse>(_OrderQueryUrl, request);
            if (!response.IsSuccess)
            {
                QueryOrderResult result3 = new QueryOrderResult();
                result3.ErrorCode = response.ErrCode;
                result3.ErrorMessage = response.ReturnMsg;
                return result3;
            }
            QueryOrderResult result2 = new QueryOrderResult();
            result2.IsSuccess = true;
            result2.EndTime = response.TimeEnd;
            result2.OutTradeNo = response.OutTradeNo;
            result2.TotalFee = ((double)response.TotalFee.GetValueOrDefault()) / 100.0;
            result2.TradeState = (TradeState)Enum.Parse(typeof(TradeState), response.TradeState, true);
            result2.TradeStateDescription = response.TradeStateDesc;
            result2.TransactionId = response.TransactionId;
            result2.Bank = WeChatBankTypes.GetBank(response.BankType);
            return result2;
        }

        private bool ValidateParameter<T>(T parameter, out string errorMessage)
        {
            ValidationContext context = new ValidationContext(parameter, null, null);
            List<ValidationResult> list = new List<ValidationResult>();
            if (Validator.TryValidateObject(parameter, context, list, true))
            {
                errorMessage = string.Empty;
                return true;
            }
            StringBuilder builder = new StringBuilder();
            foreach (ValidationResult result in list)
            {
                builder.AppendFormat("{0}:{1}", result.MemberNames.First(), result.ErrorMessage);
                builder.AppendLine();
            }
            errorMessage = builder.ToString();
            return false;
        }

        private bool ValidateResponse<T>(T response) where T : OBaseResponse
        {
            string str = WeChatSignHelper.CreateSign<T>(this.m_ApiKey, response);
            return response.Sign.Equals(str, StringComparison.OrdinalIgnoreCase);
        }
    }

}
