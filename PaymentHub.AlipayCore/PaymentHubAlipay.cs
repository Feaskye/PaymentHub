using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentHub.AlipayCore
{
    public class PaymentHubAlipay
    {
        //日志
        private RequestDelegate _next;
        private static IHttpContextAccessor _accessor;
        public static HttpContext HttpContext = _accessor.HttpContext;

        public PaymentHubAlipay(RequestDelegate next,
            IHttpContextAccessor accessor)
        {
            _next = next;
            _accessor = accessor;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            await _next(httpContext);
        }

        //public static AlipayClient AlipayClient
        //{
        //    get {
        //        return AlipayClient
        //    }
        //}


        //发起支付


        //回调方法

        //其他接口



    }



    /// <summary>
    /// CoreProvider 上下文支持
    /// </summary>
    public static class PaymentHubAlipayExtensions
    {

        public static void AddPaymentHubAlipay(this IServiceCollection services)
        {
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //add configuration
            //CoreContextProvider.Configuration =configuration;
            //去除此方式 本页已有ServiceProvider = serviceProvider.CreateScope().ServiceProvider;取容器写法
            //CoreContextProvider.ServiceProvider = serviceProvider;
        }

        /// <summary>
        /// 公用上下文
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UsePaymentHubAlipay(this IApplicationBuilder app)
        {
            //var httpContextAccessor = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
            //var hostingEnvironment = app.ApplicationServices.GetRequiredService<IHostingEnvironment>();

            //CoreContextProvider.Configure(httpContextAccessor, hostingEnvironment);
            app.UseMiddleware<PaymentHubAlipay>();
            return app;
        }
    }


}
