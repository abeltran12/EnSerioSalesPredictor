using EnSerioSalesPredictor.Api.Contracts;
using EnSerioSalesPredictor.Api.Models;
using EnSerioSalesPredictor.Api.Services;
using Moq;

namespace EnSerioSalesPredictor.Api.Test.Services;

public class EmployeeServiceTests
{
    [Fact]
    public async Task GetEmployeesAsync_ReturnsEmployeeDtos()
    {
        // Arrange
        var mockRepo = new Mock<IEmployeeRepository>();
        mockRepo.Setup(x => x.GetEmployeesAsync())
               .ReturnsAsync(
               [
                   new() { EmpId = 1, FullName = "Juan Pérez" },
                   new() { EmpId = 2, FullName = "María García" }
               ]);

        var service = new EmployeeService(mockRepo.Object);

        // Act
        var result = await service.GetEmployeesAsync();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal("Juan Pérez", result[0].FullName);
    }

    [Fact]
    public async Task GetEmployeesAsync_WhenNoEmployees_ReturnsEmptyList()
    {
        // Arrange
        var mockRepo = new Mock<IEmployeeRepository>();
        mockRepo.Setup(x => x.GetEmployeesAsync())
               .ReturnsAsync([]);

        var service = new EmployeeService(mockRepo.Object);

        // Act
        var result = await service.GetEmployeesAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}