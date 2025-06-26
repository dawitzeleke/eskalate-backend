using System.Runtime.CompilerServices;
using backend.Domain.Common;

public class Application : BaseEntity
{
    public string ApplicantId { get; set; }
    public string JobId { get; set; }
    public string ResumeLink { get; set; }
    public string CoverLetter { get; set; }
    public StatusEnum Status { get; set; } 
    public DateTime AppliedAt { get; set; }
}