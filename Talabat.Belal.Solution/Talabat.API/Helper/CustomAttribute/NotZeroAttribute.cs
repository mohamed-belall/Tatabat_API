using System.ComponentModel.DataAnnotations;

namespace Talabat.API.Helper.CustomAttribute
{
    public class NotZeroAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is int intValue && intValue == 0)
                return new ValidationResult("Value can't be Zero");

            return ValidationResult.Success;
        }
    }
}
