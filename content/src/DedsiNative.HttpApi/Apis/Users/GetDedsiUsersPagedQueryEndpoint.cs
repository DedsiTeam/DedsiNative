using DedsiNative.DedsiUsers.Operations;
using Microsoft.AspNetCore.Mvc;

namespace DedsiNative.Apis.Users;

public static partial class DedsiUserEndpoints
{
    /// <summary>
    /// 映射分页查询用户端点
    /// </summary>
    /// <param name="group">端点组</param>
    public static void MapGetDedsiUsersPagedQueryEndpoint(this RouteGroupBuilder group)
    {
        group
            .MapPost("/paged", (
                [FromBody] DedsiUserPagedQueryInputDto input,
                [FromServices] ConditionalQueryOperation conditionalQueryOperation,
                CancellationToken cancellationToken) =>
            {
                input.IsPaged = true;
                return conditionalQueryOperation.ExecuteAsync(input, cancellationToken);
            })
            .WithName("GetDedsiUsersPagedQuery")
            .WithSummary("分页查询用户")
            .WithDescription("根据条件分页查询用户列表");
    }
}
