using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace MyRecipeBook.Controller.Api.Extensions;

public static class LocalizationExtensions
{
    /// <summary>
    ///  Registra e configura a localização (i18n) da aplicação
    /// </summary>
    public static IServiceCollection AddLocalizationConfig(this IServiceCollection services)
    {
        services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = new List<CultureInfo> { new("en"), new("pt-BR") };
            
            options.DefaultRequestCulture = new RequestCulture("en");
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
            options.RequestCultureProviders.Add(new CookieRequestCultureProvider());
        });

        return services;
    }
}