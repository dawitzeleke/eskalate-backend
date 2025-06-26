
using backend.Application.Contracts.Persistence;
using backend.Persistence.DatabaseContext;
using backend.Persistence.Repositories;
using MongoDB.Driver;

public class ProductRepository : GenericRepository<Product>, IProductRepository

{
    private readonly IMongoCollection<Product> _products;

    public ProductRepository(MongoDbContext context) : base(context)
    {
        _products = context.GetCollection<Product>(typeof(Product).Name);
    }
    
}