using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace MyRecipeBook.Api.Config;

public class LocalizationOptionsConfig
{
    /// <summary>
    ///  Set Localization Configs
    /// </summary>
    /// <param name="options">wait for RequestLocalizationOptions</param>
    internal static void Configure(RequestLocalizationOptions options)
    {
        var supportedCultures = new List<CultureInfo> { new("en"), new("pt-BR") };
        options.DefaultRequestCulture = new RequestCulture("en");
        options.SupportedCultures = supportedCultures;
        options.SupportedUICultures = supportedCultures;
        options.RequestCultureProviders.Add(new CookieRequestCultureProvider());
    }
}