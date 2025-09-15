using Microsoft.AspNetCore.Authorization;
using Million.RealEstate.Application.DTOs;
using Million.RealEstate.Application.UseCases.Properties;
using System.ComponentModel.DataAnnotations;

namespace Million.RealEstate.API.EndPoints
{
    public static class PropertyEndpoints
    {
        public static void MapPropertyEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("/properties", [Authorize] async (ICreatePropertyUseCase useCase, CreatePropertyDto dto) =>
            {
                try
                {
                    var result = await useCase.ExecuteAsync(dto);
                    return Results.Created($"/properties/{result}", result);
                }
                catch (ValidationException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithOpenApi()
            .WithTags("Properties")
            .Produces<int>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);

            endpoints.MapGet("/properties/{id}", [Authorize] async (IGetPropertyByIdUseCase useCase, int id) =>
            {
                var result = await useCase.ExecuteAsync(id);
                return result != null ? Results.Ok(result) : Results.NotFound();
            })
            .WithOpenApi()
            .WithTags("Properties")
            .Produces<PropertyFindOneResponseDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status401Unauthorized);

            endpoints.MapGet("/properties", [Authorize] async (IGetAllPropertiesUseCase useCase, [AsParameters] PropertyFilterDto filter, int pageNumber = 1, int pageSize = 10) =>
            {
                var result = await useCase.ExecuteAsync(filter, pageNumber, pageSize);
                return Results.Ok(result);
            })
            .WithOpenApi()
            .WithName("GetProperties")
            .WithTags("Properties")
            .Produces<PropertyPagedResponseDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);

            endpoints.MapPut("/properties/{id}", [Authorize] async (IUpdatePropertyUseCase useCase, int id, UpdatePropertyDto dto) =>
            {
                try
                {
                    var result = await useCase.ExecuteAsync(id, dto);
                    return result ? Results.NoContent() : Results.NotFound();
                }
                catch (ValidationException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithOpenApi()
            .WithTags("Properties")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);

            endpoints.MapDelete("/properties/{id}", [Authorize] async (IDeletePropertyUseCase useCase, int id) =>
            {
                try
                {
                    var result = await useCase.ExecuteAsync(id);
                    return result ? Results.NoContent() : Results.NotFound();
                }
                catch (ValidationException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithOpenApi()
            .WithTags("Properties")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);

            endpoints.MapPatch("/properties/{id}/price", [Authorize] async (IChangePricePropertyUseCase useCase, int id, decimal newPrice) =>
            {
                try
                {
                    var result = await useCase.ExecuteAsync(id, newPrice);
                    return result ? Results.NoContent() : Results.NotFound();
                }
                catch (ValidationException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithOpenApi()
            .WithTags("Properties")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);
        }
    }
}
