using Business.Dtos.Requests.Category;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.Category;

public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryRequest>
{

    public UpdateCategoryValidator()
    {
        RuleFor(p => p.Name).NotEmpty();






    }

}
