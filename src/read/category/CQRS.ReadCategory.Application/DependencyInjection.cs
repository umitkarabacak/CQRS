global using CQRS.ReadCategory.Application.Common.Models;

using System.Reflection;
using CQRS.ReadCategory.Application.Common.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace CQRS.ReadCategory.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            #region Elastic Search

            var settings = new ConnectionSettings();
                settings.DefaultIndex("categories");
                //settings.BasicAuthentication("username","pwd");

            var elasticClient = new ElasticClient(settings);

            services.AddSingleton(elasticClient);

            #endregion

            return services;
        }
    }
}