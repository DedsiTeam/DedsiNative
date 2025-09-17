using DedsiNative.DedsiUsers.Operations;
using Microsoft.AspNetCore.Mvc;

namespace DedsiNative.Apis.DedsiUsers;

/// <summary>
/// 更新 Dedsi 用户 API
/// </summary>
public static class UpdateDedsiUserApi
{
    /// <summary>
    /// 配置更新用户路由
    /// </summary>
    /// <param name="group">路由组</param>
    public static void MapUpdateDedsiUser(this RouteGroupBuilder group)
    {
        group.MapPut("/{id}", UpdateDedsiUser)
            .WithName("UpdateDedsiUser")
            .WithSummary("更新 Dedsi 用户")
            .WithDescription("更新指定的 Dedsi 用户信息");
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
        [FromBody] UpdateDedsiUserRequestDto input,
        [FromServices] UpdateDedsiUserOperation operation,
        CancellationToken cancellationToken)
    {
        var operationInput = new UpdateDedsiUserInputDto(id, input.UserName, input.Email, input.MobilePhone);
        return operation.ExecuteAsync(operationInput, cancellationToken);
    }
}

/// <summary>
/// 更新用户请求 DTO（用于API层面，不包含ID）
/// </summary>
/// <param name="UserName">用户名</param>
/// <param name="Email">邮箱</param>
/// <param name="MobilePhone">手机号</param>
public record UpdateDedsiUserRequestDto
(
    string UserName,
    string Email,
    string MobilePhone
);