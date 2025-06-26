
using MediatR;

public class GetJobDetailsQueryHandler : IRequestHandler<GetJobDetailsQuery, BaseResponse<JobDetailsDto>>
{
    private readonly IJobRepository _jobRepo;
    private readonly IUserRepository _userRepo;
    public GetJobDetailsQueryHandler(IJobRepository jobRepo, IUserRepository userRepo)
    {
        _jobRepo = jobRepo;
        _userRepo = userRepo;
    }
    public async Task<BaseResponse<JobDetailsDto>> Handle(GetJobDetailsQuery request, CancellationToken cancellationToken)
    {
        var job = await _jobRepo.GetByIdAsync(request.JobId);
        if (job == null)
            return BaseResponse<JobDetailsDto>.Fail("Job not found");
        var creator = await _userRepo.GetByIdAsync(job.CreatedBy);
        return BaseResponse<JobDetailsDto>.CreateSuccess(new JobDetailsDto
        {
            JobId = job.Id,
            Title = job.Title,
            Description = job.Description,
            Location = job.Location,
            CreatedBy = creator?.Name ?? job.CreatedBy,
            CreatedAt = (DateTime)job.CreatedAt
        });
    }
} 