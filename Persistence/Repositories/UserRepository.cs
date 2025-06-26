using backend.Persistence.DatabaseContext;
using backend.Persistence.Repositories;
using MongoDB.Driver;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly IMongoCollection<User> _users;

    public UserRepository(MongoDbContext context) : base(context)
    {
        _users = context.GetCollection<User>(nameof(User));
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        return await _users.Find(user => user.Email == email).FirstOrDefaultAsync();
    }

    public async Task<bool> IsEmailUniqueAsync(string email)
    {
        var user = await GetByEmailAsync(email);
        return user == null;
    }

    public async Task<User> GetByIdAsync(string id)
    {
        return await _users.Find(user => user.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _users.Find(_ => true).ToListAsync();
    }
}