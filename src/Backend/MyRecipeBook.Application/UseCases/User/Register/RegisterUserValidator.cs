using FluentValidation;
using MyRecipeBook.Communication.Requests.UserAccount;
using MyRecipeBook.Exception;
     
namespace MyRecipeBook.Application.UseCases.User.Register;

public class RegisterUserValidator : AbstractValidator<RegisterUserRequest>
{
     public RegisterUserValidator()
     {
          // Name validation
          RuleFor(user  => user.Name)
               .NotEmpty()
               .WithMessage(ResourceMessagesException.NAME_IS_EMPTY);
          
          // Email validation
          RuleFor(user => user.Email)
               .NotEmpty().WithMessage(ResourceMessagesException.EMAIL_IS_EMPTY);
          When(user => !string.IsNullOrWhiteSpace(user.Email), () =>
               RuleFor(user => user.Email)
                    .EmailAddress().WithMessage(ResourceMessagesException.EMAIL_IS_INVALID));
          
          // Password validation
          RuleFor(user => user.Password)
               .NotEmpty().WithMessage(ResourceMessagesException.PASSWORD_IS_EMPTY);
          When(user => !string.IsNullOrWhiteSpace(user.Password), () =>
               RuleFor(user => user.Password)
                    .MinimumLength(7).WithMessage(ResourceMessagesException.PASSWORD_IS_SHORT));
               
     }
}