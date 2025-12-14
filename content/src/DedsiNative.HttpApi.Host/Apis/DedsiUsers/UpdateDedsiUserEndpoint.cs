using DedsiNative.DedsiUsers;
using Microsoft.AspNetCore.Mvc;

namespace DedsiNative.Apis.DedsiUsers;

public static class UpdateDedsiUserEndpoint
{
    public static void MapUpdateDedsiUserEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost(DedsiUserEndpoints.BasePath + "update", async ([FromBody] UpdateDedsiUserRequest request, IDedsiUserRepository dedsiUserRepository, CancellationToken cancellationToken) =>
        {
            var user = await dedsiUserRepository.GetAsync(request.Id, cancellationToken);
            
            user.ChangeName(request.Name);
            user.ChangeEmail(request.Email);
            user.ChangeMobilePhone(request.MobilePhone);
            
            await dedsiUserRepository.UpdateAsync(user, cancellationToken);
            return Results.Ok(true);
        })
        .WithTags("DedsiUsers")
        .WithName("UpdateDedsiUser")
        .WithSummary("更新Dedsi用户")
        .WithDescription("更新一个已存在的Dedsi用户");
    }
}

public record UpdateDedsiUserRequest(string Id, string Name, string Email, string MobilePhone);

