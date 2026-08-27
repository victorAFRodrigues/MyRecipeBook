using MyRecipeBook.Application;
using MyRecipeBook.Controller.Api.Extensions;
using MyRecipeBook.Controller.Api.Filters;
using MyRecipeBook.Infrastructure;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddLocalizationConfig();

// exception filter
builder.Services.AddMvc(option => option.Filters.Add<ExceptionFilter>()); 

// adiciona configuração para deixar as rotas com nome minusculo
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});

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