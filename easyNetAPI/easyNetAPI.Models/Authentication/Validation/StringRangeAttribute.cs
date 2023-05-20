using System;
using System.ComponentModel.DataAnnotations;

namespace easyNetAPI.Models.Authentication.Validation
{
    public class StringRangeAttribute : ValidationAttribute
    {
        public string[]? AllowableValues { get; set; }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            if (AllowableValues != null && AllowableValues.Contains(value?.ToString()))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Please enter a correct value");
        }
    }
}

