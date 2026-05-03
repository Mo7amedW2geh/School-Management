using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Models {
    public class Trainee {

        [Key]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }
        public string? Imag { get; set; }
        public string? Address { get; set; }

        [RegularExpression("A|B|C|D|F", ErrorMessage = "Grade must be one of: A, B, C, D, F.")]
        public string? Grade { get; set; }
        public int? DeptId { get; set; }

        [ForeignKey("DeptId")]
        public virtual Department? Department { get; set; }

        public virtual ICollection<CrsResult> Results { get; set; } = new List<CrsResult>();

    }
}
