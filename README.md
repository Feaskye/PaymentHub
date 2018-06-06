# PaymentHub
Collect payments sdk，such as Alipay、WechatPay、UnionPay、LaCodPay(Pos) and so on，

Provide .Net/.NetCore

Now, Alipay for .net core middleware
coding ...

#依赖说明
支付类库会以中间件Nuget包的形式存在
IAppBuilder安装依赖Microsoft.AspNetCore.Http.Abstractions命令：
Install-Package Microsoft.AspNetCore.Http.Abstractions -Version 2.1.0

IConfiguration配置依赖
Install-Package Microsoft.Extensions.Configuration.Abstractions -Version 2.1.0

ILogger
Install-Package Microsoft.Extensions.Logging -Version 2.1.0




