using Microsoft.Extensions.Options;
using MyRecipeBook.Api.Enpoints;
using MyRecipeBook.API.Extensions;
using MyRecipeBook.Api.Handlers;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddLocalizationConfig();

builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.AddProblemDetails(); 

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