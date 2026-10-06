namespace MyRecipeBook.Exception.ExceptionsBase;

public class ErrorOnValidationException(List<string> errorMessages) : MyRecipeBookExeception
{
    public IReadOnlyCollection<string> ErrorMessages => errorMessages;
    
    // propriedade privada "Desnecessária"
    // private readonly List<string> _errors = errorMessages;
    
    // chamada via metodo assim como a versao antiga mas usando primary constructor
    // public List<string> GetErrorMessages() => _errors; 

}

// Forma antiga (descontinuado)
// public class ErrorOnValidationException: MyRecipeBookExeception
// {
//     private readonly List<string> _errors;
//
//     public ErrorOnValidationException(List<string> errorMessages)
//     {
//         _errors = errorMessages;
//     }
//  
//     public List<string> GetErrorMessages() => _errors;
// }