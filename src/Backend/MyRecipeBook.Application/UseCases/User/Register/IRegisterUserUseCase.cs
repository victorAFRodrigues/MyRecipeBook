using MyRecipeBook.Communication.Requests.UserAccount;

namespace MyRecipeBook.Application.UseCases.User.Register;

public interface IRegisterUserUseCase
{
    void Execute(RegisterUserRequest request);
}