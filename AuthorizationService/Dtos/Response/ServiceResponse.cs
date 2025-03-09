using System.Net;

namespace AuthorizationService.Dtos.Response;

public class ServiceResponse<T>
{
    public bool Success { get; set; } 
    public string Message { get; set; }  
    public T? Data { get; set; }
}