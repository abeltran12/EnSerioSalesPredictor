using EnSerioSalesPredictor.Api.Contracts;
using EnSerioSalesPredictor.Api.Models;
using EnSerioSalesPredictor.Api.RequestFeautures;
using EnSerioSalesPredictor.Api.Services;
using Moq;

namespace EnSerioSalesPredictor.Api.Test.Services;

public class OrderServiceTests
{
    [Fact]
    public async Task GetOrdersAsync_WhenNoParameters_Returns3Records()
    {
        // Arrange
        var mockRepo = new Mock<IOrderRepository>();
        var fakeOrders = new List<Order>
    {
        new() { OrderId = 1, RequiredDate = "2025-01-15", ShippedDate = "2025-01-10", ShipName = "ACME Corp", ShipAddress = "123 Main St", ShipCity = "Bogotá" },
        new() { OrderId = 2, RequiredDate = "2025-01-16", ShippedDate = "2025-01-11", ShipName = "Tech Solutions", ShipAddress = "456 Oak Ave", ShipCity = "Medellín" },
        new() { OrderId = 3, RequiredDate = "2025-01-17", ShippedDate = "2025-01-12", ShipName = "Global Trade", ShipAddress = "789 Pine Rd", ShipCity = "Cali" }
    };

        mockRepo.Setup(x => x.GetOrdersAsync(It.IsAny<int>(), It.IsAny<RequestParameters>()))
               .ReturnsAsync(fakeOrders);

        var service = new OrderService(mockRepo.Object);
        var parameters = new RequestParameters();

        // Act
        var result = await service.GetOrdersAsync(1, parameters);

        // Assert
        Assert.Equal(3, result.Count);
        Assert.Equal("ACME Corp", result.First().ShipName);
    }

    [Fact]
    public async Task GetOrdersAsync_WhenSortDesc_ReturnsInvertedOrder()
    {
        // Arrange
        var mockRepo = new Mock<IOrderRepository>();
        var fakeOrders = new List<Order>
        {
            new() { OrderId = 3, RequiredDate = "2025-01-17", ShippedDate = "2025-01-12", ShipName = "Global Trade", ShipAddress = "789 Pine Rd", ShipCity = "Cali" },
            new() { OrderId = 2, RequiredDate = "2025-01-16", ShippedDate = "2025-01-11", ShipName = "Tech Solutions", ShipAddress = "456 Oak Ave", ShipCity = "Medellín" },
            new() { OrderId = 1, RequiredDate = "2025-01-15", ShippedDate = "2025-01-10", ShipName = "ACME Corp", ShipAddress = "123 Main St", ShipCity = "Bogotá" }
        };

        mockRepo.Setup(x => x.GetOrdersAsync(It.IsAny<int>(), It.IsAny<RequestParameters>()))
               .ReturnsAsync(fakeOrders);

        var service = new OrderService(mockRepo.Object);
        var parameters = new RequestParameters { Sort = "desc" };

        // Act
        var result = await service.GetOrdersAsync(1, parameters);

        // Assert
        Assert.Equal("Global Trade", result.First().ShipName);
        Assert.Equal("ACME Corp", result.Last().ShipName);
    }
}