using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Models;

namespace MVC_Day2.Controllers {
    public class CrsResultController : Controller {
        SchoolManagementContext context = new SchoolManagementContext();

        public IActionResult Create(int traineeId) {
            var model = new CrsResult { TraineeId = traineeId };
            ViewBag.Courses = new SelectList(context.Courses, "Id", "Name");
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CrsResult result) {
            var existing = context.Results
                .FirstOrDefault(r => r.TraineeId == result.TraineeId && r.CrsId == result.CrsId);

            var course = context.Courses.Find(result.CrsId);
            if (result.Degree < 0)
                ModelState.AddModelError("Degree", "Degree cannot be negative.");

            if (course != null && result.Degree > course.Degree)
                ModelState.AddModelError("Degree", $"Degree cannot exceed course maximum ({course.Degree}).");

            if (ModelState.IsValid) {
                if (existing != null) {
                    existing.Degree = result.Degree;
                    context.Update(existing);
                } else {
                    context.Results.Add(result);
                }
                context.SaveChanges();
                return RedirectToAction("ShowTraineeResult", "Trainee", new { id = result.TraineeId });
            }

            ViewBag.Courses = new SelectList(context.Courses, "Id", "Name", result.CrsId);
            return View(result);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id) {
            var result = context.Results.Find(id);
            if (result == null) return NotFound();

            context.Results.Remove(result);
            context.SaveChanges();

            return RedirectToAction("ShowTraineeResult", "Trainee", new { id = result.TraineeId });
        }
    }
}
