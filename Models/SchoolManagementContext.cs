using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SchoolManagement.Models {
    public class SchoolManagementContext : IdentityDbContext {

        public SchoolManagementContext() { }

        public SchoolManagementContext(DbContextOptions<SchoolManagementContext> options) : base(options) { }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CrsResult> Results { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Instructor> Instructors { get; set; }
        public virtual DbSet<Trainee> Trainees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Instructor>()
                .HasOne(i => i.Department)
                .WithMany(d => d.Instructors)
                .HasForeignKey(i => i.DeptId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Department>()
                .HasOne(d => d.Manger)
                .WithMany()
                .HasForeignKey(d => d.MangerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CrsResult>()
                .HasIndex(r => new { r.TraineeId, r.CrsId })
                .IsUnique();
        }

    }
}
