using DedsiNative.DedsiUsers.Operations;
using Microsoft.AspNetCore.Mvc;

namespace DedsiNative.Apis.Users;

public static partial class DedsiUserEndpoints
{
    /// <summary>
    /// 映射创建用户端点
    /// </summary>
    /// <param name="group">端点组</param>
    public static void MapCreateDedsiUserEndpoint(this RouteGroupBuilder group)
    {
        group
            .MapPost("/", (
                [FromBody] CreateDedsiUserInputDto input,
                [FromServices] CreateDedsiUserOperation operation,
                CancellationToken cancellationToken) => operation.ExecuteAsync(input, cancellationToken))
            .WithName("CreateDedsiUser")
            .WithSummary("创建 Dedsi 用户")
            .WithDescription("创建一个新的 Dedsi 用户");
    }
}
