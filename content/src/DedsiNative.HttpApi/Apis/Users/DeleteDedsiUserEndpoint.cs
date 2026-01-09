using DedsiNative.DedsiUsers.Operations;
using Microsoft.AspNetCore.Mvc;

namespace DedsiNative.Apis.Users;

public static partial class DedsiUserEndpoints
{
    /// <summary>
    /// 映射删除用户端点
    /// </summary>
    /// <param name="group">端点组</param>
    public static void MapDeleteDedsiUserEndpoint(this RouteGroupBuilder group)
    {
        group
            .MapDelete("/{id}", (
                [FromRoute] string id,
                [FromServices] DeleteDedsiUserOperation operation,
                CancellationToken cancellationToken) =>
            {
                var input = new DeleteDedsiUserInputDto(id);
                return operation.ExecuteAsync(input, cancellationToken);
            })
            .WithName("DeleteDedsiUser")
            .WithSummary("删除 Dedsi 用户")
            .WithDescription("根据ID删除指定的 Dedsi 用户");
    }
}
