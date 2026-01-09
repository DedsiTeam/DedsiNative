using DedsiNative.DedsiUsers.Operations;
using Microsoft.AspNetCore.Mvc;

namespace DedsiNative.Apis.Users;

public static partial class DedsiUserEndpoints
{
    /// <summary>
    /// 映射更新用户端点
    /// </summary>
    /// <param name="group">端点组</param>
    public static void MapUpdateDedsiUserEndpoint(this RouteGroupBuilder group)
    {
        group
            .MapPut("/{id}", (
                [FromRoute] string id,
                [FromBody] UpdateDedsiUserInputDto input,
                [FromServices] UpdateDedsiUserOperation operation,
                CancellationToken cancellationToken) =>
            {
                // 以路由中的 id 为准，覆盖请求体中的 Id
                var dto = input with { Id = id };
                return operation.ExecuteAsync(dto, cancellationToken);
            })
            .WithName("UpdateDedsiUser")
            .WithSummary("更新 Dedsi 用户")
            .WithDescription("更新指定的 Dedsi 用户信息");
    }
}
