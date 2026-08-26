namespace MyRecipeBook.Exception.ExceptionsBase;

public class ErrorOnValidationException(List<string> errorMessages) : MyRecipeBookExeception
{
    private readonly List<string> _errors = errorMessages;
}