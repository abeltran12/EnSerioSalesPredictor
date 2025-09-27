using System.ComponentModel.DataAnnotations;

namespace EnSerioSalesPredictor.Api.RequestFeautures;

public class RequestParameters
{
    private const int maxPageSize = 10;

    [Range(1, int.MaxValue, ErrorMessage = "PageNumber must be greater than or equal to 1.")]
    public int PageNumber { get; set; } = 1;
    [Range(1, maxPageSize, ErrorMessage = "PageSize must be between 1 and 50.")]
    public int PageSize { get; set; } = 10;
    public string? OrderBy { get; set; }
    public string? Sort { get; set; }
}