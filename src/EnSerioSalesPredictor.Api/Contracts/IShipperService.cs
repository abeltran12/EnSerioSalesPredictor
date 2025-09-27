using EnSerioSalesPredictor.Api.Dtos;

namespace EnSerioSalesPredictor.Api.Contracts;

public interface IShipperService
{
    Task<List<ShipperDto>> GetShipperAsync();
}