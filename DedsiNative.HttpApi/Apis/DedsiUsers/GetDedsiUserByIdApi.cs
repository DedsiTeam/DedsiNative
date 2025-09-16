using DedsiNative.DedsiUsers.Queries;
using DedsiNative.DedsiUsers.Queries.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace DedsiNative.HttpApi.Apis.DedsiUsers;

/// <summary>
/// 根据 ID 获取用户 API
/// </summary>
public static class GetDedsiUserByIdApi
{
    /// <summary>
    /// 配置根据 ID 获取用户端点
    /// </summary>
    /// <param name="group">路由组</param>
    public static void MapGetDedsiUserById(this RouteGroupBuilder group)
    {
        group.MapGet("/{id}", GetDedsiUserById)
            .WithName("GetDedsiUserById")
            .WithSummary("根据 ID 获取用户")
            .WithDescription("通过用户 ID 获取单个用户信息");
    }

    /// <summary>
    /// 根据 ID 获取用户
    /// </summary>
    /// <param name="id">用户 ID</param>
    /// <param name="query">用户查询服务</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>用户信息</returns>
    private static Task<DedsiUserDto> GetDedsiUserById(
        [FromRoute] string id, 
        [FromServices] DedsiUserQuery query,
        CancellationToken cancellationToken)
    {
        return query.GetByIdAsync(id, cancellationToken);
    }
}