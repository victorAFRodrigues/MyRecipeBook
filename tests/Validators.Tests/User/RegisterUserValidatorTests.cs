using CommonTestsUtils.Requests;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Exception;
using Shouldly;

namespace Validators.Tests.User;

public class RegisterUserValidatorTests
{
    [Fact]
    public void Success()
    {
        // AAA
        // 1. Arrange
        var request = RegisterUserRequestBuilder.Build();
        
        var validator = new RegisterUserValidator();
        
        // 2. Act
        var result = validator.Validate(request);
        
        // 3. Assert
        // Assert.True(result.IsValid);
        
        // 3. Assert com Shoudly:
        result.IsValid.ShouldBeTrue();
    }
    
    [Fact]
    public void ShouldHaveError_WhenNameIsEmpty()
    {
        // AAA
        // 1. Arrange
        var request = RegisterUserRequestBuilder.Build();

        request.Name = "";
        
        var validator = new RegisterUserValidator();
        
        // 2. Act
        var result = validator.Validate(request);
        
        // 3. Assert
        // Assert.False(result.IsValid);
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(error => error.ErrorMessage.Equals(ResourceMessagesException.NAME_IS_EMPTY));
        });
    }
    
    [Fact]
    public void ShouldHaveError_WhenPasswordIsEmpty()
    {
        // AAA
        // 1. Arrange
        var request = RegisterUserRequestBuilder.Build();

        request.Password = "";
        
        var validator = new RegisterUserValidator();
        
        // 2. Act
        var result = validator.Validate(request);
        
        // 3. Assert
        // Assert.False(result.IsValid);
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(error => error.ErrorMessage.Equals(ResourceMessagesException.PASSWORD_IS_EMPTY));
        });
    }
    
    [Fact]
    public void ShouldHaveError_WhenPasswordIsShort()
    {
        // AAA
        // 1. Arrange
        var request = RegisterUserRequestBuilder.Build();

        request.Password = new string(request.Password.Take(4).ToArray());
        
        var validator = new RegisterUserValidator();
        
        // 2. Act
        var result = validator.Validate(request);
        
        // 3. Assert
        // Assert.False(result.IsValid);
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(error => error.ErrorMessage.Equals(ResourceMessagesException.PASSWORD_IS_SHORT));
        });
    }
    
    [Fact]
    public void ShouldHaveError_WhenEmailIsEmpty()
    {
        // AAA
        // 1. Arrange
        var request = RegisterUserRequestBuilder.Build();

        request.Email = "";
        
        var validator = new RegisterUserValidator();
        
        // 2. Act
        var result = validator.Validate(request);
        
        // 3. Assert
        // Assert.False(result.IsValid);
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(error => error.ErrorMessage.Equals(ResourceMessagesException.EMAIL_IS_EMPTY));
        });
    }
    
    [Fact]
    public void ShouldHaveError_WhenEmailIsInvalid()
    {
        // AAA
        // 1. Arrange
        var request = RegisterUserRequestBuilder.Build();

        request.Email = "vafr.com";
        
        var validator = new RegisterUserValidator();
        
        // 2. Act
        var result = validator.Validate(request);
        
        // 3. Assert
        // Assert.False(result.IsValid);
        
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(error => error.ErrorMessage.Equals(ResourceMessagesException.EMAIL_IS_INVALID));
        });
    }
}