global using AutoMapper;
global using CQRS.WriteProduct.Application.Common.Interfaces;
global using CQRS.WriteProduct.Application.Common.Mappings;
global using CQRS.WriteProduct.Domain.Entities;
global using Events;
global using FluentValidation;
global using MediatR;
global using System.Reflection;

using CQRS.WriteProduct.Application.Common.Behaviours;
using Microsoft.Extensions.DependencyInjection;

namespace CQRS.WriteProduct.Application
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