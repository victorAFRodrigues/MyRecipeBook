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
               .Cascade(CascadeMode.Stop) // Interrompe a validacao após a propagacao do primeiro erro
               .NotEmpty()
               .WithMessage(ResourceMessagesException.EMAIL_IS_EMPTY)
               .EmailAddress()
               .WithMessage(ResourceMessagesException.EMAIL_IS_INVALID);
          
          // Password validation
          

          RuleFor(user => user.Password)
               .Cascade(CascadeMode.Stop) // Interrompe a validacao após a propagacao do primeiro erro
               .NotEmpty()
               .WithMessage(ResourceMessagesException.PASSWORD_IS_EMPTY)
               .MinimumLength(7)
               .WithMessage(ResourceMessagesException.PASSWORD_IS_SHORT);
               
     }
}