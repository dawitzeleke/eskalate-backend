using MediatR;
using Application.Dtos;
using backend.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

public class GetJobDetailsQuery : IRequest<BaseResponse<JobDetailsDto>>
{
    public string JobId { get; set; }
}

public class GetJobDetailsQueryHandler : IRequestHandler<GetJobDetailsQuery, BaseResponse<JobDetailsDto>>
{
    private readonly JobRepository _jobRepo;
    private readonly UserRepository _userRepo;
    public GetJobDetailsQueryHandler(JobRepository jobRepo, UserRepository userRepo)
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
            CreatedAt = job.CreatedAt
        });
    }
} 