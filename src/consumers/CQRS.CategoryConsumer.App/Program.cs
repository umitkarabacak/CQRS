using CQRS.CategoryConsumer.App.Consumers;
using CQRS.CategoryConsumer.App.Services;
using MassTransit;
using Nest;

var builder = WebApplication.CreateBuilder(args);

#region MassTransit

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

#endregion

#region Elastic Search

var settings = new ConnectionSettings();
    settings.DefaultIndex("categories");
    //settings.BasicAuthentication("username","pwd");
    
var elasticClient = new ElasticClient(settings);

builder.Services.AddSingleton(elasticClient);

#endregion


builder.Services.AddHostedService<ConsumeCategoryService>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
