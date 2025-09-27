namespace EnSerioSalesPredictor.Api.Dtos;

public class CreateOrderDto
{
    public int CustomerId { get; set; }
    public int EmpId { get; set; }
    public int ShipperId { get; set; }
    public required string ShipName { get; set; }
    public required string ShipAddress { get; set; }
    public required string ShipCity { get; set; }
    public DateTime OrderDate { get; set; }
    public required string RequiredDate { get; set; }
    public required string ShippedDate { get; set; }
    public decimal Freight { get; set; }
    public required string ShipCountry { get; set; }
    public required CreateOrderDetailsDto createOrderDetails { get; set; }
}