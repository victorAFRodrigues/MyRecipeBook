namespace MyRecipeBook.Communication.Responses;

public class ResponseError
{
    public List<string> Errors { get; private set; }

    public ResponseError(List<string> errorMessages) => Errors = errorMessages;
    
    public ResponseError(IReadOnlyCollection<string> errorMessages) => Errors = [.. errorMessages]; // Adiciona suporte a IReadOnlyCollection
    
    public ResponseError(string errorMessage) =>  Errors = [errorMessage];
}