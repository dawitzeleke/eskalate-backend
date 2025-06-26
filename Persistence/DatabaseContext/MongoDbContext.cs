using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace backend.Persistence.DatabaseContext;

public class MongoDbSettings
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
}
 
public class MongoDbContext{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IMongoDatabase database)
    {
       _database = database;
    }

    public IMongoCollection<T> GetCollection<T>(string name)
    {
        return _database.GetCollection<T>(name);
    }

   
}