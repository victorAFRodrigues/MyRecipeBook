using Moq;
using MyRecipeBook.Domain.Repositories.User;

namespace MyRecipeBook.TestUtilities.Mocks;

public class IUserReadOnlyRepositoryBuilder
{
    private readonly Mock<IUserReadOnlyRepository> _mock;
    
    public IUserReadOnlyRepositoryBuilder()
    {
        _mock = new Mock<IUserReadOnlyRepository>();
    }

    public void ExistActiveUserWithEmail(string email)
    {
        _mock.Setup(repo => repo.ExistActiveUserWithEmail(email)).ReturnsAsync(true);
    }
    
    public IUserReadOnlyRepository Build() => _mock.Object;
}