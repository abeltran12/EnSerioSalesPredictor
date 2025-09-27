using EnSerioSalesPredictor.Api.Dtos;
using EnSerioSalesPredictor.Api.Models;
using EnSerioSalesPredictor.Api.RequestFeautures;

namespace EnSerioSalesPredictor.Api.Contracts;

public interface IOrderRepository
{
    Task<List<Order>> GetOrdersAsync(int id, RequestParameters parameters);

    Task<int> CreateOrderAsync(int id, CreateOrderDto orderDto);
}