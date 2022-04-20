using Mapster;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using TektonApp.Application.Common.Interfaces.Repositories;
using TektonApp.Application.Common.Interfaces.Services;
using TektonApp.Application.Dtos.Request;
using TektonApp.Application.Dtos.Response;
using TektonApp.Application.Dtos.Response.External;
using TektonApp.Application.Dtos.Response.Helpers;
using TektonApp.Domain.Entities;
using TektonApp.Infrastructure.Attributes;

namespace TektonApp.Infrastructure.Implementations.Services
{
    [ScopedService]
    public class ProductService : IProductService
    {
        #region Constructor
        private readonly IConfiguration _config;
        private readonly IProductRepository _productRepository;
        public ProductService(IConfiguration config, IProductRepository productRepository)
        {
            _config = config;
            _productRepository = productRepository;
        }
        #endregion

        #region public async Task<ProductResponseDto> GetProductByIdAsync(int Id)
        public async Task<ProductResponseDto> GetProductByIdAsync(int Id)
        {
            var _product = await _productRepository.GetProductByIdAsync(Id);
            var _result = _product.Adapt<ProductResponseDto>();
            using (HttpClient _client = new HttpClient())
            {
                _client.BaseAddress = new Uri(_config.GetSection("BaseAddress").Value);
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await _client.GetAsync("discount-amount/" + _product.Id);
                if (response.IsSuccessStatusCode)
                {
                    var responseMessage = await response.Content.ReadAsStringAsync();
                    var _mock = JsonConvert.DeserializeObject<MockDiscountResponseDto>(responseMessage);
                    _result.Discount = _mock.Value;
                    _result.FinalPrice = _product.Price * ((100 - _result.Discount) / 100);
                }
                else
                {
                    _result.Discount = 0;
                    _result.FinalPrice = _product.Price;
                }

            }
            return _result;
        }
        #endregion

        #region public async Task<List<ProductResponseDto>> GetProductsAsync()
        public async Task<List<ProductResponseDto>> GetProductsAsync()
        {
            var _products = await _productRepository.GetProductsAsync();
            return _products.Adapt<List<ProductResponseDto>>();
        }
        #endregion

        #region public async Task<ServiceResult> CreateProductAsync(ProductRequestDto Product)
        public async Task<ServiceResultDto> CreateProductAsync(ProductRequestDto Product)
        {
            var _product = Product.Adapt<ProductMaster>();
            _product = await _productRepository.CreateProductAsync(_product);
            return new ServiceResultDto { StatusCode = 200, Result = _product.Adapt<ProductResponseDto>() };
        }
        #endregion

        #region public async Task<ServiceResult> UpdateProductAsync(ProductRequestDto Product)
        public async Task<ServiceResultDto> UpdateProductAsync(ProductRequestDto Product)
        {
            var _update = Product.Adapt<ProductMaster>();
            var _result = await _productRepository.UpdateProductAsync(_update);
            return new ServiceResultDto { StatusCode = 200, Result = _result.Adapt<ProductResponseDto>() };
        }
        #endregion

        #region public async Task<ServiceResult> DeleteProductAsync(int Id)
        public async Task<ServiceResultDto> DeleteProductAsync(int Id)
        {
            var _product = await _productRepository.GetProductByIdAsync(Id);
            _product = await _productRepository.DeleteProductAsync(_product);
            return new ServiceResultDto { StatusCode = 200, Result = _product.Adapt<ProductResponseDto>() };
        }
        #endregion
    }
}
