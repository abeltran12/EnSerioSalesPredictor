using EnSerioSalesPredictor.Api.Models;

namespace EnSerioSalesPredictor.Api.Contracts;

public interface IProductRepository
{
    Task<List<Product>> GetProductsAsync();
}