public class BaseResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T Object { get; set; }

    public static BaseResponse<T> CreateSuccess(T obj) => new BaseResponse<T> { Success = true, Message = "Success", Object = obj };
    public static BaseResponse<T> Fail(string message) => new BaseResponse<T> { Success = false, Message = message, Object = default(T) };
} 