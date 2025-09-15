using Microsoft.AspNetCore.Authorization;
using Million.RealEstate.Application.DTOs.Auth;
using Million.RealEstate.Application.UseCases.Auth;

namespace Million.RealEstate.API.EndPoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("/auth/login", async (LoginRequestDto request, ILoginUseCase useCase) =>
            {
                try
                {
                    var result = await useCase.ExecuteAsync(request);
                    return Results.Ok(result);
                }
                catch (UnauthorizedAccessException)
                {
                    return Results.Unauthorized();
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithOpenApi()
            .WithTags("Authentication")
            .AllowAnonymous()
            .Produces<LoginResponseDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);
        }
    }
}