using EnSerioSalesPredictor.Api.Dtos;

namespace EnSerioSalesPredictor.Api.Contracts;

public interface IProductService
{
    Task<List<ProductDto>> GetProductsAsync();
}