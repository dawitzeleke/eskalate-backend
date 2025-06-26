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
            throw new Exception("Job not found");

        var applications = await _applicationRepo.GetApplicationsForJobAsync(request.JobId, request.PageNumber, request.PageSize);
        var totalCount = await _applicationRepo.CountByJobIdAsync(request.JobId);

        var applicationDtos = applications.Select(a => new ApplicationForJobDto
        {
            ApplicantName = $"{a.Applicant.FirstName} {a.Applicant.LastName}",
            ResumeLink = a.ResumeLink,
            CoverLetter = a.CoverLetter,
            Status = a.Status.ToString(),
            AppliedAt = a.AppliedAt
        }).ToList();

        return new PaginatedResponse<ApplicationForJobDto>.CreateSuccess(applicationDtos, totalCount, request.PageNumber, request.PageSize);
    }
}