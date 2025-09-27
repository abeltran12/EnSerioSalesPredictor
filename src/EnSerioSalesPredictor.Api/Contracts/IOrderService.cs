using EnSerioSalesPredictor.Api.Dtos;
using EnSerioSalesPredictor.Api.RequestFeautures;

namespace EnSerioSalesPredictor.Api.Contracts;

public interface IOrderService
{
    Task<PagedList<OrderDto>> GetOrdersAsync(int id, RequestParameters parameters);
    Task<int> CreateOrderAsync(int id, CreateOrderDto orderDto);
}