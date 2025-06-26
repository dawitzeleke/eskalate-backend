using backend.Application.Contracts.Persistence;

public interface IJobRepository : IGenericRepository<Job>
{
    Task<(List<Job> Jobs, int TotalCount)> BrowseJobsAsync(JobFilter filter, int pageNumber, int pageSize);

}