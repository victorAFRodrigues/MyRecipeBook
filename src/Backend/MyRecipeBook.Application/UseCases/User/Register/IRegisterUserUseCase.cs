using MyRecipeBook.Communication.Requests.UserAccount;
using MyRecipeBook.Communication.Responses;

namespace MyRecipeBook.Application.UseCases.User.Register;

public interface IRegisterUserUseCase
{
    Task<RegisterUserResponse> Execute(RegisterUserRequest request);
}