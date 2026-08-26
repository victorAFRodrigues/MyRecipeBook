using MyRecipeBook.Controller.Api.Extensions;
using MyRecipeBook.Controller.Api.Filters;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddLocalizationConfig();

builder.Services.AddMvc(option => option.Filters.Add<ExceptionFilter>()); // exception filter

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