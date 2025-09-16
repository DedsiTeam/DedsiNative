using DedsiNative.HttpApi.Apis.DedsiUsers;

namespace DedsiNative.Apis.DedsiUsers;

/// <summary>
/// DedsiUser œ‡πÿ API ∂Àµ„
/// </summary>
public static class DedsiUserEndpoints
{
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