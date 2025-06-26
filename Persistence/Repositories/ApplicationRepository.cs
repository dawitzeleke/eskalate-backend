using backend.Persistence.DatabaseContext;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

public class ApplicationRepository
{
    private readonly MongoDbContext _context;
    public ApplicationRepository(MongoDbContext context)
    {
        _context = context;
    }

    public async Task<Application> GetByIdAsync(string id)
    {
        return await _context.GetCollection<Application>("Applications").Find(a => a.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<Application>> GetByApplicantIdAsync(string applicantId, int page, int pageSize)
    {
        return await _context.GetCollection<Application>("Applications")
            .Find(a => a.ApplicantId == applicantId)
            .Skip((page - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync();
    }

    public async Task<int> CountByApplicantIdAsync(string applicantId)
    {
        return (int)await _context.GetCollection<Application>("Applications")
            .CountDocumentsAsync(a => a.ApplicantId == applicantId);
    }

    public async Task<List<Application>> GetByJobIdAsync(string jobId, int page, int pageSize)
    {
        return await _context.GetCollection<Application>("Applications")
            .Find(a => a.JobId == jobId)
            .Skip((page - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync();
    }

    public async Task<int> CountByJobIdAsync(string jobId)
    {
        return (int)await _context.GetCollection<Application>("Applications")
            .CountDocumentsAsync(a => a.JobId == jobId);
    }

    public async Task<Application> GetByApplicantAndJobAsync(string applicantId, string jobId)
    {
        return await _context.GetCollection<Application>("Applications")
            .Find(a => a.ApplicantId == applicantId && a.JobId == jobId)
            .FirstOrDefaultAsync();
    }

    public async Task CreateAsync(Application application)
    {
        await _context.GetCollection<Application>("Applications").InsertOneAsync(application);
    }

    public async Task UpdateAsync(Application application)
    {
        await _context.GetCollection<Application>("Applications").ReplaceOneAsync(a => a.Id == application.Id, application);
    }

    public async Task<bool> UpdateApplicationStatusAsync(string applicationId, StatusEnum status)
    {
        var result = await _context.GetCollection<Application>("Applications")
            .UpdateOneAsync(a => a.Id == applicationId, Builders<Application>.Update.Set(a => a.Status, status));
        return result.ModifiedCount > 0;
    }
    public async Task<List<Application>> GetByJobIdAsync(string jobId, int pageNumber, int pageSize)
    {
        return await _context.GetCollection<Application>("Applications")
            .Find(a => a.JobId == jobId)
            .Skip((pageNumber - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync();
    }
} 