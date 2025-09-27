using System.Data;
using Dapper;
using EnSerioSalesPredictor.Api.Context;
using EnSerioSalesPredictor.Api.Contracts;
using EnSerioSalesPredictor.Api.Models;
using EnSerioSalesPredictor.Api.RequestFeautures;

namespace EnSerioSalesPredictor.Api.Repositories;

public class SalesPredictionRepository(DapperContext context) : ISalesPredictionRepository
{
    private readonly DapperContext _context = context;

    public async Task<List<SalesPrediction>> GetSalesPredictionsAsync(RequestParameters parameters)
    {
        var dynamicParameters = new DynamicParameters();
        dynamicParameters.Add("PageNumber", parameters.PageNumber, DbType.Int32);
        dynamicParameters.Add("PageSize", parameters.PageSize, DbType.Int32);
        dynamicParameters.Add("SortColumn", parameters.OrderBy, DbType.String);
        dynamicParameters.Add("SortDirection", parameters.Sort, DbType.String);

        using var connection = _context.CreateConnection();

        var shippers = await connection.QueryAsync<SalesPrediction>(
            "SP_GET_SALES_PREDICTION", dynamicParameters,
            commandType: CommandType.StoredProcedure
        );

        return shippers.ToList();
    }
}