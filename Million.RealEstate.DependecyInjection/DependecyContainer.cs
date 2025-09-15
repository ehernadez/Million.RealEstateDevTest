using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Million.RealEstate.Application;
using Million.RealEstate.Application.Mapping;
using Million.RealEstate.Infrastructure.EntityFramework;

namespace Million.RealEstate.DependecyInjection
{
    public static class DependecyContainer
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddUseCasesServices();
            services.AddRepositories(configuration);
            
            services.AddAutoMapper(cfg => {
                cfg.AddProfile<PropertyProfile>();
                cfg.AddProfile<UserProfile>();
            });

            return services;
        }
    }
}
