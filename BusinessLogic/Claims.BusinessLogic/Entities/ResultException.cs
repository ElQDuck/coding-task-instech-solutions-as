namespace Claims.BusinessLogic.Entities;

[Serializable]
public class ResultException: Exception
{
    public string Error { get; }
    public new string Message { get; }

    public ResultException(string error, string message) : base(message)
    {
        // TODO add property status code e.g. StatusCodes.Status400BadRequest,
        Error = error;
        Message = message;
    }
}