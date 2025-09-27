using Dapper;
using EnSerioSalesPredictor.Api.Context;
using EnSerioSalesPredictor.Api.Contracts;
using EnSerioSalesPredictor.Api.Models;

namespace EnSerioSalesPredictor.Api.Repositories;

public class EmployeeRepository(DapperContext context) : IEmployeeRepository
{
    private readonly DapperContext _context = context;

    public async Task<List<Employee>> GetEmployeesAsync()
    {
        using var connection = _context.CreateConnection();

        var shippers = await connection.QueryAsync<Employee>(
            "SP_GET_EMPLOYEES", 
            commandType: System.Data.CommandType.StoredProcedure
        );

        return shippers.ToList();
    }
}