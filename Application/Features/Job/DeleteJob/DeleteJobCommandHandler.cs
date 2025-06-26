using MediatR;

public class DeleteJobCommandHandler : IRequestHandler<DeleteJobCommand, DeleteJobResponse>
{
    private readonly IJobRepository _jobRepository;

    public DeleteJobCommandHandler(IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }

    public async Task<DeleteJobResponse> Handle(DeleteJobCommand request, CancellationToken cancellationToken)
    {
        var response = new DeleteJobResponse();

        try
        {
            var job = await _jobRepository.GetByIdAsync(request.JobId);
            if (job == null)
            {
                response.Success = false;
                response.Message = "Job not found.";
                return response;
            }

            await _jobRepository.DeleteAsync(job);
            response.Success = true;
            response.Message = "Job deleted successfully.";
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = "An error occurred while deleting the job.";
            response.Errors = new List<string> { ex.Message };
        }

        return response;
    }
}
