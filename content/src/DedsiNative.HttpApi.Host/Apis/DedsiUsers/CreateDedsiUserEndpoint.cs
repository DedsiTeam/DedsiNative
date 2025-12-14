using DedsiNative.DedsiUsers;
using Microsoft.AspNetCore.Mvc;

namespace DedsiNative.Apis.DedsiUsers;

public static class CreateDedsiUserEndpoint
{
    public static void MapCreateDedsiUserEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost(DedsiUserEndpoints.BasePath + "create", async ([FromBody] CreateDedsiUserRequest request, IDedsiUserRepository dedsiUserRepository, CancellationToken cancellationToken) =>
        {
            var user = new DedsiUser(Guid.NewGuid().ToString(), request.Name, request.Email, request.MobilePhone);
            await dedsiUserRepository.InsertAsync(user, cancellationToken);
            return Results.Ok(true);
        })
        .WithTags("DedsiUsers")
        .WithName("CreateDedsiUser")
        .WithSummary("创建Dedsi用户")
        .WithDescription("创建一个新的Dedsi用户");
    }
}

public record CreateDedsiUserRequest(string Name, string Email, string MobilePhone);

