using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Domain.Security.PasswordHashing;
using MyRecipeBook.Infrastructure.Security.PasswordHashing;

namespace MyRecipeBook.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher, Argon2PasswordHasher>();
        
        return services;
    }
}