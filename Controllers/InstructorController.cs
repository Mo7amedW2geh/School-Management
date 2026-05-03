using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Day2.Repository;
using SchoolManagement.Models;

namespace SchoolManagement.Controllers {
    [Authorize]
    public class InstructorController : Controller {
        SchoolManagementContext context = new SchoolManagementContext();
        InstructorRepository repository = new InstructorRepository();

        public IActionResult Index() {
            var instructors = repository.GetAll();

            return View(instructors);
        }

        public IActionResult Details(int id) {
            var instructor = repository.Get(id);

            if (instructor == null) return NotFound();

            return View(instructor);
        }

        [HttpGet]
        public IActionResult Create() {
            ViewBag.Departments = new SelectList(context.Departments, "Id", "Name");
            ViewBag.Courses = new SelectList(context.Courses, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Instructor instructor) {
            if (ModelState.IsValid) {
                repository.Add(instructor);
                repository.Save();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Departments = new SelectList(context.Departments, "Id", "Name", instructor.DeptId);
            ViewBag.Courses = new SelectList(context.Courses, "Id", "Name", instructor.CrsId);
            return View(instructor);
        }

        [HttpGet]
        public IActionResult Edit(int id) {
            var instructor = repository.Get(id);
            if (instructor == null) return NotFound();

            ViewBag.Departments = new SelectList(context.Departments, "Id", "Name", instructor.DeptId);
            ViewBag.Courses = new SelectList(context.Courses, "Id", "Name", instructor.CrsId);
            return View(instructor);
        }

        [HttpPost]
        public IActionResult Edit(Instructor instructor) {
            if (ModelState.IsValid) {
                repository.Update(instructor);
                repository.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Departments = new SelectList(context.Departments, "Id", "Name", instructor.DeptId);
            ViewBag.Courses = new SelectList(context.Courses, "Id", "Name", instructor.CrsId);
            return View(instructor);
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
