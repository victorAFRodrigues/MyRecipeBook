namespace MyRecipeBook.Domain.Extensions;

public static class StringExtension
{
    public static bool IsNotEmpty(this string? value) => !string.IsNullOrWhiteSpace(value);
}