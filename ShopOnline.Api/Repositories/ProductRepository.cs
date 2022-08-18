using Microsoft.EntityFrameworkCore;
using ShopOnline.Api.Data;
using ShopOnline.Api.Entities;
using ShopOnline.Api.Repositories.Contracts;


namespace ShopOnline.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopOnlineDbContext shopOnlineDbContext;

        public ProductRepository(ShopOnlineDbContext shopOnlineDbContext)
        {
            this.shopOnlineDbContext = shopOnlineDbContext;
        }
        public async Task<IEnumerable<ProductCategory>> GetCategories()
        {
            var categories = await this.shopOnlineDbContext.ProductCategories.ToListAsync();

            return categories;

        }

        public async Task<ProductCategory> GetCategory(int id)
        {
            var category = await shopOnlineDbContext.ProductCategories.SingleOrDefaultAsync(c => c.Id == id);
#pragma warning disable CS8603 // Possible null reference return.
            return category;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<Product> GetItem(int id)
        {
            var product = await shopOnlineDbContext.Products
                                .Include(p => p.Name)
                                .SingleOrDefaultAsync(p => p.Id == id);
#pragma warning disable CS8603 // Possible null reference return.
            return product;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<IEnumerable<Product>> GetItems()
        {
            var products = await this.shopOnlineDbContext.Products
                                     .Include(p => p.Name).ToListAsync();

            return products;

        }

        public async Task<IEnumerable<Product>> GetItemsByCategory(int id)
        {
            var products = await this.shopOnlineDbContext.Products
                                     .Include(p => p.Name)
                                     .Where(p => p.CategoryId == id).ToListAsync();
            return products;
        }
    }
}
