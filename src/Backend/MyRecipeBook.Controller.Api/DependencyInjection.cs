using MyRecipeBook.Controller.Api.Converter;
using MyRecipeBook.Controller.Api.Extensions;
using MyRecipeBook.Controller.Api.Filters;

namespace MyRecipeBook.Controller.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddControllerApiExtension(this IServiceCollection services)
    {
        services.AddControllers()
                .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new StringConverter()));

        services.AddOpenApi();
        
        // Adiciona localizacao (multilinguagem baseada na request)
        services.AddLocalizationConfig();

        // exception filter
        services.AddMvc(option => option.Filters.Add<ExceptionFilter>()); 

        // adiciona configuração para deixar as rotas com nome minusculo
        services.Configure<RouteOptions>(options =>
        {
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
        });
        
        return services;
    }
}