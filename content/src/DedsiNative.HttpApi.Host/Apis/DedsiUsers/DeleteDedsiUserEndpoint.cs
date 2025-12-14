using DedsiNative.DedsiUsers;
using Microsoft.AspNetCore.Mvc;

namespace DedsiNative.Apis.DedsiUsers;

public static class DeleteDedsiUserEndpoint
{
    public static void MapDeleteDedsiUserEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost(DedsiUserEndpoints.BasePath + "delete/{id}", async ([FromRoute] string id, IDedsiUserRepository repository, CancellationToken cancellationToken) =>
        {
            await repository.DeleteAsync(id, cancellationToken);
            return Results.NoContent();
        })
        .WithTags("DedsiUsers")
        .WithName("DeleteDedsiUser")
        .WithSummary("删除Dedsi用户")
        .WithDescription("删除Dedsi用户");
    }
}

