using backend.Persistence.DatabaseContext;
using backend.Persistence.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

public class JobRepository : GenericRepository<Job>, IJobRepository
{
    public JobRepository(MongoDbContext context) : base(context)
    {
        _collection = context.GetCollection<Job>(nameof(Job));
    }
    public async Task<(List<Job> Jobs, int TotalCount)> BrowseJobsAsync(JobFilter filter, int pageNumber, int pageSize)
    {
        var query = _collection.AsQueryable();

        if (!string.IsNullOrEmpty(filter.Title))
        {
            query = query.Where(job => job.Title.Contains(filter.Title));
        }

        if (!string.IsNullOrEmpty(filter.Location))
        {
            query = query.Where(job => job.Location.Contains(filter.Location));
        }

        if (!string.IsNullOrEmpty(filter.CompanyId))
        {
            query = query.Where(job => job.CreatedBy == filter.CompanyId);
        }

        var totalCount = await query.CountAsync();
        var jobs = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (jobs, totalCount);
    }

}