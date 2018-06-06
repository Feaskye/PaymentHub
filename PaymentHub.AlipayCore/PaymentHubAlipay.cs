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
        private IConfigurationSection _config;
        //日志
        private ILogger _logger;
        public PaymentHubAlipay(IConfigurationSection section, ILoggerFactory loggerFactory)
        {
            _config = section;
            _logger = loggerFactory.CreateLogger<PaymentHubAlipay>();
        }

        public async Task Invoke(HttpContext httpContext)
        {

        }

        //发起支付


        //回调方法

        //其他接口



    }
}
