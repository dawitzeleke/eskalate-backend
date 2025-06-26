
using MediatR;

public class GetApplicationsForJobQueryHandler : IRequestHandler<GetApplicationsForJobQuery, PaginatedResponse<ApplicationForJobDto>>
{
    private readonly IApplicationRepository _applicationRepo;
    private readonly IJobRepository _jobRepo;
    private readonly IUserRepository _userRepo;
    public GetApplicationsForJobQueryHandler(IApplicationRepository applicationRepo, IJobRepository jobRepo, IUserRepository userRepo)
    {
        _applicationRepo = applicationRepo;
        _jobRepo = jobRepo;
        _userRepo = userRepo;
    }
    public async Task<PaginatedResponse<ApplicationForJobDto>> Handle(GetApplicationsForJobQuery request, CancellationToken cancellationToken)
    {
        var job = await _jobRepo.GetByIdAsync(request.JobId);
        if (job == null)
            return PaginatedResponse<ApplicationForJobDto>.Fail("Job not found");
        if (job.CreatedBy != request.CompanyId)
            return PaginatedResponse<ApplicationForJobDto>.Fail("Unauthorized access");
        var apps = await _applicationRepo.GetByJobIdAsync(request.JobId, request.PageNumber, request.PageSize);
        var total = await _applicationRepo.CountByJobIdAsync(request.JobId);
        var applicantIds = apps.Select(a => a.ApplicantId).Distinct().ToList();
        var applicants = applicantIds.Count > 0 ? (await Task.WhenAll(applicantIds.Select(id => _userRepo.GetByIdAsync(id)))) : Array.Empty<User>();
        var result = apps.Select(a => {
            var applicant = applicants.FirstOrDefault(u => u?.Id == a.ApplicantId);
            return new ApplicationForJobDto
            {
                ApplicantName = applicant?.Name ?? "",
                ResumeLink = a.ResumeLink,
                CoverLetter = a.CoverLetter,
                Status = a.Status.ToString(),
                AppliedAt = a.AppliedAt
            };
        }).ToList();
        return PaginatedResponse<ApplicationForJobDto>.CreateSuccess(result, request.PageNumber, request.PageSize, total);
    }
} 