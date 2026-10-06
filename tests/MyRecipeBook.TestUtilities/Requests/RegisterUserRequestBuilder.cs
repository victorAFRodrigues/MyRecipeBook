using Bogus;
using MyRecipeBook.Communication.Requests.UserAccount;

namespace MyRecipeBook.TestUtilities.Requests;

public static class RegisterUserRequestBuilder
{
    public static RegisterUserRequest Build()
    {
        return new Faker<RegisterUserRequest>()
            .RuleFor(request => request.Name, f => f.Person.FullName)
            .RuleFor(request => request.Email, (f, user) => f.Internet.Email(user.Name))
            .RuleFor(request => request.Password, f => f.Internet.Password());
    }
}