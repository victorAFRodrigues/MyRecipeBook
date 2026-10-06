using Moq;
using MyRecipeBook.Domain.Repositories;

namespace MyRecipeBook.TestUtilities.Mocks;

public class IUnitOfWorkBuilder
{
    public static IUnitOfWork Build()
    {
        var moq = new Mock<IUnitOfWork>();
        return moq.Object;
    }
}