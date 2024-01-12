using Business.Dtos.Requests.Product;
using Entities.Concrete;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation.Product;

public class CreateProductValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductValidator()
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
