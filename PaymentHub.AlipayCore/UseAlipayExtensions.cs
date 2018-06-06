using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentHub.AlipayCore
{
    public class UseAlipayExtensions
    {
        public static IApplicationBuilder UseAlipay(IApplicationBuilder app,IConfigurationSection section)
        {
            app.UseMiddleware<PaymentHubAlipay>();
            return app;
        }
    }
}
