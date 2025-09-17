using DedsiNative.Apis.DedsiUsers;

namespace DedsiNative.Apis;

public static class DedsiNativeEndpoints
{
    public static void MapDedsiNativeEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapDedsiUserEndpoints();
    }
}
