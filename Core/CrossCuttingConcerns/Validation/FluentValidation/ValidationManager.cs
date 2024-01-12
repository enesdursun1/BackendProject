using FluentValidation;

namespace Core.CrossCuttingConcerns.Validation.FluentValidation;

public static class ValidationManager
{
    
    public static async Task ValidateAsync(Type validatorType,object entity)
    {

        if (!typeof(IValidator).IsAssignableFrom(validatorType))
        {
            throw new ValidationException("Wrong Validation Type");
        }

        var validator = (IValidator)Activator.CreateInstance(validatorType);
        
        await ValidationTool.ValidateAsync(validator, entity);
       
    
    }


}