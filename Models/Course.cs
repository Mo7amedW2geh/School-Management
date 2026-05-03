using MVC_Day2.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Models {
    public class Course {

        [Key]
        public int Id { get; set; }

        [Required, StringLength(50)]
        [UniqueName]
        public string Name { get; set; }

        [Required]
        [Range(10, 1000)]
        public int Degree { get; set; }

        [Required]
        [LessThanDegree]
        public int MinDegree { get; set; }
        public int? DeptId { get; set; }

        [ForeignKey("DeptId")]
        public virtual Department? Department { get; set; }

        public virtual ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();
        public virtual ICollection<CrsResult> Results { get; set; } = new List<CrsResult>();

    }
}
