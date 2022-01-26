using CQRS.Distributor.App.BackgroundServices;
using CQRS.Distributor.App.Data;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//MassTransit Configuration
builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.AddBus(provider => Bus.Factory.CreateUsingRabbitMq());
});

//Project Context Configuration
builder.Services.AddDbContext<ProjectContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
);

builder.Services.AddHostedService<OutboxMessageService>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
