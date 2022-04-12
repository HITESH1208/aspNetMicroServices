using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;
        public ProductRepository(ICatalogContext context)
        {
            _context = context;
        }
        public async Task CreateProduct(Product productData)
        {
            _context.Products.InsertOne(productData);
        }

        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            var deleteResult = await _context.Products.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount>0;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
           return await _context.Products.Find(p=>true).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string category)
        {
            return await _context.Products.Find(p => p.Category == category).ToListAsync();
        }

        public async Task<Product> GetProductById(string id)
        {
            return await _context.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            return await _context.Products.Find(p => p.Name == name).ToListAsync();

        }

        public async Task<bool> UpdateProduct(Product productData)
        {
            var UpdateResult = await _context.Products.ReplaceOneAsync(filter: p=> p.Id==productData.Id,replacement:productData);

            return UpdateResult.IsAcknowledged && UpdateResult.ModifiedCount > 0;
        }
    }
}
