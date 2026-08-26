using FluentValidation;
using MyRecipeBook.Communication.Requests.UserAccount;
using MyRecipeBook.Exception;
     
namespace MyRecipeBook.Application.UseCases.User.Register;

public class RegisterUserValidator : AbstractValidator<RegisterUserRequest>
{
     public RegisterUserValidator()
     {
          RuleFor(user  => user.Name)
               .NotEmpty()
               .WithMessage(ResourceMessagesException.NAME_IS_REQUIRED);
          
          RuleFor(user => user.Email)
               .NotEmpty()
               .WithMessage(ResourceMessagesException.EMAIL_IS_REQUIRED);
          
          When(user => !string.IsNullOrWhiteSpace(user.Email), () => 
               RuleFor(user => user.Email)
                    .EmailAddress()
                    .WithMessage(ResourceMessagesException.EMAIL_IS_INVALID));
          
          RuleFor(user => user.Password)
               .NotEmpty()
               .WithMessage("Please specify a valid password");

          
     }
}