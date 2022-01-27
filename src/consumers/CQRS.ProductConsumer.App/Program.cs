using CQRS.ProductConsumer.App.Consumers;
using CQRS.ProductConsumer.App.Services;
using MassTransit;
using Nest;

var builder = WebApplication.CreateBuilder(args);

#region MassTransit

builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.AddConsumer<ProductConsumer>();
    busConfigurator.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
    {
        cfg.ReceiveEndpoint("product-consumer", receieEndpointConfigurator =>
        {
            receieEndpointConfigurator.ConfigureConsumer<ProductConsumer>(provider);
        });
    }));
});

#endregion

#region Elastic Search

var settings = new ConnectionSettings();
    settings.DefaultIndex("products");
    //settings.BasicAuthentication("username","pwd");

var elasticClient = new ElasticClient(settings);

builder.Services.AddSingleton(elasticClient);

#endregion


builder.Services.AddHostedService<ConsumeProductService>();

var app = builder.Build();

app.MapGet("/", () => "Product consume service is here!");

app.Run();
