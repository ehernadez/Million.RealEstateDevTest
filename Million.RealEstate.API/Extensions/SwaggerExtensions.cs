using Microsoft.OpenApi.Models;

namespace Million.RealEstate.API.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Million RealEstate API",
                    Version = "v1",
                    Description = "API for Real Estate Property Management"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

                c.MapType<IFormFile>(() => new OpenApiSchema
                {
                    Type = "string",
                    Format = "binary"
                });
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Million RealEstate API V1");
                c.RoutePrefix = string.Empty;
            });
            return app;
        }

        public static RouteHandlerBuilder WithSwaggerUploadFile(this RouteHandlerBuilder builder, 
            string parameterName = "file",
            string description = "File to upload",
            bool required = true,
            Dictionary<string, OpenApiSchema>? additionalProperties = null)
        {
            return builder.WithOpenApi(operation =>
            {
                operation.RequestBody = new OpenApiRequestBody
                {
                    Content =
                    {
                        ["multipart/form-data"] = new OpenApiMediaType
                        {
                            Schema = new OpenApiSchema
                            {
                                Type = "object",
                                Properties = BuildProperties(parameterName, description, additionalProperties),
                                Required = required ? new HashSet<string> { parameterName } : new HashSet<string>()
                            }
                        }
                    }
                };
                return operation;
            });
        }

        private static Dictionary<string, OpenApiSchema> BuildProperties(string parameterName, string description, Dictionary<string, OpenApiSchema>? additionalProperties)
        {
            var properties = new Dictionary<string, OpenApiSchema>
            {
                [parameterName] = new OpenApiSchema
                {
                    Type = "string",
                    Format = "binary",
                    Description = description
                }
            };

            if (additionalProperties != null)
            {
                foreach (var property in additionalProperties)
                {
                    properties.Add(property.Key, property.Value);
                }
            }

            return properties;
        }
    }
}
