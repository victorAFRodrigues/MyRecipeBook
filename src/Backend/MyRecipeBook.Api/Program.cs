using Microsoft.Extensions.Options;
using MyRecipeBook.Api.Enpoints;
using MyRecipeBook.API.Extensions;
using MyRecipeBook.Api.Handlers;
using MyRecipeBook.Application.UseCases.User.Register;
using Scalar.AspNetCore;
using MyRecipeBook.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddLocalizationConfig();

builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<RegisterUserUseCase>();

var app = builder.Build();

var localizationOptions =  app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>();

app.UseRequestLocalization(localizationOptions.Value);

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference("docs");
}

// app.UseHttpsRedirection();

app.UseExceptionHandler(); // middleware que dispara o handler de exception

app.MapUserEndpoints();

app.Run();