using Microsoft.EntityFrameworkCore;
using ShopMania.API.Data;
using ShopMania.API.Entities;
using ShopMania.API.Repositories.Contracts;

namespace ShopMania.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopManiaDbContext shopManiaDbContext;

        public ProductRepository(ShopManiaDbContext shopManiaDbContext)
        {
            this.shopManiaDbContext = shopManiaDbContext;
        }
        public async Task<IEnumerable<ProductCategory>> GetCategories()
        {
            var categories = await this.shopManiaDbContext.ProductCategories.ToListAsync();
            return categories;
        }

        public async Task<ProductCategory> GetCategory(int id)
        {
            var category = await this.shopManiaDbContext.ProductCategories.SingleOrDefaultAsync(
                                                        c => c.Id == id);
            return category;
        }

        public async Task<Product> GetItem(int id)
        {
            var product = await this.shopManiaDbContext.Products.FindAsync(id);

            return product;
        }

        public async Task<IEnumerable<Product>> GetItems()
        {
            var products = await this.shopManiaDbContext.Products.ToListAsync();

            return products;
        }
    }
}
