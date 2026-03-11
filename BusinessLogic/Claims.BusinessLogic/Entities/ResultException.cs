namespace Claims.BusinessLogic.Entities;

[Serializable]
public class ResultException: Exception
{
    public string Error { get; }
    public new string Message { get; }

    public ResultException(string error, string message) : base(message)
    {
        Error = error;
        Message = message;
    }
}