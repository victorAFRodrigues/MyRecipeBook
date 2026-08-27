using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Domain.Security.PasswordHashing;
// using MyRecipeBook.Infrastructure.Security.PasswordHashing;

namespace MyRecipeBook.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        
        return services;
    }
}