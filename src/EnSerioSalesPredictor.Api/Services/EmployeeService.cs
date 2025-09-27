using EnSerioSalesPredictor.Api.Contracts;
using EnSerioSalesPredictor.Api.Dtos;

namespace EnSerioSalesPredictor.Api.Services;

public class EmployeeService(IEmployeeRepository repository) : IEmployeeService
{
    private readonly IEmployeeRepository _repository = repository;

    public async Task<List<EmployeeDto>> GetEmployeesAsync()
    {
        var employees = await _repository.GetEmployeesAsync();

        var result = employees.Select(x => new EmployeeDto
        {
            EmpId = x.EmpId,
            FullName = x.FullName
        });

        return result.ToList();
    }
}