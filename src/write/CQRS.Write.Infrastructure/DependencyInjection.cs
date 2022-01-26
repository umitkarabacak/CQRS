global using CQRS.Write.Domain.Entities;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.DependencyInjection;

using Microsoft.Extensions.Configuration;

namespace CQRS.Write.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ProjectContext>(opt =>
                opt.UseSqlServer(configuration.GetConnectionString("Default"))
            );

            return services;
        }
    }
}