namespace MyRecipeBook.Communication.Responses;

public class ErrorResponse
{
    public List<string> Errors { get; private set; }

    public ErrorResponse(List<string> errorMessages) => Errors = errorMessages;
    
    public ErrorResponse(IReadOnlyCollection<string> errorMessages) => Errors = [.. errorMessages]; // Adiciona suporte a IReadOnlyCollection
    
    public ErrorResponse(string errorMessage) =>  Errors = [errorMessage];
}