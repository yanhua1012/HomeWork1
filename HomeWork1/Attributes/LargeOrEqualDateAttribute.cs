using System;
using System.ComponentModel.DataAnnotations;

namespace HomeWork1.Attributes
{
    public class LargeOrEqualDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime dt = (DateTime) value;
            if (DateTime.Today >= dt)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(ErrorMessage ?? $"日期不得大於今日");
            }
        }
    }
}