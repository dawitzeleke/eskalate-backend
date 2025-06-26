
using MediatR;

public class GetMyApplicationsQueryHandler : IRequestHandler<GetMyApplicationsQuery, PaginatedResponse<ApplicationListItemDto>>
{
    private readonly IApplicationRepository _applicationRepo;
    private readonly IJobRepository _jobRepo;
    private readonly IUserRepository _userRepo;
    public GetMyApplicationsQueryHandler(IApplicationRepository applicationRepo, IJobRepository jobRepo, IUserRepository userRepo)
    {
        _applicationRepo = applicationRepo;
        _jobRepo = jobRepo;
        _userRepo = userRepo;
    }
    public async Task<PaginatedResponse<ApplicationListItemDto>> Handle(GetMyApplicationsQuery request, CancellationToken cancellationToken)
    {
        var apps = await _applicationRepo.GetByApplicantIdAsync(request.ApplicantId, request.PageNumber, request.PageSize);
        var total = await _applicationRepo.CountByApplicantIdAsync(request.ApplicantId);
        var jobIds = apps.Select(a => a.JobId).ToList();
        var jobs = jobIds.Count > 0 ? (await Task.WhenAll(jobIds.Select(id => _jobRepo.GetByIdAsync(id)))) : Array.Empty<Job>();
        var companies = jobs.Select(j => j?.CreatedBy).Distinct().Where(id => !string.IsNullOrEmpty(id)).ToList();
        var companyUsers = companies.Count > 0 ? (await Task.WhenAll(companies.Select(id => _userRepo.GetByIdAsync(id)))) : Array.Empty<User>();
        var result = apps.Select(a => {
            var job = jobs.FirstOrDefault(j => j?.Id == a.JobId);
            var company = companyUsers.FirstOrDefault(u => u?.Id == job?.CreatedBy);
            return new ApplicationListItemDto
            {
                ApplicationId = a.Id,
                JobTitle = job?.Title ?? "",
                CompanyName = company?.Name ?? "",
                Status = a.Status.ToString(),
                AppliedAt = a.AppliedAt
            };
        }).ToList();
        return PaginatedResponse<ApplicationListItemDto>.CreateSuccess(result, request.PageNumber, request.PageSize, total);
    }
} 