using Microsoft.AspNetCore.Http;

public class ApplyForJobRequest
{
    public string JobId { get; set; }
    public string CoverLetter { get; set; }  
    public IFormFile Resume { get; set; } 
}

public class ApplicationListItemDto
{
    public string ApplicationId { get; set; }
    public string JobTitle { get; set; }
    public string CompanyName { get; set; }
    public string Status { get; set; }
    public DateTime AppliedAt { get; set; }
}

public class ApplicationStatusUpdateRequest
{
    public string ApplicationId { get; set; }
    public string NewStatus { get; set; } 
} 