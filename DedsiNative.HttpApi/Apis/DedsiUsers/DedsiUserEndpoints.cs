using DedsiNative.HttpApi.Apis.DedsiUsers;

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

        group.MapCreateDedsiUser();
        group.MapDeleteDedsiUser();
        group.MapUpdateDedsiUser();

        group.MapGetDedsiUserById();
        group.MapGetDedsiUsersPagedQuery();
    }
}