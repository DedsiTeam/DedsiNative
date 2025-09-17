using DedsiNative.DedsiUsers.Operations;
using Microsoft.AspNetCore.Mvc;

namespace DedsiNative.Apis.DedsiUsers;

/// <summary>
/// 删除 Dedsi 用户 API
/// </summary>
public static class DeleteDedsiUserApi
{
    /// <summary>
    /// 配置删除用户路由
    /// </summary>
    /// <param name="group">路由组</param>
    public static void MapDeleteDedsiUser(this RouteGroupBuilder group)
    {
        group.MapDelete("/{id}", DeleteDedsiUser)
            .WithName("DeleteDedsiUser")
            .WithSummary("删除 Dedsi 用户")
            .WithDescription("根据ID删除指定的 Dedsi 用户");
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
}