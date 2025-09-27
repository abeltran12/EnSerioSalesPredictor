namespace EnSerioSalesPredictor.Api.Dtos;

public class CreateOrderDetailsDto
{
    public int ProductId { get; set; }
    public decimal UnitPrice { get; set; }
    public int Qty { get; set; }
    public decimal Discount { get; set; }
}