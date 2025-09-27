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

        var pagedList = new PagedList<Order>(fakeOrders, fakeOrders.Count, 1, 10);
        
        mockRepo.Setup(x => x.GetOrdersAsync(It.IsAny<int>(), It.IsAny<RequestParameters>()))
               .ReturnsAsync(pagedList);

        var service = new OrderService(mockRepo.Object);
        var parameters = new RequestParameters { PageNumber = 1, PageSize = 10 };

        // Act
        var result = await service.GetOrdersAsync(1, parameters);

        // Assert
        Assert.Equal(3, result.metaData.TotalCount);
        Assert.Equal("ACME Corp", result.Item1[0].ShipName);
        Assert.Equal(3, result.metaData.TotalCount);
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

        var pagedList = new PagedList<Order>(fakeOrders, fakeOrders.Count, 1, 10);
        
        mockRepo.Setup(x => x.GetOrdersAsync(It.IsAny<int>(), It.IsAny<RequestParameters>()))
               .ReturnsAsync(pagedList);

        var service = new OrderService(mockRepo.Object);
        var parameters = new RequestParameters { Sort = "desc", PageNumber = 1, PageSize = 10 };

        // Act
        var result = await service.GetOrdersAsync(1, parameters);

        // Assert
        Assert.Equal("Global Trade", result.Item1[0].ShipName);
        Assert.Equal("ACME Corp", result.Item1[2].ShipName);
        Assert.Equal(3, result.metaData.TotalCount);
    }

    [Fact]
    public async Task GetOrdersAsync_WithPagination_ReturnsCorrectMetadata()
    {
        // Arrange
        var mockRepo = new Mock<IOrderRepository>();
        var fakeOrders = new List<Order>
        {
            new() { OrderId = 1, RequiredDate = "2025-01-15", ShippedDate = "2025-01-10", ShipName = "ACME Corp", ShipAddress = "123 Main St", ShipCity = "Bogotá" },
            new() { OrderId = 2, RequiredDate = "2025-01-16", ShippedDate = "2025-01-11", ShipName = "Tech Solutions", ShipAddress = "456 Oak Ave", ShipCity = "Medellín" }
        };

        var pagedList = new PagedList<Order>(fakeOrders, 2, 2, 2);
        
        mockRepo.Setup(x => x.GetOrdersAsync(It.IsAny<int>(), It.IsAny<RequestParameters>()))
               .ReturnsAsync(pagedList);

        var service = new OrderService(mockRepo.Object);
        var parameters = new RequestParameters { PageNumber = 2, PageSize = 2 };

        // Act
        var result = await service.GetOrdersAsync(1, parameters);

        // Assert
        Assert.Equal(2, result.metaData.TotalCount);
        Assert.Equal(2, result.metaData.CurrentPage);
        Assert.Equal(2, result.metaData.PageSize);
        Assert.Equal(1, result.metaData.TotalPages);
        Assert.True(result.metaData.HasPrevious);
        Assert.False(result.metaData.HasNext);
    }

    [Fact]
    public async Task GetOrdersAsync_EmptyResult_ReturnsEmptyPagedList()
    {
        // Arrange
        var mockRepo = new Mock<IOrderRepository>();
        var emptyOrders = new List<Order>();
        var pagedList = new PagedList<Order>(emptyOrders, 0, 1, 10);
        
        mockRepo.Setup(x => x.GetOrdersAsync(It.IsAny<int>(), It.IsAny<RequestParameters>()))
               .ReturnsAsync(pagedList);

        var service = new OrderService(mockRepo.Object);
        var parameters = new RequestParameters { PageNumber = 1, PageSize = 10 };

        // Act
        var result = await service.GetOrdersAsync(1, parameters);

        // Assert
        Assert.Equal(0, result.metaData.TotalCount);
        Assert.Empty(result.Item1);
        Assert.False(result.metaData.HasNext);
        Assert.False(result.metaData.HasPrevious);
    }
}