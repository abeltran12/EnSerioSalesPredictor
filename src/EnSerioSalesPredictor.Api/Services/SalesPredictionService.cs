using EnSerioSalesPredictor.Api.Contracts;
using EnSerioSalesPredictor.Api.Dtos;
using EnSerioSalesPredictor.Api.Models;
using EnSerioSalesPredictor.Api.RequestFeautures;

namespace EnSerioSalesPredictor.Api.Services;

public class SalesPredictionService(ISalesPredictionRepository repository) : ISalesPredictionService
{
    private readonly ISalesPredictionRepository _repository = repository;

    public async Task<(PagedList<SalesPredictionDto>, MetaData metaData)> GetSalesPredictionsAsync(
        RequestParameters parameters)
    {
        var salesPredictions = await _repository.GetSalesPredictionsAsync(parameters);
        PagedList<SalesPredictionDto> pagedList =
            MapToDto(parameters, salesPredictions.Items, salesPredictions.TotalCount);

        return (pagedList, pagedList.MetaData);
    }

    private static PagedList<SalesPredictionDto> MapToDto(
        RequestParameters parameters, List<SalesPrediction> salesPredictions, int total)
    {
        var result = salesPredictions
                    .Select(x => new SalesPredictionDto
                    {
                        CustomerName = x.CustomerName,
                        LastOrderDate = x.LastOrderDate,
                        NextPredictedOrder = x.NextPredictedOrder,
                    }).ToList();

        var pagedList = new PagedList<SalesPredictionDto>(
            result,
            total,
            parameters.PageNumber,
            parameters.PageSize
        );
        return pagedList;
    }
}