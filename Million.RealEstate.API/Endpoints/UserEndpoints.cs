using Microsoft.AspNetCore.Authorization;
using Million.RealEstate.Application.DTOs.Users;
using Million.RealEstate.Application.UseCases.Users;
using System.ComponentModel.DataAnnotations;

namespace Million.RealEstate.API.EndPoints
{
    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("/users", async (CreateUserDto request, ICreateUserUseCase useCase) =>
            {
                try
                {
                    var result = await useCase.ExecuteAsync(request);
                    return Results.Created($"/users/{result}", result);
                }
                catch (ValidationException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
                catch (InvalidOperationException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithOpenApi()
            .WithTags("BaseUsers")
            .WithName("CreateUser")
            .AllowAnonymous()
            .Produces<int>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);
        }
    }
}