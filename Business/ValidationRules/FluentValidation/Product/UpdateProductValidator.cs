using Business.Dtos.Requests.Product;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.Product;

public class UpdateProductValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductValidator()
    {
        RuleFor(p => p.Name).NotEmpty();
        RuleFor(p => p.Stock).NotNull();
        RuleFor(p => p.Stock).GreaterThanOrEqualTo(0);
        RuleFor(p => p.UnitPrice).NotNull();
        RuleFor(p => p.UnitPrice).GreaterThan(0);
        RuleFor(p => p.CategoryId).NotNull();
        RuleFor(p => p.CategoryId).GreaterThan(0);



    }

}