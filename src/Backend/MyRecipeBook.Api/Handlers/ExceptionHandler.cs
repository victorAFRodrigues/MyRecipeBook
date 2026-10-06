using Microsoft.AspNetCore.Diagnostics;
using MyRecipeBook.Exception;
using MyRecipeBook.Exception.ExceptionsBase;
using MyRecipeBook.Communication.Responses;

namespace MyRecipeBook.Api.Handlers;

public class ExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync( HttpContext httpContext, System.Exception exception, CancellationToken cancellationToken)
    {
        if (exception is ErrorOnValidationException ex)
        {
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            await httpContext.Response.WriteAsJsonAsync(
                new ErrorResponse(ex.ErrorMessages), 
                cancellationToken);
        }
        else
        {
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(
                new ErrorResponse(ResourceMessagesException.UNKNOWN_ERROR), 
                cancellationToken);
        }

        return true; 
    }
}