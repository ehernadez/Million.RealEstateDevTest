using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Million.RealEstate.Application.DTOs;
using Million.RealEstate.Application.UseCases.PropertyTraces;
using System.ComponentModel.DataAnnotations;

namespace Million.RealEstate.API.EndPoints
{
    public static class PropertyTraceEndpoints
    {
        public static void MapPropertyTraceEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("/properties/{propertyId}/traces", [Authorize] async ([FromRoute] int propertyId, [FromBody] CreatePropertyTraceDto dto, [FromServices] ICreatePropertyTraceUseCase useCase) =>
            {
                try
                {
                    var result = await useCase.ExecuteAsync(propertyId, dto);
                    return Results.Created($"/properties/traces/{result}", result);
                }
                catch (ValidationException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithOpenApi()
            .WithTags("PropertyTraces")
            .WithName("CreatePropertyTrace")
            .Produces<int>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);

            endpoints.MapGet("/properties/{propertyId}/traces", [Authorize] async ([FromRoute] int propertyId, [FromServices] IGetPropertyTracesByPropertyUseCase useCase) =>
            {
                try
                {
                    var result = await useCase.ExecuteAsync(propertyId);
                    return Results.Ok(result);
                }
                catch (ValidationException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithOpenApi()
            .WithTags("PropertyTraces")
            .WithName("GetPropertyTraces")
            .Produces<IEnumerable<PropertyTraceDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);

            endpoints.MapGet("/properties/traces/{id}", [Authorize] async ([FromRoute] int id, [FromServices] IGetPropertyTraceByIdUseCase useCase) =>
            {
                var result = await useCase.ExecuteAsync(id);
                return result != null ? Results.Ok(result) : Results.NotFound();
            })
            .WithOpenApi()
            .WithTags("PropertyTraces")
            .WithName("GetPropertyTrace")
            .Produces<PropertyTraceDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status401Unauthorized);

            endpoints.MapPut("/properties/traces/{id}", [Authorize] async ([FromRoute] int id, [FromBody] UpdatePropertyTraceDto dto, [FromServices] IUpdatePropertyTraceUseCase useCase) =>
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
            .WithTags("PropertyTraces")
            .WithName("UpdatePropertyTrace")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);

            endpoints.MapDelete("/properties/traces/{id}", [Authorize] async ([FromRoute] int id, [FromServices] IDeletePropertyTraceUseCase useCase) =>
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
            .WithTags("PropertyTraces")
            .WithName("DeletePropertyTrace")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);
        }
    }
}