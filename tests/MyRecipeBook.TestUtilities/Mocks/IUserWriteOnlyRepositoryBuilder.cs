using Moq;
using MyRecipeBook.Domain.Repositories.User;

namespace MyRecipeBook.TestUtilities.Mocks;

public class IUserWriteOnlyRepositoryBuilder
{
    public static IUserWriteOnlyRepository Build()
    {
        var moq = new Mock<IUserWriteOnlyRepository>();
        return moq.Object;
    }
}
