using Microsoft.AspNetCore.Mvc;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Communication.Requests.UserAccount;
using MyRecipeBook.Communication.Responses;

namespace MyRecipeBook.Controller.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(RegisterUserResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest registerUserRequest, IRegisterUserUseCase useCase)
    {
        var userResponse = await useCase.Execute(registerUserRequest);
    
        return Created(string.Empty, userResponse);
    }
}