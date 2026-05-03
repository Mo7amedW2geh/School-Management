using MVC_Day2.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Models {
    public class CrsResult {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Degree is required.")]
        [ValidCourseDegree]
        public int Degree { get; set; }

        [Required]
        public int CrsId { get; set; }

        [Required]
        public int TraineeId { get; set; }

        [ForeignKey("CrsId")]
        public virtual Course? Course { get; set; }

        [ForeignKey("TraineeId")]
        public virtual Trainee? Trainee { get; set; }



    }
}
