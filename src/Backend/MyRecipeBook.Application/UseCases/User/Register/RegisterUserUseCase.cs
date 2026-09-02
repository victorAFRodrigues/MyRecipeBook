using Mapster;
using MyRecipeBook.Communication.Requests.UserAccount;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Domain.Security.PasswordHashing;
using MyRecipeBook.Exception;
using MyRecipeBook.Exception.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.User.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{
     private readonly IPasswordHasher _passwordHasher;
     private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
     private readonly IUnitOfWork  _unitOfWork;
     private readonly IUserReadOnlyRepository  _userReadOnlyRepository;
     
     public RegisterUserUseCase(
          IPasswordHasher passwordHasher, 
          IUserWriteOnlyRepository userWriteOnlyRepository, 
          IUserReadOnlyRepository  userReadOnlyRepository,
          IUnitOfWork unitOfWork)
     {
          _passwordHasher = passwordHasher;
          _userWriteOnlyRepository = userWriteOnlyRepository;
          _unitOfWork = unitOfWork;
          _userReadOnlyRepository = userReadOnlyRepository;
     }
     
     public async Task<RegisterUserResponse> Execute(RegisterUserRequest request)
     {
          await ValidateAndThrowOnFailure(request);
          
          var user = request.Adapt<Domain.Entities.User>();
          
          user.Password = _passwordHasher.HashPassword(request.Password);
          
          await _userWriteOnlyRepository.AddAsync(user);

          await _unitOfWork.CommitAsync();

          return new RegisterUserResponse
          {
               Name = user.Name
          };
     }
     
     private async Task ValidateAndThrowOnFailure(RegisterUserRequest request)
     {
          var validator = new RegisterUserValidator();
          
          var result = validator.Validate(request);
          
          var emailExist = await _userReadOnlyRepository.ExistActiveUserWithEmail(request.Email);
          
          if(emailExist)
               result.Errors.Add(new(string.Empty, ResourceMessagesException.EMAIL_ALREADY_EXISTS));

          if (!result.IsValid)
               throw new ErrorOnValidationException(result.Errors.Select(x => x.ErrorMessage).ToList());
     }
}