using SchoolManagement.Models;
using System.ComponentModel.DataAnnotations;

namespace MVC_Day2.Attributes {
    public class UniqueNameAttribute : ValidationAttribute {

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) {
            if (value == null) {
                return null;
            }
            string newName = value.ToString();
            var currentCourse = (Course)validationContext.ObjectInstance;
            var context = (SchoolManagementContext)validationContext.GetService(typeof(SchoolManagementContext));
            Course course = context.Courses.FirstOrDefault(c => c.Name == newName && c.Id != currentCourse.Id);
            if (course != null) {
                return new ValidationResult("Name must be unique");
            }
            return ValidationResult.Success;
        }

    }
}
