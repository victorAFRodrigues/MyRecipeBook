using Microsoft.AspNetCore.Mvc;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Communication.Requests.UserAccount;

namespace MyRecipeBook.Api.Enpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        var routeGroup = app.MapGroup("/api/user");

        routeGroup.MapPost("", Register);
    }

    private static IResult Register([FromBody] RegisterUserRequest registerUserRequest, [FromServices] IRegisterUserUseCase useCase){
        useCase.Execute(registerUserRequest);
    
        return Results.Created();
    }

}