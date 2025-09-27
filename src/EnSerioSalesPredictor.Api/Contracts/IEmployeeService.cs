using EnSerioSalesPredictor.Api.Dtos;

namespace EnSerioSalesPredictor.Api.Contracts;

public interface IEmployeeService
{
    Task<List<EmployeeDto>> GetEmployeesAsync();
}