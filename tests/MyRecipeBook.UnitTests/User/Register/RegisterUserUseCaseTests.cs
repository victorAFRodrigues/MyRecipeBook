using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Exception;
using MyRecipeBook.Exception.ExceptionsBase;
using MyRecipeBook.TestUtilities.Mocks;
using MyRecipeBook.TestUtilities.Requests;
using Shouldly;

namespace MyRecipeBook.UnitTests.User.Register;

public class RegisterUserUseCaseTests
{
    private RegisterUserUseCase CreateUseCase(string? emailThatAlreadyExists = null)
    {
        var passwordHasher = new IPasswordHasherBuilder().Build(); // não estatica pois retorna valores nas funcoes
        var userWriteOnlyRepository = IUserWriteOnlyRepositoryBuilder.Build();
        var userReadOnlyRepository = new IUserReadOnlyRepositoryBuilder(); // não estatica pois retorna valores nas funcoes
        var unitOfWork = IUnitOfWorkBuilder.Build();

        if (emailThatAlreadyExists != null)
            userReadOnlyRepository.ExistActiveUserWithEmail(emailThatAlreadyExists);
        
        
        return new RegisterUserUseCase(passwordHasher, userWriteOnlyRepository, userReadOnlyRepository.Build(), unitOfWork);
    }
    
    [Fact]
    public async Task Sucess()
    {
        var request = RegisterUserRequestBuilder.Build();
        
        var useCase = CreateUseCase();
        
        // Act
        var result = await useCase.Execute(request);
        
        result.ShouldNotBeNull();
        
        result.Tokens.ShouldNotBeNull();

        result.Name.ShouldBe(request.Name);
        
        result.Tokens.AccessToken.ShouldBeNullOrEmpty();
        
        result.Tokens.AccessToken.ShouldBeNullOrEmpty();
        
        result.Tokens.RefreshToken.ShouldBeNullOrEmpty();
    }
    
    [Fact]
    public async Task ShouldThowException_WhenEmailAlreadyExists()
    {
        var request = RegisterUserRequestBuilder.Build();
        var beforeEmail = request.Email;
        var useCase = CreateUseCase();
        
        // Act 1. Usuario criado com sucesso
        var result = await useCase.Execute(request);
        result.ShouldNotBeNull();
        
        // Act 2. Tenta criar outro usuario com mesmo email
        useCase = CreateUseCase(beforeEmail);
        
        var result2 = await useCase.Execute(request).ShouldThrowAsync<ErrorOnValidationException>();

        result2.ShouldNotBeNull();
        
        result2.ErrorMessages.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(error => error.Equals(ResourceMessagesException.EMAIL_ALREADY_EXISTS));
        });
    }
    
    [Fact]
    public async Task ShouldThowException_WhenNameIsEmpty()
    {
        var request = RegisterUserRequestBuilder.Build();
        
        request.Name = string.Empty;
        
        var useCase = CreateUseCase();
        
        // Act
        var result = await useCase.Execute(request).ShouldThrowAsync<ErrorOnValidationException>();
        
        result.ErrorMessages.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(error => error.Equals(ResourceMessagesException.NAME_IS_EMPTY));
        });
        

    }
}