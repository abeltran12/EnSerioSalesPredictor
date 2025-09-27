using EnSerioSalesPredictor.Api.Contracts;
using EnSerioSalesPredictor.Api.Dtos;

namespace EnSerioSalesPredictor.Api.Services;

public class ProductService(IProductRepository repository) : IProductService
{
    private readonly IProductRepository _repository = repository;

    public async Task<List<ProductDto>> GetProductsAsync()
    {
        var products = await _repository.GetProductsAsync();

        var result = products.Select(x => new ProductDto
        {
            ProductId = x.ProductId,
            ProductName = x.ProductName,
        });

        return result.ToList();
    }
}