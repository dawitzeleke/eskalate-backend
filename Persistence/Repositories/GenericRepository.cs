// filepath: /home/maya/Documents/projects/capstone/capstoneProject/backend/Persistence/Repositories/GenericRepository.cs
using backend.Application.Contracts.Persistence;
using backend.Domain.Common;
using backend.Persistence.DatabaseContext;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected IMongoCollection<T> _collection;

        public GenericRepository(MongoDbContext context)
        {
            _collection = context.GetCollection<T>(typeof(T).Name);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var response = await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
            if (response.IsAcknowledged && response.ModifiedCount > 0)
            {
                return entity;
            }
            return null;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            var response = await _collection.DeleteOneAsync(x => x.Id == entity.Id);
            if (!response.IsAcknowledged)
            {
                return false;
            }
            return true;
        }

    }
}