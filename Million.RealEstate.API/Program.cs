using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Million.RealEstate.API.EndPoints;
using Million.RealEstate.API.Extensions;
using Million.RealEstate.DependecyInjection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddSwaggerDocumentation();
builder.Services.AddDependencies(configuration);

var jwtKey = configuration["JwtSettings:SecretKey"];
var keyBytes = Encoding.ASCII.GetBytes(jwtKey ?? throw new InvalidOperationException("JWT secret key not configured"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = configuration["JwtSettings:Issuer"],
            ValidAudience = configuration["JwtSettings:Audience"],
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseSwaggerDocumentation();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

var solutionPath = Directory.GetParent(Directory.GetCurrentDirectory())?.FullName 
    ?? throw new InvalidOperationException("Could not determine solution directory");
var wwwrootPath = Path.Combine(solutionPath, "wwwroot");

if (!Directory.Exists(wwwrootPath))
{
    Directory.CreateDirectory(wwwrootPath);
}
var imagesPath = Path.Combine(wwwrootPath, "images", "properties");
if (!Directory.Exists(imagesPath))
{
    Directory.CreateDirectory(imagesPath);
}

app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(wwwrootPath),
    RequestPath = ""
});

app.UseHttpsRedirection();

// Map endpoints
app.MapUserEndpoints();
app.MapAuthEndpoints();
app.MapPropertyEndpoints();
app.MapPropertyImageEndpoints();
app.MapPropertyTraceEndpoints();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<Million.RealEstate.Infrastructure.EntityFramework.Data.RealEstateDbContext>();
    db.Database.Migrate();
}

app.Run();
