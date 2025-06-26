public class DeleteJobResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public object? Object { get; set; }
    public List<string>? Errors { get; set; }
}