namespace DedsiNative;

public static class DedsiNativeEndpoints
{
    public static void MapDedsiNativeEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapDedsiUserEndpoints();
    }
}
