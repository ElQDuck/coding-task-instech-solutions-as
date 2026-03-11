using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Claims.BusinessLogic.Entities;

public class ResultExceptionHandler: IExceptionHandler
{
    private readonly IProblemDetailsService _problemDetailsService;

    public ResultExceptionHandler(IProblemDetailsService problemDetailsService)
    {
        _problemDetailsService = problemDetailsService;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not ResultException resultException)
        {
            return true;
        }

        // TODO get StatusCodes from exception
        var exceptionDetails = new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = resultException.Error,
            Detail = resultException.Message,
            Type = "Bad Request"
        };

        httpContext.Response.StatusCode = exceptionDetails.Status.Value;
        return await _problemDetailsService.TryWriteAsync(
            new ProblemDetailsContext
            {
                HttpContext = httpContext,
                ProblemDetails = exceptionDetails
            });
    }
}