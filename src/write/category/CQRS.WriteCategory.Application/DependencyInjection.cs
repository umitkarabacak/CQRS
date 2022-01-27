global using AutoMapper;
global using CQRS.WriteCategory.Application.Common.Interfaces;
global using CQRS.WriteCategory.Application.Common.Mappings;
global using CQRS.WriteCategory.Domain.Entities;
global using Events;
global using FluentValidation;
global using MediatR;
global using System.Reflection;

using CQRS.WriteCategory.Application.Common.Behaviours;
using Microsoft.Extensions.DependencyInjection;

namespace CQRS.WriteCategory.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            return services;
        }
    }
}