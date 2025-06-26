
using MediatR;

public class UpdateApplicationStatusCommandHandler : IRequestHandler<UpdateApplicationStatusCommand, BaseResponse<ApplicationForJobDto>>
{
    private readonly IApplicationRepository _applicationRepo;
    private readonly IJobRepository _jobRepo;
    private readonly IUserRepository _userRepo;
    public UpdateApplicationStatusCommandHandler(IApplicationRepository applicationRepo, IJobRepository jobRepo, IUserRepository userRepo)
    {
        _applicationRepo = applicationRepo;
        _jobRepo = jobRepo;
        _userRepo = userRepo;
    }
    public async Task<BaseResponse<ApplicationForJobDto>> Handle(UpdateApplicationStatusCommand request, CancellationToken cancellationToken)
    {
        var app = await _applicationRepo.GetByIdAsync(request.ApplicationId);
        if (app == null)
            return BaseResponse<ApplicationForJobDto>.Fail("Application not found");
        var job = await _jobRepo.GetByIdAsync(app.JobId);
        if (job == null)
            return BaseResponse<ApplicationForJobDto>.Fail("Job not found");
        if (job.CreatedBy != request.CompanyId)
            return BaseResponse<ApplicationForJobDto>.Fail("Unauthorized");
        if (!Enum.TryParse<StatusEnum>(request.NewStatus, out var newStatus))
            return BaseResponse<ApplicationForJobDto>.Fail("Invalid status");
        app.Status = newStatus;
        await _applicationRepo.UpdateAsync(app);
        var applicant = await _userRepo.GetByIdAsync(app.ApplicantId);
        return BaseResponse<ApplicationForJobDto>.CreateSuccess(new ApplicationForJobDto
        {
            ApplicationId = app.Id,
            ApplicantName = applicant?.Name ?? "",
            ResumeLink = app.ResumeLink,
            CoverLetter = app.CoverLetter,
            Status = app.Status.ToString(),
            AppliedAt = app.AppliedAt
        });
    }
} 