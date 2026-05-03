using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Models {
    public class Department {

        [Key]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }
        public int? MangerId { get; set; }

        [ForeignKey("MangerId")]
        public virtual Instructor? Manger { get; set; }

        public virtual ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();
        public virtual ICollection<Trainee> Trainees { get; set; } = new List<Trainee>();
        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    }
}
