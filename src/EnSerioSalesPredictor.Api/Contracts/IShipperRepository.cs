using EnSerioSalesPredictor.Api.Models;

namespace EnSerioSalesPredictor.Api.Contracts;

public interface IShipperRepository
{
    Task<List<Shipper>> GetShipperAsync();
}