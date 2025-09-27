namespace EnSerioSalesPredictor.Api.Models;

public class SalesPrediction
{
    public required string CustomerName { get; set; }
    public required string LastOrderDate { get; set; }
    public required string NextPredictedOrder { get; set; }
}