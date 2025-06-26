
using MediatR;

public class GetMyPostedJobsQueryHandler : IRequestHandler<GetMyPostedJobsQuery, PaginatedResponse<JobListItemDto>>
{
    private readonly IJobRepository _jobRepo;
    private readonly IApplicationRepository _applicationRepo;
    public GetMyPostedJobsQueryHandler(IJobRepository jobRepo, IApplicationRepository applicationRepo)
    {
        _jobRepo = jobRepo;
        _applicationRepo = applicationRepo;
    }
    public async Task<PaginatedResponse<JobListItemDto>> Handle(GetMyPostedJobsQuery request, CancellationToken cancellationToken)
    {
        var filter = new JobFilter { CompanyId = request.CompanyId };
        var (jobs, total) = await _jobRepo.BrowseJobsAsync(filter, request.PageNumber, request.PageSize);
        var jobIds = jobs.Select(j => j.Id).ToList();
        var appCounts = jobIds.Count > 0 ? (await Task.WhenAll(jobIds.Select(id => _applicationRepo.CountByJobIdAsync(id)))) : Array.Empty<int>();
        var result = jobs.Select((j, idx) => new JobListItemDto
        {
            JobId = j.Id,
            Title = j.Title,
            Description = j.Description?.Length > 200 ? j.Description.Substring(0, 200) + "..." : j.Description,
            Location = j.Location,
            CreatedAt = (DateTime)j.CreatedAt,
            ApplicationCount = appCounts.Length > idx ? appCounts[idx] : 0
        }).ToList();
        return PaginatedResponse<JobListItemDto>.CreateSuccess(result, request.PageNumber, request.PageSize, total);
    }
} 