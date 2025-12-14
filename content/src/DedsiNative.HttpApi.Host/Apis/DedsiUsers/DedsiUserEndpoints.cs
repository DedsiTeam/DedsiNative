using DedsiNative.Apis.DedsiUsers;

namespace DedsiNative;

public static class DedsiUserEndpoints
{
    public const string BasePath = "/api/dedsiusers/";
    
    public static void MapDedsiUserEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapCreateDedsiUserEndpoint();
        endpoints.MapDeleteDedsiUserEndpoint();
        endpoints.MapUpdateDedsiUserEndpoint();
        endpoints.MapGetDedsiUserEndpoint();
    }
}
