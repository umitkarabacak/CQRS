using CQRS.CategoryConsumer.App.Consumers;
using CQRS.CategoryConsumer.App.Services;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

//MassTransit
builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.AddConsumer<CategoryConsumer>();
    busConfigurator.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
    {
        cfg.ReceiveEndpoint("category-consumer", receieEndpointConfigurator =>
        {
            receieEndpointConfigurator.ConfigureConsumer<CategoryConsumer>(provider);
        });
    }));
});

builder.Services.AddHostedService<ConsumeCategoryService>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
