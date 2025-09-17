using DedsiNative.DedsiUsers.Dtos;
using DedsiNative.DedsiUsers.Operations;
using Microsoft.AspNetCore.Mvc;

namespace DedsiNative.Apis.DedsiUsers;

/// <summary>
/// 分页查询用户 API
/// </summary>
public static class GetDedsiUsersPagedQueryApi
{
    /// <summary>
    /// 配置分页查询用户端点
    /// </summary>
    /// <param name="group">路由组</param>
    public static void MapGetDedsiUsersPagedQuery(this RouteGroupBuilder group)
    {
        group.MapPost("/paged", GetDedsiUsersPagedQuery)
            .WithName("GetDedsiUsersPagedQuery")
            .WithSummary("分页查询用户")
            .WithDescription("根据条件分页查询用户列表");
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