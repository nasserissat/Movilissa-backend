namespace Movilissa.core.Responses;

public class ServiceResponse<T>
{
    public T Data { get; set; }
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public List<string> Errors { get; set; }

    public ServiceResponse()
    {
        Errors = new List<string>();
    }
}