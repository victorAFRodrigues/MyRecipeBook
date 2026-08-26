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
               .NotEmpty().WithMessage(ResourceMessagesException.EMAIL_IS_REQUIRED)
               .EmailAddress().WithMessage(ResourceMessagesException.EMAIL_IS_INVALID);     

          RuleFor(user => user.Password)
               .NotEmpty().WithMessage(ResourceMessagesException.PASSWORD_EMPTY)
               .MinimumLength(6).WithMessage(ResourceMessagesException.PASSWORD_IS_SHORT);
     }
}