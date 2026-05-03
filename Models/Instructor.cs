using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Models {
    public class Instructor {

        [Key]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }
        public string? Imag {  get; set; }
        public decimal? Salary { get; set; }
        public string? Address { get; set; }
        public int? DeptId { get; set; }
        public int? CrsId { get; set; }

        [ForeignKey("DeptId")]
        public virtual Department? Department { get; set; }

        [ForeignKey("CrsId")]
        public virtual Course? Course { get; set; }

    }
}
