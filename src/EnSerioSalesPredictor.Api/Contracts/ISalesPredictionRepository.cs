using EnSerioSalesPredictor.Api.Models;
using EnSerioSalesPredictor.Api.RequestFeautures;

namespace EnSerioSalesPredictor.Api.Contracts;

public interface ISalesPredictionRepository
{
    Task<(List<SalesPrediction> Items, int TotalCount)> GetSalesPredictionsAsync(RequestParameters parameters);
}