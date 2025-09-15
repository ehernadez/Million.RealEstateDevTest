using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Million.RealEstate.Application.DTOs;
using Million.RealEstate.Application.UseCases.Owners;
using System.ComponentModel.DataAnnotations;

namespace Million.RealEstate.API.EndPoints
{
    public static class OwnerEndpoints
    {
        public static void MapOwnerEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("/owners", [Authorize] async ([FromBody] CreateOwnerDto dto, ICreateOwnerUseCase useCase) =>
            {
                try
                {
                    var result = await useCase.ExecuteAsync(dto);
                    return Results.Created($"/owners/{result}", result);
                }
                catch (ValidationException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithOpenApi()
            .WithTags("Owners")
            .Produces<int>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);

            endpoints.MapGet("/owners/{id}", [Authorize] async (int id, IGetOwnerByIdUseCase useCase) =>
            {
                var result = await useCase.ExecuteAsync(id);
                return result != null ? Results.Ok(result) : Results.NotFound();
            })
            .WithOpenApi()
            .WithTags("Owners")
            .Produces<OwnerDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status401Unauthorized);

            endpoints.MapGet("/owners", [Authorize] async (IGetAllOwnersUseCase useCase, int pageNumber = 1, int pageSize = 10) =>
            {
                var result = await useCase.ExecuteAsync(pageNumber, pageSize);
                return Results.Ok(result);
            })
            .WithOpenApi()
            .WithTags("Owners")
            .Produces<OwnerPagedResponseDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);

            endpoints.MapPut("/owners/{id}", [Authorize] async (int id, [FromBody] UpdateOwnerDto dto, IUpdateOwnerUseCase useCase) =>
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
            .WithTags("Owners")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);

            endpoints.MapDelete("/owners/{id}", [Authorize] async (int id, IDeleteOwnerUseCase useCase) =>
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
            .WithTags("Owners")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);
        }
    }
}