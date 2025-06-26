using Microsoft.AspNetCore.Http;

public class ApplyForJobRequest
{
    public string JobId { get; set; }
    public string CoverLetter { get; set; } // optional, max 200 chars
    public IFormFile Resume { get; set; } // required, PDF
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
    public string NewStatus { get; set; } // Applied, Reviewed, Interview, Rejected, Hired
} 