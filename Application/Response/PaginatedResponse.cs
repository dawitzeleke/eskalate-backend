public class PaginatedResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalSize { get; set; }
    public List<T> Object { get; set; }

    public static PaginatedResponse<T> CreateSuccess(List<T> data, int page, int size, int total) =>
        new PaginatedResponse<T>
        {
            Success = true,
            Message = "Success",
            PageNumber = page,
            PageSize = size,
            TotalSize = total,
            Object = data
        };

    public static PaginatedResponse<T> Fail(string message) =>
        new PaginatedResponse<T> { Success = false, Message = message, Object = new List<T>() };
}
