using System.Globalization;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Castle.Core.Resource;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Domain.Extensions;
using MyRecipeBook.Exception;
using MyRecipeBook.Infrastructure.Data;
using MyRecipeBook.IntegrationTests.InlineData;
using MyRecipeBook.TestUtilities.Requests;
using Shouldly;

namespace MyRecipeBook.IntegrationTests.User;

public class RegisterUserTests : IClassFixture<MyRecipeBookWebApplicationFactory>
{
    private const string REQUEST_URI = "/users";
    
    private readonly HttpClient _httpClient;
    private readonly MyRecipeBookDbContext _dbContext;
    
    public RegisterUserTests(MyRecipeBookWebApplicationFactory factory)
    {
        _httpClient = factory.CreateClient();

        var scope = factory.Services.CreateScope();
        
        _dbContext = scope.ServiceProvider.GetRequiredService<MyRecipeBookDbContext>(); 
    }
    
    [Fact]
    public async Task Success()
    {
        var request = RegisterUserRequestBuilder.Build();
        
        var response = await _httpClient.PostAsJsonAsync(REQUEST_URI, request);
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
        
        // usa o using para liberar memoria após uso
        await using var responseBody = await response.Content.ReadAsStreamAsync();
        
        // não utiliza deserializacão pois testa explicitamente o retorno recebido pelos consumidores finais (clients)
        var responseData = await JsonDocument.ParseAsync(responseBody);
        responseData.RootElement.GetProperty("name").GetString().ShouldBe(request.Name);
        responseData.RootElement.GetProperty("tokens").GetProperty("accessToken").GetString().ShouldBeEmpty();
        
        var userExist  = await _dbContext.Users.AnyAsync(user => user.Active && user.Email.Equals(request.Email) && user.Name.Equals(request.Name));
        userExist.ShouldBeTrue();
    }
    
    [Theory]
    [ClassData(typeof(CultureInlineData))] // dessa forma centraliza as variacoes de cultura do retorno das mensagens de forma que fique escalavel
    public async Task Failure(string culture)
    {
        var request = RegisterUserRequestBuilder.Build();
        request.Name = string.Empty;
        
        _httpClient.DefaultRequestHeaders.AcceptLanguage.Clear();
        _httpClient.DefaultRequestHeaders.AcceptLanguage.ParseAdd(culture);
        
        var response = await _httpClient.PostAsJsonAsync(REQUEST_URI, request);
        
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        
        // usa o using para liberar memoria após uso
        await using var responseBody = await response.Content.ReadAsStreamAsync();
        
        // não utiliza deserializacão pois testa explicitamente o retorno recebido pelos consumidores finais (clients)
        var responseData = await JsonDocument.ParseAsync(responseBody);
        
        var expectedErrorMessage = ResourceMessagesException.ResourceManager.GetString("NAME_IS_EMPTY", new CultureInfo(culture));
        
        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();
        errors.ShouldSatisfyAllConditions(errorList =>
        {
            errorList.Count().ShouldBe(1);
            errorList.ShouldContain(error => error.GetString().IsNotEmpty() && error.GetString()!.Equals(expectedErrorMessage));
        });
        
        var userExist  = await _dbContext.Users.AnyAsync(user => user.Active && user.Email.Equals(request.Email) && user.Name.Equals(request.Name));
        userExist.ShouldBeFalse();
    }
}