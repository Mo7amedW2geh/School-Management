using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Day2.Repository;
using SchoolManagement.Models;

namespace MVC_Day2.Controllers {
    [Authorize]
    public class DepartmentController : Controller {
        SchoolManagementContext context = new SchoolManagementContext();
        DepartmentRepository repository = new DepartmentRepository();

        public IActionResult Index() {
            var departments = repository.GetAll();

            return View(departments);
        }

        public IActionResult Details(int id) {
            var department = repository.Get(id);

            if (department == null) return NotFound();

            return View(department);
        }

        [HttpGet]
        public IActionResult Create() {
            ViewBag.Mangers = new SelectList(context.Instructors, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department department) {
            if (ModelState.IsValid) {
                repository.Add(department);
                repository.Save();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Mangers = new SelectList(context.Instructors, "Id", "Name", department.MangerId);
            return View(department);
        }

        [HttpGet]
        public IActionResult Edit(int id) {
            var department = repository.Get(id);
            if (department == null) return NotFound();

            ViewBag.Mangers = new SelectList(context.Instructors, "Id", "Name", department.MangerId);
            return View(department);
        }

        [HttpPost]
        public IActionResult Edit(Department department) {
            if (ModelState.IsValid) {
                repository.Update(department);
                repository.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Mangers = new SelectList(context.Instructors, "Id", "Name", department.MangerId);
            return View(department);
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
