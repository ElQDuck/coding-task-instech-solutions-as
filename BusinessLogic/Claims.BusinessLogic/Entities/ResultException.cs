using System.Net;

namespace Claims.BusinessLogic.Entities;

public class ResultException(string message, params Exception[] innerExceptions): AggregateException(message, innerExceptions)
{
    public ResultException(string message, Exception? innerException) : this(message,
        innerException is null ? [] : [innerException])
    {
    }
    
    public ResultException(string message, IEnumerable<Exception> innerExceptions): this(message, [..innerExceptions])
    {}
    
    public WebExceptionStatus Status { get; init; } = WebExceptionStatus.UnknownError;
    public required string Code {get; init;}
    public override string Message { get; } = message;
    public Dictionary<string, object>? AdditionalInfo { get; init; }
}