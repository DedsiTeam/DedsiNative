using DedsiAi;
using DedsiNative.Apis;
using DedsiNative.EntityFrameworkCores;
using DedsiNative.Middleware;
using Scalar.AspNetCore;
using Serilog;
using Serilog.Events;
using System.Reflection;

// 配置 Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Async(c => c.File(path:"Logs/logs.txt", rollingInterval:RollingInterval.Hour, retainedFileCountLimit: null))
    .WriteTo.Async(c => c.Console())
    .CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host
    .UseSerilog((context, services, loggerConfiguration) =>
    {
        loggerConfiguration
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Async(c => c.File(path:"Logs/logs.txt", rollingInterval:RollingInterval.Hour, retainedFileCountLimit: null))
            .WriteTo.Async(c => c.Console());
    });

// 依赖注入
builder.Services
    .Scan(scan => scan
    .FromAssemblies(
        Assembly.Load("DedsiNative.HttpApi"),
        Assembly.Load("DedsiNative.Application"),
        Assembly.Load("DedsiNative.Infrastructure")
    )
    .AddClasses(classes => classes.AssignableTo<IDedsiNativeOperation>())
    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Repository")))
    .AsImplementedInterfaces()
    .WithTransientLifetime()
);

// 添加 EF Core MySQL 支持
builder.Services.AddMySqlDb(builder.Configuration);

builder.Services.AddOpenApi();

var app = builder.Build();

// 添加 Serilog 请求日志记录
app.UseSerilogRequestLogging(options =>
{
    options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";
    options.GetLevel = (httpContext, elapsed, ex) => ex != null
        ? Serilog.Events.LogEventLevel.Error 
        : httpContext.Response.StatusCode > 499 
            ? Serilog.Events.LogEventLevel.Error
            : elapsed > 2000
                ? Serilog.Events.LogEventLevel.Warning
                : Serilog.Events.LogEventLevel.Information;
    options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
    {
        diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
        diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
        diagnosticContext.Set("UserAgent", httpContext.Request.Headers["User-Agent"]);
    };
});

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    
    app.MapScalarApiReference(options =>
    {
        options.Title = "DedsiNative API";
        options.WithDefaultHttpClient(ScalarTarget.JavaScript, ScalarClient.Axios);
    });
}

app.UseHttpsRedirection();

// 添加全局异常处理中间件
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

// 注册 API 端点
app.MapDedsiNativeEndpoints();

try
{
    Log.Information("正在启动 DedsiNative API...");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "应用程序启动失败");
}
finally
{
    Log.CloseAndFlush();
}
