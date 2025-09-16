using System.Net;
using System.Text.Json;
using DedsiNative.HttpApi.Models;

namespace DedsiNative.Middleware;

/// <summary>
/// 全局异常处理中间件
/// </summary>
public class GlobalExceptionHandlingMiddleware(
    RequestDelegate next,
    ILogger<GlobalExceptionHandlingMiddleware> logger,
    IWebHostEnvironment environment)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "发生未处理的异常: {Message}", exception.Message);
            await HandleExceptionAsync(context, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        
        var response = new ErrorResponse
        {
            Path = context.Request.Path,
            TraceId = context.TraceIdentifier
        };

        switch (exception)
        {
            case ArgumentNullException:
                response.Code = "ARGUMENT_NULL";
                response.Message = "参数不能为空";
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;

            case ArgumentException:
                response.Code = "INVALID_ARGUMENT";
                response.Message = "参数无效";
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;

            case UnauthorizedAccessException:
                response.Code = "UNAUTHORIZED";
                response.Message = "未授权访问";
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                break;

            case KeyNotFoundException:
                response.Code = "NOT_FOUND";
                response.Message = "请求的资源不存在";
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                break;

            case TimeoutException:
                response.Code = "TIMEOUT";
                response.Message = "请求超时";
                context.Response.StatusCode = (int)HttpStatusCode.RequestTimeout;
                break;

            case InvalidOperationException:
                response.Code = "INVALID_OPERATION";
                response.Message = "操作无效";
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;

            case NotImplementedException:
                response.Code = "NOT_IMPLEMENTED";
                response.Message = "功能暂未实现";
                context.Response.StatusCode = (int)HttpStatusCode.NotImplemented;
                break;

            default:
                response.Code = "INTERNAL_ERROR";
                response.Message = "服务器内部错误";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                break;
        }

        // 在开发环境下显示详细错误信息
        if (environment.IsDevelopment())
        {
            response.Details = exception.ToString();
        }

        var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        });

        await context.Response.WriteAsync(jsonResponse);
    }
}
