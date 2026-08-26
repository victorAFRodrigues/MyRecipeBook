using Microsoft.AspNetCore.Mvc;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Communication.Requests.UserAccount;

namespace MyRecipeBook.Controller.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpPost]
    public IActionResult Register([FromBody] RegisterUserRequest registerUserRequest)
    {
        var useCase = new RegisterUserUseCase();
    
        useCase.Execute(registerUserRequest);
    
        return Created();
    }
}