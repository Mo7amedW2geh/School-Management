using SchoolManagement.Models;
using System.ComponentModel.DataAnnotations;

namespace MVC_Day2.Attributes {
    public class ValidCourseDegreeAttribute : ValidationAttribute {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
            var result = (CrsResult)validationContext.ObjectInstance;
            var context = (SchoolManagementContext)validationContext.GetService(typeof(SchoolManagementContext));

            if (value is int degree) {
                if (degree < 0)
                    return new ValidationResult("Degree cannot be negative.");

                var course = context.Courses.Find(result.CrsId);
                if (course != null && degree > course.Degree)
                    return new ValidationResult($"Degree cannot exceed course maximum ({course.Degree}).");
            }

            return ValidationResult.Success;
        }
    }
}
