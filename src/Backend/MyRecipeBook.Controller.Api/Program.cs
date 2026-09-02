using MyRecipeBook.Application;
using MyRecipeBook.Controller.Api;
using MyRecipeBook.Controller.Api.Extensions;
using MyRecipeBook.Controller.Api.Filters;
using MyRecipeBook.Infrastructure;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// adiciona todas as configuracoes pertinentes SOMENTE a API
builder.Services.AddControllerApiExtension();

builder.Services.AddInfrastructure();

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

app.Run();