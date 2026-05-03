using SchoolManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace MVC_Day2.Repository {
    public class DepartmentRepository {
        SchoolManagementContext context;

        public DepartmentRepository() {
            context = new SchoolManagementContext();
        }

        public void Add(Department department) {
            context.Departments.Add(department);
        }

        public void Update(Department department) {
            context.Departments.Update(department);
        }

        public void Delete(int id) {
            var department = Get(id);
            if (department != null) {
                var instructors = context.Instructors.Where(i => i.DeptId == id).ToList();
                foreach (var inst in instructors) {
                    inst.DeptId = null;
                }

                var courses = context.Courses.Where(c => c.DeptId == id).ToList();
                foreach (var crs in courses) {
                    crs.DeptId = null;
                }

                var trainees = context.Trainees.Where(t => t.DeptId == id).ToList();
                foreach (var trn in trainees) {
                    trn.DeptId = null;
                }

                context.Departments.Remove(department);
            }
        }

        public Department Get(int id) {
            var department = context.Departments
                .Include(d => d.Manger)
                .Include(d => d.Instructors)
                .Include(d => d.Trainees)
                .Include(d => d.Courses)
                .FirstOrDefault(i => i.Id == id);

            return department;
        }

        public List<Department> GetAll() {
            var departments = context.Departments
                .Include(d => d.Manger)
                .Include(d => d.Instructors)
                .Include(d => d.Trainees)
                .Include(d => d.Courses)
                .ToList();

            return departments;
        }

        public void Save() {
            context.SaveChanges();
        }
    }
}
