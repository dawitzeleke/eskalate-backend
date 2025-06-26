using backend.Domain.Common;

public class Job : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
}