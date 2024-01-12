using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationException = FluentValidation.ValidationException;

namespace Core.CrossCuttingConcerns.Validation.FluentValidation;

public static class ValidationTool
{
    public static async Task ValidateAsync(IValidator validator, object entity)
    {
        ValidationContext<object> context = new(entity);
        var result =await validator.ValidateAsync(context);
        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors);
        }
    }
}