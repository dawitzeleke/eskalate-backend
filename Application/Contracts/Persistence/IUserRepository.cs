using backend.Application.Contracts.Persistence;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User> GetByEmailAsync(string email);
    Task<bool> IsEmailUniqueAsync(string email);
    Task<User> GetByIdAsync(string id);
    Task<IEnumerable<User>> GetAllUsersAsync();
}