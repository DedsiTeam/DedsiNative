using DedsiNative.Apis.Dtos;
using DedsiNative.DedsiUsers;

namespace DedsiNative.Apis.DedsiUsers;

public static class GetDedsiUserEndpoint
{
    public static void MapGetDedsiUserEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet(DedsiUserEndpoints.BasePath + "{id}", async (string id, IDedsiUserRepository dedsiUserRepository, CancellationToken cancellationToken) =>
        {
            var user = await dedsiUserRepository.GetAsync(id, cancellationToken);
            return Results.Ok(new DedsiUserDto(user.Id, user.Name, user.Email, user.MobilePhone));
        })
        .WithTags("DedsiUsers")
        .WithName("GetDedsiUser")
        .WithSummary("获取Dedsi用户")
        .WithDescription("根据ID获取Dedsi用户详情");
    }
}
