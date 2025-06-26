
using MediatR;

public class UpdateJobCommandHandler : IRequestHandler<UpdateJobCommand, UpdateJobResponse>
{
    private readonly IJobRepository _jobRepository;

    public UpdateJobCommandHandler(IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }

    public async Task<UpdateJobResponse> Handle(UpdateJobCommand request, CancellationToken cancellationToken)
    {
        var job = await _jobRepository.GetByIdAsync(request.JobId);
        if (job == null)
        {
            return new UpdateJobResponse
            {
                Success = false,
                Message = "Job not found"
            };
        }

        job.Title = request.Title;
        job.Description = request.Description;
        job.Location = request.Location;
        job.UpdatedAt = DateTime.UtcNow;

        await _jobRepository.UpdateAsync(job);

        return new UpdateJobResponse
        {
            Success = true,
            Message = "Job updated successfully",
            Object = job
        };
    }

}
