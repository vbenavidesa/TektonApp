using TektonApp.Domain.Entities;

namespace TektonApp.Application.Common.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<ProductMaster> GetProductByIdAsync(int Id);
        Task<List<ProductMaster>> GetProductsAsync();
        Task<ProductMaster> CreateProductAsync(ProductMaster Product);
        Task<ProductMaster> UpdateProductAsync(ProductMaster Product);
        Task<ProductMaster> DeleteProductAsync(ProductMaster Product);
        Task<bool> SaveChangesAsync();
    }
}
