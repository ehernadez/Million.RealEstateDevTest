using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Million.RealEstate.API.Extensions;
using Million.RealEstate.Application.DTOs;
using Million.RealEstate.Application.UseCases.PropertyImages;
using System.ComponentModel.DataAnnotations;

namespace Million.RealEstate.API.EndPoints
{
    public static class PropertyImageEndpoints
    {
        public static void MapPropertyImageEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("/properties/images/{propertyId}", [Authorize] async Task<IResult> (int propertyId, IFormFile file, bool enabled, IAddPropertyImageUseCase useCase) =>
            {
                try
                {
                    if (file == null || file.Length == 0)
                        return Results.BadRequest(new { error = "No file was provided" });

                    var allowedTypes = new[] { "image/jpeg", "image/png", "image/gif" };
                    if (!allowedTypes.Contains(file.ContentType.ToLower()))
                        return Results.BadRequest(new { error = "Invalid file type. Only JPEG, PNG and GIF are allowed." });

                    using var stream = new MemoryStream();
                    await file.CopyToAsync(stream);
                    stream.Position = 0;

                    var dto = new AddPropertyImageDto
                    {
                        ImageStream = stream,
                        FileName = file.FileName,
                        ContentType = file.ContentType,
                        Length = file.Length,
                        Enabled = enabled
                    };

                    var result = await useCase.ExecuteAsync(propertyId, dto);
                    return Results.Created($"/properties/{propertyId}/images/{result.IdPropertyImage}", result);
                }
                catch (ValidationException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .DisableAntiforgery()
            .WithName("AddPropertyImage")
            .WithTags("PropertyImages")
            .WithSwaggerUploadFile("file", "Image file to upload (JPEG, PNG, GIF only, max 5MB)", true, new Dictionary<string, OpenApiSchema>
            {
                ["enabled"] = new OpenApiSchema
                {
                    Type = "boolean",
                    Description = "Enable/disable the image",
                    Default = new OpenApiBoolean(true)
                }
            })
            .Produces<PropertyImageDto>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);

            endpoints.MapDelete("/properties/images/{id}", [Authorize] async (IDeletePropertyImageUseCase useCase, int id) =>
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
            .WithTags("PropertyImages")
            .WithName("DeletePropertyImage")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);

            endpoints.MapPatch("/properties/images/{id}/enable", [Authorize] async (IUpdatePropertyImageEnableUseCase useCase, int id, bool enabled) =>
            {
                try
                {
                    var result = await useCase.ExecuteAsync(id, enabled);
                    return result ? Results.NoContent() : Results.NotFound();
                }
                catch (ValidationException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            })
            .WithOpenApi()
            .WithTags("PropertyImages")
            .WithName("UpdatePropertyImageEnable")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);
        }
    }
}