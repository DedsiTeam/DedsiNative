using DedsiNative.DedsiUsers.Operations;
using Microsoft.AspNetCore.Mvc;

namespace DedsiNative.Apis.Users;

public static partial class DedsiUserEndpoints
{
    /// <summary>
    /// 映射根据 ID 获取用户端点
    /// </summary>
    /// <param name="group">端点组</param>
    public static void MapGetDedsiUserByIdEndpoint(this RouteGroupBuilder group)
    {
        group
            .MapGet("/{id}", (
                [FromRoute] string id,
                [FromServices] GetDedsiUserOperation getDedsiUserOperation,
                CancellationToken cancellationToken) => getDedsiUserOperation.ExecuteAsync(id, cancellationToken))
            .WithName("GetDedsiUserById")
            .WithSummary("根据 ID 获取用户")
            .WithDescription("通过用户 ID 获取单个用户信息");
    }
}
