using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentHub.AlipayCore
{
    public class PaymentHubAlipay
    {
        //配置
        private static IConfigurationSection _config;
        //日志
        private static ILogger _logger;
        private RequestDelegate _next;
        public PaymentHubAlipay(RequestDelegate next,IConfigurationSection section, ILoggerFactory loggerFactory)
        {
            _next = next;
            _config = section;
            _logger = loggerFactory.CreateLogger<PaymentHubAlipay>();
        }

        public async Task Invoke(HttpContext httpContext)
        {
            await _next(httpContext);
        }

        public static AlipayClient AlipayClient
        {
            get {
                return AlipayClient
            }
        }


        //发起支付


        //回调方法

        //其他接口



    }
}
