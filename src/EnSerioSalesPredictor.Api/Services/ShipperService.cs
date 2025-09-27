using EnSerioSalesPredictor.Api.Contracts;
using EnSerioSalesPredictor.Api.Dtos;

namespace EnSerioSalesPredictor.Api.Services;

public class ShipperService(IShipperRepository repository) : IShipperService
{
    private readonly IShipperRepository _repository = repository;

    public async Task<List<ShipperDto>> GetShipperAsync()
    {
        var shippers = await _repository.GetShipperAsync();

        var result = shippers.Select(x => new ShipperDto
        {
            ShipperId = x.ShipperId,
            CompanyName = x.CompanyName
        }).ToList();

        return result;
    }
}