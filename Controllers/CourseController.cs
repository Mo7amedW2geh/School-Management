using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Day2.Repository;
using SchoolManagement.Models;

namespace SchoolManagement.Controllers {
    [Authorize]
    public class CourseController : Controller {
        SchoolManagementContext context = new SchoolManagementContext();
        CourseRepository repository = new CourseRepository();

        public IActionResult Index() {
            var courses = repository.GetAll();

            return View(courses);
        }

        public IActionResult ShowCourseResult(int id) {
            var course = repository.GetCourseResults(id);

            if (course == null) return NotFound();

            return View(course);
        }

        [HttpGet]
        public IActionResult Create() {
            ViewBag.Departments = new SelectList(context.Departments, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Course course) {
            if (ModelState.IsValid) {
                repository.Add(course);
                repository.Save();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Departments = new SelectList(context.Departments, "Id", "Name", course.DeptId);
            return View(course);
        }

        [HttpGet]
        public IActionResult Edit(int id) {
            var course = repository.Get(id);
            if (course == null) return NotFound();

            ViewBag.Departments = new SelectList(context.Departments, "Id", "Name", course.DeptId);
            return View(course);
        }

        [HttpPost]
        public IActionResult Edit(Course course) {
            if (ModelState.IsValid) {
                repository.Update(course);
                repository.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Departments = new SelectList(context.Departments, "Id", "Name", course.DeptId);
            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id) {
            repository.Delete(id);
            repository.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
