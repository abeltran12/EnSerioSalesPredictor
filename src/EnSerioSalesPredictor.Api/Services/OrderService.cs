using EnSerioSalesPredictor.Api.Contracts;
using EnSerioSalesPredictor.Api.Dtos;
using EnSerioSalesPredictor.Api.Models;
using EnSerioSalesPredictor.Api.RequestFeautures;

namespace EnSerioSalesPredictor.Api.Services;

public class OrderService(IOrderRepository repository) : IOrderService
{
    private readonly IOrderRepository _repository = repository;

    public async Task<(PagedList<OrderDto>, MetaData metaData)> GetOrdersAsync(int id, RequestParameters parameters)
    {
        var orders = await _repository.GetOrdersAsync(id, parameters);
        PagedList<OrderDto> pagedList = MapToDto(parameters, orders);

        return (pagedList, pagedList.MetaData);
    }

    public async Task<int> CreateOrderAsync(int id, CreateOrderDto orderDto)
    {
        var result = await _repository.CreateOrderAsync(id, orderDto);
        return result;
    }

    private static PagedList<OrderDto> MapToDto(RequestParameters parameters, List<Order> orders)
    {
        var result = orders
                    .Select(x => new OrderDto
                    {
                        OrderId = x.OrderId,
                        RequiredDate = x.RequiredDate,
                        ShipAddress = x.ShipAddress,
                        ShipCity = x.ShipCity,
                        ShipName = x.ShipName,
                        ShippedDate = x.ShippedDate
                    }).ToList();

        var pagedList = new PagedList<OrderDto>(
            result,
            orders.Count,
            parameters.PageNumber,
            parameters.PageSize
        );
        return pagedList;
    }
}