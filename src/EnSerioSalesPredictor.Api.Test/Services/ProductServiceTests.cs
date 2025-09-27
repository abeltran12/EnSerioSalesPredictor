using EnSerioSalesPredictor.Api.Contracts;
using EnSerioSalesPredictor.Api.Models;
using EnSerioSalesPredictor.Api.Services;
using Moq;

namespace EnSerioSalesPredictor.Api.Test.Services;

public class ProductServiceTests
{
    [Fact]
    public async Task GetProductsAsync_ReturnsProductDtos()
    {
        // Arrange
        var mockRepo = new Mock<IProductRepository>();
        mockRepo.Setup(x => x.GetProductsAsync())
               .ReturnsAsync(new List<Product>
               {
                   new() { ProductId = 1, ProductName = "Laptop HP" },
                   new() { ProductId = 2, ProductName = "Mouse Logitech" }
               });

        var service = new ProductService(mockRepo.Object);

        // Act
        var result = await service.GetProductsAsync();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal("Laptop HP", result[0].ProductName);
    }

    [Fact]
    public async Task GetProductsAsync_WhenNoProducts_ReturnsEmptyList()
    {
        // Arrange
        var mockRepo = new Mock<IProductRepository>();
        mockRepo.Setup(x => x.GetProductsAsync())
               .ReturnsAsync([]);

        var service = new ProductService(mockRepo.Object);

        // Act
        var result = await service.GetProductsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}