using MediatR;

public class BrowseJobsQuery : IRequest<PaginatedResponse<JobDto>>
{
    public string? Title { get; set; }
    public string? Location { get; set; }
    public string? CompanyName { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? CompanyId { get; set; } 
}
