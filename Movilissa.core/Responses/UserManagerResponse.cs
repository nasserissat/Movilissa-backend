namespace Movilissa.core.Responses;

public class UserManagerResponse
{
    public UserManagerResponse() {}

    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public IEnumerable<string> Errors { get; set; }

    public UserManagerResponse(bool isSuccess, string message, IEnumerable<string> errors = null)
    {
        IsSuccess = isSuccess;
        Message = message;
        Errors = errors ?? new List<string>();
    }
}
