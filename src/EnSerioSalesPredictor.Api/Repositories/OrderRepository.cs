using System.Data;
using Dapper;
using EnSerioSalesPredictor.Api.Context;
using EnSerioSalesPredictor.Api.Contracts;
using EnSerioSalesPredictor.Api.Dtos;
using EnSerioSalesPredictor.Api.Models;
using EnSerioSalesPredictor.Api.RequestFeautures;

namespace EnSerioSalesPredictor.Api.Repositories;

public class OrderRepository(DapperContext context) : IOrderRepository
{
    private readonly DapperContext _context = context;

    public async Task<(List<Order>, int TotalCount)> GetOrdersAsync(int id, RequestParameters parameters)
    {
        var dynamicParameters = new DynamicParameters();
        dynamicParameters.Add("CustomerId", id, DbType.Int32);
        dynamicParameters.Add("PageNumber", parameters.PageNumber, DbType.Int32);
        dynamicParameters.Add("PageSize", parameters.PageSize, DbType.Int32);
        dynamicParameters.Add("SortColumn", parameters.OrderBy, DbType.String);
        dynamicParameters.Add("SortDirection", parameters.Sort, DbType.String);

        using var connection = _context.CreateConnection();

        var orders = await connection.QueryMultipleAsync(
            "SP_GET_CLIENT_ORDERS", dynamicParameters,
            commandType: CommandType.StoredProcedure
        );

        var totalCount = await orders.ReadFirstAsync<int>();
        var items = (await orders.ReadAsync<Order>()).ToList();

        return (items, totalCount);
    }

    public async Task<int> CreateOrderAsync(int id, CreateOrderDto orderDto)
    {
        var dynamicParameters = new DynamicParameters();
        dynamicParameters.Add("EmpId", orderDto.EmpId, DbType.Int32);
        dynamicParameters.Add("CustId", id, DbType.Int32);
        dynamicParameters.Add("OrderDate", orderDto.OrderDate, DbType.Date);
        dynamicParameters.Add("RequiredDate", orderDto.RequiredDate, DbType.Date);
        dynamicParameters.Add("ShippedDate", orderDto.ShippedDate, DbType.Date);
        dynamicParameters.Add("ShipperId", orderDto.ShipperId, DbType.Int32);
        dynamicParameters.Add("Freight", orderDto.Freight, DbType.Decimal);
        dynamicParameters.Add("ShipName", orderDto.ShipName, DbType.String);
        dynamicParameters.Add("ShipAddress", orderDto.ShipAddress, DbType.String);
        dynamicParameters.Add("ShipCity", orderDto.ShipCity, DbType.String);
        dynamicParameters.Add("ShipCountry", orderDto.ShipCountry, DbType.String);
        dynamicParameters.Add("ProductId", orderDto.createOrderDetails.ProductId, DbType.Int32);
        dynamicParameters.Add("UnitPrice", orderDto.createOrderDetails.UnitPrice, DbType.Decimal);
        dynamicParameters.Add("Quantity", orderDto.createOrderDetails.Qty, DbType.Int32);
        dynamicParameters.Add("Discount", orderDto.createOrderDetails.Discount, DbType.Decimal);

        using var connection = _context.CreateConnection();

        var result = await connection.ExecuteScalarAsync<int>(
            "SP_ADD_ORDER", dynamicParameters,
            commandType: CommandType.StoredProcedure
        );

        return result;
    }
}