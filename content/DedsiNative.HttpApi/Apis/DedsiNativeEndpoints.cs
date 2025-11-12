using DedsiNative.Apis.Users;

namespace DedsiNative.Apis;

public static class DedsiNativeEndpoints
{
    public static void MapDedsiNativeEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapDedsiUserEndpoints();
    }
}
