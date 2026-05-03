using SchoolManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace MVC_Day2.Repository {
    public class CourseRepository {
        SchoolManagementContext context;

        public CourseRepository() {
            context = new SchoolManagementContext();
        }

        public void Add(Course course) {
            context.Courses.Add(course);
        }

        public void Update(Course course) {
            context.Courses.Update(course);
        }

        public void Delete(int id) {
            var course = Get(id);
            if (course != null) {
                var instructors = context.Instructors.Where(i => i.CrsId == id).ToList();
                foreach (var inst in instructors) {
                    inst.CrsId = null;
                }

                var results = context.Results.Where(r => r.CrsId == id).ToList();
                context.Results.RemoveRange(results);

                context.Courses.Remove(course);
            }
        }

        public Course Get(int id) {
            var course = context.Courses
                .Include(c => c.Department)
                .FirstOrDefault(i => i.Id == id);

            return course;
        }

        public List<Course> GetAll() {
            var courses = context.Courses
                .Include(c => c.Department)
                .ToList();

            return courses;
        }

        public Course GetCourseResults(int crsId) {
            var course = context.Courses
                .Include(c => c.Results)
                .ThenInclude(r => r.Trainee)
                .FirstOrDefault(c => c.Id == crsId);
            return course;
        }

        public void Save() {
            context.SaveChanges();
        }
    }
}
