using SchoolManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace MVC_Day2.Repository {
    public class TraineeRepository {
        SchoolManagementContext context;

        public TraineeRepository() {
            context = new SchoolManagementContext();
        }

        public void Add(Trainee trainee) {
            if (string.IsNullOrEmpty(trainee.Imag)) {
                trainee.Imag = "default.png";
            }

            context.Trainees.Add(trainee);
        }

        public void Update(Trainee trainee) {
            context.Trainees.Update(trainee);
        }

        public void Delete(int id) {
            var trainee = Get(id);
            if (trainee != null) {
                var results = context.Results.Where(r => r.CrsId == id).ToList();
                context.Results.RemoveRange(results);

                context.Trainees.Remove(trainee);
            }
        }

        public Trainee Get(int id) {
            var trainee = context.Trainees
                .Include(t => t.Department)
                .FirstOrDefault(i => i.Id == id);

            return trainee;
        }

        public List<Trainee> GetAll() {
            var trainees = context.Trainees
                .Include(t => t.Department)
                .ToList();

            return trainees;
        }

        public CrsResult? GetResult(int traineeId, int crsId) {
            var result = context.Results
                .Include(r => r.Course)
                .Include(r => r.Trainee)
                .FirstOrDefault(r => r.TraineeId == traineeId && r.CrsId == crsId);
            return result;
        }

        public Trainee GetTraineeResults(int traineeId) {
            var trainee = context.Trainees
                .Include(t => t.Results)
                .ThenInclude(r => r.Course)
                .FirstOrDefault(t => t.Id == traineeId);
            return trainee;
        }

        public void Save() {
            context.SaveChanges();
        }
    }
}
