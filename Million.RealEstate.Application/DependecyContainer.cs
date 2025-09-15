using Microsoft.Extensions.DependencyInjection;
using Million.RealEstate.Application.UseCases.Auth;
using Million.RealEstate.Application.UseCases.Auth.Implementations;
using Million.RealEstate.Application.UseCases.Properties;
using Million.RealEstate.Application.UseCases.Properties.Implementations;
using Million.RealEstate.Application.UseCases.PropertyImages;
using Million.RealEstate.Application.UseCases.PropertyImages.Implementations;
using Million.RealEstate.Application.UseCases.PropertyTraces;
using Million.RealEstate.Application.UseCases.PropertyTraces.Implementations;
using Million.RealEstate.Application.UseCases.Users;
using Million.RealEstate.Application.UseCases.Users.Implementations;

namespace Million.RealEstate.Application
{
    public static class DependecyContainer
    {
        public static IServiceCollection AddUseCasesServices(this IServiceCollection services)
        {
            // Property Use Cases
            services.AddScoped<ICreatePropertyUseCase, CreatePropertyUseCase>();
            services.AddScoped<IGetPropertyByIdUseCase, GetPropertyByIdUseCase>();
            services.AddScoped<IGetAllPropertiesUseCase, GetAllPropertiesUseCase>();
            services.AddScoped<IUpdatePropertyUseCase, UpdatePropertyUseCase>();
            services.AddScoped<IDeletePropertyUseCase, DeletePropertyUseCase>();
            services.AddScoped<IChangePricePropertyUseCase, ChangePricePropertyUseCase>();

            // PropertyImage Use Cases
            services.AddScoped<IAddPropertyImageUseCase, AddPropertyImageUseCase>();
            services.AddScoped<IDeletePropertyImageUseCase, DeletePropertyImageUseCase>();
            services.AddScoped<IUpdatePropertyImageEnableUseCase, UpdatePropertyImageEnableUseCase>();

            // PropertyTrace Use Cases
            services.AddScoped<ICreatePropertyTraceUseCase, CreatePropertyTraceUseCase>();
            services.AddScoped<IGetPropertyTracesByPropertyUseCase, GetPropertyTracesByPropertyUseCase>();
            services.AddScoped<IGetPropertyTraceByIdUseCase, GetPropertyTraceByIdUseCase>();
            services.AddScoped<IUpdatePropertyTraceUseCase, UpdatePropertyTraceUseCase>();
            services.AddScoped<IDeletePropertyTraceUseCase, DeletePropertyTraceUseCase>();

            // Auth Use Cases
            services.AddScoped<ILoginUseCase, LoginUseCase>();

            // User Use Cases
            services.AddScoped<ICreateUserUseCase, CreateUserUseCase>();

            return services;
        }
    }
}
