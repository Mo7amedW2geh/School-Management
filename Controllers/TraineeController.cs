using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Day2.Repository;
using SchoolManagement.Models;

namespace SchoolManagement.Controllers {
    [Authorize]
    public class TraineeController : Controller {
        SchoolManagementContext context = new SchoolManagementContext();
        TraineeRepository repository = new TraineeRepository();

        public IActionResult Index() {
            var traines = repository.GetAll();

            return View(traines);
        }

        public IActionResult Details(int id) {
            var traine = repository.Get(id);

            if (traine == null) {
                return NotFound();
            }

            return View(traine);
        }

        public IActionResult ShowResult(int id, int crsId) {
            var result = repository.GetResult(id, crsId);

            if (result == null) return NotFound();

            return View(result);
        }

        public IActionResult ShowTraineeResult(int id) {
            var trainee = repository.GetTraineeResults(id);

            if (trainee == null) return NotFound();

            return View(trainee);
        }

        [HttpGet]
        public IActionResult Create() {
            ViewBag.Departments = new SelectList(context.Departments, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Trainee trainee) {
            if (ModelState.IsValid) {
                repository.Add(trainee);
                repository.Save();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Departments = new SelectList(context.Departments, "Id", "Name", trainee.DeptId);
            return View(trainee);
        }

        [HttpGet]
        public IActionResult Edit(int id) {
            var trainee = repository.Get(id);
            if (trainee == null) return NotFound();

            ViewBag.Departments = new SelectList(context.Departments, "Id", "Name", trainee.DeptId);
            return View(trainee);
        }

        [HttpPost]
        public IActionResult Edit(Trainee trainee) {
            if (ModelState.IsValid) {
                repository.Update(trainee);
                repository.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Departments = new SelectList(context.Departments, "Id", "Name", trainee.DeptId);
            return View(trainee);
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
