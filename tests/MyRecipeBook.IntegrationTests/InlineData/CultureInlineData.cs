using System.Collections;

namespace MyRecipeBook.IntegrationTests.InlineData;

public class CultureInlineData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return ["en-US"];
        yield return ["pt-BR"];
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}