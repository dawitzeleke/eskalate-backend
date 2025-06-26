
using MediatR;

public class ApplyForJobCommandHandler : IRequestHandler<ApplyForJobCommand, BaseResponse<ApplicationListItemDto>>
{
    private readonly IApplicationRepository _applicationRepo;
    private readonly IJobRepository _jobRepo;
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IUserRepository _userRepo;
    public ApplyForJobCommandHandler(IApplicationRepository applicationRepo, IJobRepository jobRepo, ICloudinaryService cloudinaryService, IUserRepository userRepo)
    {
        _applicationRepo = applicationRepo;
        _jobRepo = jobRepo;
        _cloudinaryService = cloudinaryService;
        _userRepo = userRepo;
    }
    public async Task<BaseResponse<ApplicationListItemDto>> Handle(ApplyForJobCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.JobId) || request.Resume == null)
            return BaseResponse<ApplicationListItemDto>.Fail("JobId and Resume are required.");
        if (request.CoverLetter != null && request.CoverLetter.Length > 200)
            return BaseResponse<ApplicationListItemDto>.Fail("Cover letter must be under 200 characters.");
        var job = await _jobRepo.GetByIdAsync(request.JobId);
        if (job == null)
            return BaseResponse<ApplicationListItemDto>.Fail("Job not found.");
        var duplicate = await _applicationRepo.GetApplicationByJobIdAndUserIdAsync(request.ApplicantId, request.JobId);
        if (duplicate != null)
            return BaseResponse<ApplicationListItemDto>.Fail("You have already applied to this job.");
        var resumeUrl = await _cloudinaryService.UploadAsync(request.Resume);
        if (string.IsNullOrEmpty(resumeUrl))
            return BaseResponse<ApplicationListItemDto>.Fail("Resume upload failed or invalid file type.");
        var application = new Application
        {
            ApplicantId = request.ApplicantId,
            JobId = request.JobId,
            ResumeLink = resumeUrl,
            CoverLetter = request.CoverLetter,
            Status = StatusEnum.Applied,
            AppliedAt = DateTime.UtcNow
        };
        await _applicationRepo.CreateAsync(application);
        var company = await _userRepo.GetByIdAsync(job.CreatedBy);
        return BaseResponse<ApplicationListItemDto>.CreateSuccess(new ApplicationListItemDto
        {
            ApplicationId = application.Id,
            JobTitle = job.Title,
            CompanyName = company?.Name ?? "",
            Status = application.Status.ToString(),
            AppliedAt = application.AppliedAt
        });
    }
} 