namespace LuckyRest.Utils;

public class ServiceResult
{
    public ServiceResultStatus Status { get; set; }
    
    public static ServiceResult Success => new() {Status = ServiceResultStatus.Success};
    public static ServiceResult Exists => new() { Status = ServiceResultStatus.Exists };
    public static ServiceResult NotFound => new() {Status = ServiceResultStatus.NotFound};
    public static ServiceResult NoContent => new() {Status = ServiceResultStatus.NoContent};
}

public class ServiceResult<T> : ServiceResult
{
    public T? Data { get; set; }
}

public static class ServiceResultExtensions
{
    public static ServiceResult<T> WithData<T>(this ServiceResult result, T? data)
    {
        return new ServiceResult<T>()
        {
            Status = result.Status,
            Data = data
        };
    }
}

public enum ServiceResultStatus
{
    Success,
    NotFound,
    NoContent,
    Exists,
    Error,
}