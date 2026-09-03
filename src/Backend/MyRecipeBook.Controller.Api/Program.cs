using MyRecipeBook.Application;
using MyRecipeBook.Controller.Api;
using MyRecipeBook.Controller.Api.Extensions;
using MyRecipeBook.Controller.Api.Filters;
using MyRecipeBook.Infrastructure;
using MyRecipeBook.Infrastructure.Migrations;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// adiciona todas as configuracoes pertinentes SOMENTE a API
builder.Services.AddControllerApiExtension();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference("docs");
}

app.UseAuthorization();

app.MapControllers();

// Configura execução das migrations no startup da API
await using (var scope = app.Services.CreateAsyncScope())
{
    await DatabaseMigration.ExecuteMigrations(scope.ServiceProvider);
}

app.Run();