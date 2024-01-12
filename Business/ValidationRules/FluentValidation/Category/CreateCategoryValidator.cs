using Business.Dtos.Requests.Category;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.Category;

public class CreateCategoryValidator : AbstractValidator<CreateCategoryRequest>
{
   
    public CreateCategoryValidator()
    {
        RuleFor(p => p.Name).NotEmpty();






    }

}
