namespace DedsiNative.Apis.Users;

/// <summary>
/// DedsiUser 相关 API 端点
/// </summary>
public static partial class DedsiUserEndpoints
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
        group.MapCreateDedsiUserEndpoint();

        // 删除用户
        group.MapDeleteDedsiUserEndpoint();

        // 根据 ID 获取用户
        group.MapGetDedsiUserByIdEndpoint();

        // 分页查询用户
        group.MapGetDedsiUsersPagedQueryEndpoint();

        // 更新用户
        group.MapUpdateDedsiUserEndpoint();
    }
}
