namespace EnSerioSalesPredictor.Api.Dtos;

public class OrderDto
{
    public int OrderId { get; set; }
    public required string RequiredDate { get; set; }
    public required string ShippedDate { get; set; }
    public required string ShipName { get; set; }
    public required string ShipAddress { get; set; }
    public required string ShipCity { get; set; }
}