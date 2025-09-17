using DedsiNative.DedsiUsers.Operations;
using Microsoft.AspNetCore.Mvc;

namespace DedsiNative.Apis.DedsiUsers;

/// <summary>
/// 创建用户 API
/// </summary>
public static class CreateDedsiUserApi
{
    /// <summary>
    /// 配置创建用户路由
    /// </summary>
    /// <param name="group">路由组</param>
    public static void MapCreateDedsiUser(this RouteGroupBuilder group)
    {
        group.MapPost("/", CreateDedsiUser)
            .WithName("CreateDedsiUser")
            .WithSummary("创建 Dedsi 用户")
            .WithDescription("创建一个新的 Dedsi 用户");
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
}