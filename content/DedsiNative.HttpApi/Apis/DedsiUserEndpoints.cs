using DedsiNative.DedsiUsers.Operations;
using Microsoft.AspNetCore.Mvc;

namespace DedsiNative.Apis.DedsiUsers;

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
        var group = endpoints.MapGroup("/api/dedsi-users")
            .WithTags("DedsiUser");

        // 创建用户
        group.MapPost("/", CreateDedsiUser)
            .WithName("CreateDedsiUser")
            .WithSummary("创建 Dedsi 用户")
            .WithDescription("创建一个新的 Dedsi 用户");

        // 删除用户
        group.MapDelete("/{id}", DeleteDedsiUser)
            .WithName("DeleteDedsiUser")
            .WithSummary("删除 Dedsi 用户")
            .WithDescription("根据ID删除指定的 Dedsi 用户");

        // 更新用户
        group.MapPut("/{id}", UpdateDedsiUser)
            .WithName("UpdateDedsiUser")
            .WithSummary("更新 Dedsi 用户")
            .WithDescription("更新指定的 Dedsi 用户信息");

        // 根据 ID 获取用户
        group.MapGet("/{id}", GetDedsiUserById)
            .WithName("GetDedsiUserById")
            .WithSummary("根据 ID 获取用户")
            .WithDescription("通过用户 ID 获取单个用户信息");

        // 分页查询用户
        group.MapPost("/paged", GetDedsiUsersPagedQuery)
            .WithName("GetDedsiUsersPagedQuery")
            .WithSummary("分页查询用户")
            .WithDescription("根据条件分页查询用户列表");
    }

    /// <summary>
    /// 创建 Dedsi 用户
    /// </summary>
    /// <param name="input">创建用户的输入数据</param>
    /// <param name="operation">创建用户操作实例</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>操作结果</returns>
    private static Task<bool> CreateDedsiUser(
        [FromBody] CreateDedsiUserInputDto input,
        [FromServices] CreateDedsiUserOperation operation,
        CancellationToken cancellationToken)
    {
        return operation.ExecuteAsync(input, cancellationToken);
    }

    /// <summary>
    /// 删除 Dedsi 用户
    /// </summary>
    /// <param name="id">用户ID</param>
    /// <param name="operation">删除用户操作实例</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>操作结果</returns>
    private static Task<bool> DeleteDedsiUser(
        [FromRoute] string id,
        [FromServices] DeleteDedsiUserOperation operation,
        CancellationToken cancellationToken)
    {
        var input = new DeleteDedsiUserInputDto(id);
        return operation.ExecuteAsync(input, cancellationToken);
    }

    /// <summary>
    /// 更新 Dedsi 用户
    /// </summary>
    /// <param name="id">用户ID</param>
    /// <param name="input">更新用户的信息</param>
    /// <param name="operation">更新用户操作实例</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>操作结果</returns>
    private static Task<bool> UpdateDedsiUser(
        [FromRoute] string id,
        [FromBody] UpdateDedsiUserInputDto input,
        [FromServices] UpdateDedsiUserOperation operation,
        CancellationToken cancellationToken)
    {
        return operation.ExecuteAsync(input, cancellationToken);
    }

    /// <summary>
    /// 根据 ID 获取用户
    /// </summary>
    /// <param name="id">用户 ID</param>
    /// <param name="getDedsiUserOperation">用户查询服务</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>用户信息</returns>
    private static Task<DedsiUserDto> GetDedsiUserById(
        [FromRoute] string id,
        [FromServices] GetDedsiUserOperation getDedsiUserOperation,
        CancellationToken cancellationToken)
    {
        return getDedsiUserOperation.ExecuteAsync(id, cancellationToken);
    }

    /// <summary>
    /// 分页查询用户
    /// </summary>
    /// <param name="input">入参</param>
    /// <param name="conditionalQueryOperation">用户查询服务</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>分页查询结果</returns>
    private static Task<DedsiUserPagedQueryResultDto> GetDedsiUsersPagedQuery(
        [FromBody] DedsiUserPagedQueryInputDto input,
        [FromServices] ConditionalQueryOperation conditionalQueryOperation,
        CancellationToken cancellationToken)
    {
        input.IsPaged = true;
        return conditionalQueryOperation.ExecuteAsync(input, cancellationToken);
    }
}