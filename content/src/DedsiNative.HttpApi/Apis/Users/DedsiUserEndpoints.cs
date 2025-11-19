using DedsiNative.DedsiUsers.Operations;
using Microsoft.AspNetCore.Mvc;

namespace DedsiNative.Apis.Users;

/// <summary>
/// DedsiUser 相关 API 端点
/// </summary>
public static class DedsiUserEndpoints
{
    /// <summary>
    /// 映射 DedsiUser 相关的所有端点
    /// </summary>
    /// <param name="endpoints">端点路由构建器</param>
    public static void MapDedsiUserEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints
            .MapGroup("/api/dedsi-users")
            .WithTags("DedsiUser");

        // 创建用户
        group
        .MapPost("/", (
            [FromBody] CreateDedsiUserInputDto input,
            [FromServices] CreateDedsiUserOperation operation,
            CancellationToken cancellationToken) => operation.ExecuteAsync(input, cancellationToken))
        .WithName("CreateDedsiUser")
        .WithSummary("创建 Dedsi 用户")
        .WithDescription("创建一个新的 Dedsi 用户");

        // 删除用户
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

        // 更新用户
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

        // 根据 ID 获取用户
        group
        .MapGet("/{id}", (
            [FromRoute] string id,
            [FromServices] GetDedsiUserOperation getDedsiUserOperation,
            CancellationToken cancellationToken) => getDedsiUserOperation.ExecuteAsync(id, cancellationToken))
        .WithName("GetDedsiUserById")
        .WithSummary("根据 ID 获取用户")
        .WithDescription("通过用户 ID 获取单个用户信息");

        // 分页查询用户
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