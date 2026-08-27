using Mapster;
using MyRecipeBook.Communication.Requests.UserAccount;
using MyRecipeBook.Exception.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.User.Register;

public class RegisterUserUseCase
{
     public void Execute(RegisterUserRequest request)
     {
          ValidateAndThrowOnFailure(request);
          
          var user = request.Adapt<Domain.Entities.User>();
     }
     
     private void ValidateAndThrowOnFailure(RegisterUserRequest request)
     {
          var validator = new RegisterUserValidator();
          
          var result = validator.Validate(request);

          if (!result.IsValid)
               throw new ErrorOnValidationException(result.Errors.Select(x => x.ErrorMessage).ToList());
     }
}