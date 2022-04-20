using Microsoft.EntityFrameworkCore;
using TektonApp.Application.Common.Interfaces;
using TektonApp.Application.Common.Interfaces.Repositories;
using TektonApp.Domain.Entities;
using TektonApp.Infrastructure.Attributes;

namespace TektonApp.Infrastructure.Implementations.Repositories
{
    [ScopedService]
    public class ProductRepository : IProductRepository
    {
        #region Constructor
        private readonly ITektonDbContext _context;
        public ProductRepository(ITektonDbContext context)
        {
            _context = context;
        }
        #endregion

        #region public async Task<ProductMaster> GetProductByIdAsync(int Id)
        public async Task<ProductMaster> GetProductByIdAsync(int Id)
        {
            return await _context.ProductMasters
                .Where(x => x.State != "D")
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == Id);
        }
        #endregion

        #region public async Task<List<ProductMaster>> GetProductsAsync()
        public async Task<List<ProductMaster>> GetProductsAsync()
        {
            return await _context.ProductMasters
                .Where(x => x.State != "D")
                .ToListAsync();
        }
        #endregion

        #region public async Task<ProductMaster> CreateProductAsync(ProductMaster Product)
        public async Task<ProductMaster> CreateProductAsync(ProductMaster Product)
        {
            await _context.ProductMasters.AddAsync(Product);
            await _context.SaveChangesAsync();
            return Product;
        }
        #endregion

        #region public async Task<ProductMaster> UpdateProductAsync(ProductMaster Product)
        public async Task<ProductMaster> UpdateProductAsync(ProductMaster Product)
        {
            _context.ProductMasters.Update(Product);
            await _context.SaveChangesAsync();
            return Product;
        }
        #endregion

        #region public async Task<ProductMaster> DeleteProductAsync(ProductMaster Product)
        public async Task<ProductMaster> DeleteProductAsync(ProductMaster Product)
        {
            _context.ProductMasters.Remove(Product);
            await _context.SaveChangesAsync();
            return Product;
        }
        #endregion

        #region public int SaveChangesAsync()
        public async Task<bool> SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
            return true;
        }
        #endregion
    }
}
