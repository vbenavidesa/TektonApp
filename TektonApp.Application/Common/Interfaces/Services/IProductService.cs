using TektonApp.Application.Dtos.Request;
using TektonApp.Application.Dtos.Response;
using TektonApp.Application.Dtos.Response.Helpers;

namespace TektonApp.Application.Common.Interfaces.Services
{
    public interface IProductService
    {
        Task<ProductResponseDto> GetProductByIdAsync(int Id);
        Task<List<ProductResponseDto>> GetProductsAsync();
        Task<ServiceResultDto> CreateProductAsync(ProductRequestDto Product);
        Task<ServiceResultDto> UpdateProductAsync(ProductRequestDto Product);
        Task<ServiceResultDto> DeleteProductAsync(int Id);
    }
}
