using EnSerioSalesPredictor.Api.Dtos;
using FluentValidation;

namespace EnSerioSalesPredictor.Api.Validators;

public class CreateOrderDtoValidator : AbstractValidator<CreateOrderDto>
{
    public CreateOrderDtoValidator()
    {
        RuleFor(x => x.EmpId).GreaterThan(0);
        RuleFor(x => x.ShipperId).GreaterThan(0);
        RuleFor(x => x.ShipName).NotEmpty();
        RuleFor(x => x.ShipAddress).NotEmpty();
        RuleFor(x => x.ShipCity).NotEmpty();
        RuleFor(x => x.OrderDate).NotEmpty();
        RuleFor(x => x.RequiredDate).NotEmpty();
        RuleFor(x => x.ShippedDate)
            .NotEmpty()
            .LessThan(x => x.RequiredDate)
            .WithMessage("ShippedDate must be less than RequiredDate.");
        RuleFor(x => x.Freight).GreaterThanOrEqualTo(0);
        RuleFor(x => x.ShipCountry).NotEmpty();
        RuleFor(x => x.createOrderDetails).SetValidator(new CreateOrderDetailsDtoValidator());
    }
}

public class CreateOrderDetailsDtoValidator : AbstractValidator<CreateOrderDetailsDto>
{
    public CreateOrderDetailsDtoValidator()
    {
        RuleFor(x => x.ProductId).GreaterThan(0);
        RuleFor(x => x.UnitPrice).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Qty).GreaterThan(0);
        RuleFor(x => x.Discount).InclusiveBetween(0, 1);
    }
}