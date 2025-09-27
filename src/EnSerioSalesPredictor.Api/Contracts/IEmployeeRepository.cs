using EnSerioSalesPredictor.Api.Models;

namespace EnSerioSalesPredictor.Api.Contracts;

public interface IEmployeeRepository
{
    Task<List<Employee>> GetEmployeesAsync();
}