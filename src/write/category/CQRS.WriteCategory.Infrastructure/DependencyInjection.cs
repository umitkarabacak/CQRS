global using CQRS.WriteCategory.Application.Common.Interfaces;
global using CQRS.WriteCategory.Domain.Entities;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.DependencyInjection;

using Microsoft.Extensions.Configuration;

namespace CQRS.WriteCategory.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ProjectContext>(opt =>
                opt.UseSqlServer(configuration.GetConnectionString("Default"))
            );

            services.AddScoped<IProjectContext>(provider => provider.GetService<ProjectContext>());

            return services;
        }
    }
}