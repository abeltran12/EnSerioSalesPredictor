namespace EnSerioSalesPredictor.Api.Dtos;

public class SalesPredictionDto
{
    public required string CustomerName { get; set; }
    public required string LastOrderDate { get; set; }
    public required string NextPredictedOrder { get; set; }
}