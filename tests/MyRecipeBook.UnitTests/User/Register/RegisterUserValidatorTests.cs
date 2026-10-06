using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Exception;
using MyRecipeBook.TestUtilities.Requests;
using Shouldly;

namespace MyRecipeBook.UnitTests.User.Register;

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
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("                ")]
    public void ShouldHaveError_WhenNameIsEmpty(string name)
    {
        // AAA
        // 1. Arrange
        var request = RegisterUserRequestBuilder.Build();

        request.Name = name;
        
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
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("                ")]
    public void ShouldHaveError_WhenPasswordIsEmpty(string password)
    {
        // AAA
        // 1. Arrange
        var request = RegisterUserRequestBuilder.Build();

        request.Password = password;
        
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
    
    [Theory]
    [InlineData(1)]
    [InlineData(6)]
    public void ShouldHaveError_WhenPasswordIsShort(int passwordLength)
    {
        // AAA
        // 1. Arrange
        var request = RegisterUserRequestBuilder.Build();

        request.Password =  new string('a', passwordLength);
        
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
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("                ")]
    public void ShouldHaveError_WhenEmailIsEmpty(string email)
    {
        // AAA
        // 1. Arrange
        var request = RegisterUserRequestBuilder.Build();

        request.Email = email;
        
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
    
    [Theory]
    [InlineData("victor.com")]
    [InlineData("victor@")]
    [InlineData("@")] 
    [InlineData("@gmail.com")] 
    public void ShouldHaveError_WhenEmailIsInvalid(string email)
    {
        // AAA
        // 1. Arrange
        var request = RegisterUserRequestBuilder.Build();

        request.Email = email;
        
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