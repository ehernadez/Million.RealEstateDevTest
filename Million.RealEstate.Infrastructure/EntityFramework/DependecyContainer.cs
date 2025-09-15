using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Million.RealEstate.Domain.Interfaces;
using Million.RealEstate.Infrastructure.EntityFramework.Data;
using Million.RealEstate.Infrastructure.EntityFramework.Repositories;
using Million.RealEstate.Infrastructure.Services;

namespace Million.RealEstate.Infrastructure.EntityFramework
{
    public static class DependecyContainer
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RealEstateDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("RealEstateConnection")));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IPropertyRepository, PropertyRepository>();
            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<IPropertyImageRepository, PropertyImageRepository>();
            services.AddScoped<IPropertyTraceRepository, PropertyTraceRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IFileStorageService, LocalFileStorageService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
