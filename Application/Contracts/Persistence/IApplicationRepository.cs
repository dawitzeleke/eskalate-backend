using backend.Application.Contracts.Persistence;

public interface IApplicationRepository : IGenericRepository<Application>
{
    Task<(List<Application> Applications, int TotalCount)> GetApplicationsForJobAsync(string jobId, int pageNumber, int pageSize);

    Task<Application?> GetApplicationByJobIdAndUserIdAsync(string jobId, string userId);

    Task<bool> IsUserAppliedToJobAsync(string jobId, string userId);

    Task<bool> UpdateApplicationStatusAsync(string applicationId, StatusEnum status);

    Task<int> GetByJobIdAsync(string jobId, int page, int pageSize);
    Task<int> CountByJobIdAsync(string jobId);
    Task<List<Application>> GetByApplicantIdAsync(string applicantId, int page, int pageSize);
    Task<int> CountByApplicantIdAsync(string applicantId);
}