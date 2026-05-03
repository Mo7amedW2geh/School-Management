using SchoolManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace MVC_Day2.Repository {
    public class InstructorRepository {
        SchoolManagementContext context;

        public InstructorRepository() {
            context = new SchoolManagementContext();
        }

        public void Add(Instructor instructor) {
            if (string.IsNullOrEmpty(instructor.Imag)) {
                instructor.Imag = "default.png";
            }

            context.Instructors.Add(instructor);
        }

        public void Update(Instructor instructor) {
            context.Instructors.Update(instructor);
        }

        public void Delete(int id) {
            var instructor = Get(id);
            if (instructor != null) {
                var departments = context.Departments.Where(d => d.MangerId == id).ToList();
                foreach (var dept in departments) {
                    dept.MangerId = null;
                }

                context.Instructors.Remove(instructor);
            }
        }

        public Instructor Get(int id) {
            var instructor = context.Instructors
                .Include(i => i.Department)
                .Include(i => i.Course)
                .FirstOrDefault(i => i.Id == id);

            return instructor;
        }

        public List<Instructor> GetAll() {
            var instructors = context.Instructors
                .Include(i => i.Department)
                .Include(i => i.Course)
                .ToList();

            return instructors;
        }

        public void Save() {
            context.SaveChanges();
        }
    }
}
