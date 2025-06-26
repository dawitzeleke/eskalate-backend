using MediatR;
using Application.Dtos;
using Application.Response;
using System.Collections.Generic;
using backend.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

public class GetMyPostedJobsQuery : IRequest<PaginatedResponse<JobListItemDto>>
{
    public string CompanyId { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetMyPostedJobsQueryHandler : IRequestHandler<GetMyPostedJobsQuery, PaginatedResponse<JobListItemDto>>
{
    private readonly JobRepository _jobRepo;
    private readonly ApplicationRepository _applicationRepo;
    public GetMyPostedJobsQueryHandler(JobRepository jobRepo, ApplicationRepository applicationRepo)
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
            Description = j.Description,
            Location = j.Location,
            CreatedAt = j.CreatedAt,
            ApplicationCount = appCounts.Length > idx ? appCounts[idx] : 0
        }).ToList();
        return PaginatedResponse<JobListItemDto>.CreateSuccess(result, request.PageNumber, request.PageSize, total);
    }
} 