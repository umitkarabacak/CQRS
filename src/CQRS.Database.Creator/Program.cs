using CQRS.Database.Creator.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProjectContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<ProjectContext>();
    await context.Database.EnsureCreatedAsync();
}

app.MapGet("/", () => "First Database Schema Creator Service!");

app.MapGet("/categories", (ProjectContext projectContext) => projectContext.Categories.OrderByDescending(c => c.Id));

app.MapGet("/products", (ProjectContext projectContext) => projectContext.Products.OrderByDescending(c => c.Id));

app.MapGet("/outboxMessages", (ProjectContext projectContext) =>
{
    var outboxMessages = projectContext.OutboxMessages
        .OrderBy(o => o.IsPublished)
            .ThenBy(o => o.PublishedDate);

    return outboxMessages;
});

app.Run();
