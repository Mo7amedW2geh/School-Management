using System.ComponentModel.DataAnnotations;

namespace MVC_Day2.Attributes {
    public class LessThanDegreeAttribute : ValidationAttribute {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
            var currentValue = (int?)value;

            var property = validationContext.ObjectType.GetProperty("Degree");
            if (property == null)
                throw new ArgumentException("Property with this name not found");

            var comparisonValue = (int?)property.GetValue(validationContext.ObjectInstance);

            if (currentValue.HasValue && comparisonValue.HasValue && currentValue.Value >= comparisonValue.Value) {
                return new ValidationResult("Minimem Degree must be less than The Degree");
            }

            return ValidationResult.Success;
        }
    }
}
