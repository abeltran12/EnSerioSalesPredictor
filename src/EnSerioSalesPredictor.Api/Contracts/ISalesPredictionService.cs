using EnSerioSalesPredictor.Api.Dtos;
using EnSerioSalesPredictor.Api.RequestFeautures;

namespace EnSerioSalesPredictor.Api.Contracts;

public interface ISalesPredictionService
{
    Task<(PagedList<SalesPredictionDto>, MetaData metaData)> GetSalesPredictionsAsync(RequestParameters parameters);
}