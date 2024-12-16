using GYMFeeManagement_System_BE.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace GYMFeeManagement_System_BE.Database
{
    public class GymDbContext : DbContext
    {
        public GymDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<TrainingProgram> TrainingPrograms { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<EnrollProgram> EnrollPrograms { get; set; }
        // public DbSet<LastId> LastId { get; set; }
        public DbSet<ProgramType> ProgramTypes { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<WorkoutPlan> WorkoutPlans { get; set; }
        public DbSet<WorkoutEnrollment> WorkoutEnrollments { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Alert> Alerts { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ContactUsMessage> ContactUsMessages { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        public DbSet<Image> Images { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EnrollProgram>()
                .HasOne(ep => ep.member)
                .WithMany(m => m.EnrollPrograms)
                .HasForeignKey(ep => ep.MemberId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EnrollProgram>()
                .HasOne(ep => ep.TrainingProgram)
                .WithMany(tp => tp.EnrollPrograms)
                .HasForeignKey(ep => ep.ProgramId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Member>()
                .HasOne(m => m.Staff)
                .WithMany(s => s.Members)
                .HasForeignKey(m => m.TrainerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Member>()
                .HasOne(m => m.Branch)
                .WithMany(b => b.Members)
                .HasForeignKey(m => m.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Staff>()
                .HasOne(s => s.Branch)
                .WithMany(b => b.Staffs)
                .HasForeignKey(s => s.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Member)
                .WithMany(m => m.Payments)
                .HasForeignKey(p => p.MemberId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Alert>()
                .HasOne(a => a.member)
                .WithMany(m => m.Alerts)
                .HasForeignKey(a => a.MemberId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Alert>()
                .HasOne(a => a.TrainingProgram)
                .WithMany(tp => tp.Alerts)
                .HasForeignKey(a => a.ProgramId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Request>()
                .HasOne(r => r.member)
                .WithMany(m => m.Requests)
                .HasForeignKey(r => r.MemberId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Request>()
                .HasOne(r => r.trainingProgram)
                .WithMany(t => t.Requests)
                .HasForeignKey(r => r.ProgramId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<WorkoutPlan>()
                .HasOne(wp => wp.Staff)
                .WithMany(s => s.WorkoutPlans)
                .HasForeignKey(wp => wp.StaffId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<WorkoutEnrollment>()
                .HasOne(tp => tp.Member)
                .WithMany(m => m.WorkoutEnrollments)
                .HasForeignKey(tp => tp.MemberId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TrainingProgram>()
                .HasOne(tp => tp.ProgramType)
                .WithMany(pt => pt.TrainingPrograms)
                .HasForeignKey(tp => tp.TypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
               .HasOne(r => r.Member)  
               .WithMany(m => m.Reviews) 
               .HasForeignKey(r => r.MemberId)  
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Branch>()
               .HasOne(b => b.Address)
               .WithOne(a => a.Branch)
               .HasForeignKey<Address>(a => a.BranchId);

            modelBuilder.Entity<Member>()
                .HasOne(m => m.Address)
                .WithOne(a => a.Member)
                .HasForeignKey<Address>(a => a.MemberId);

            modelBuilder.Entity<Staff>()
                .HasOne(a => a.Address)
                .WithOne(a => a.Staff)
                .HasForeignKey<Address>(a => a.StaffId);

            modelBuilder.Entity<Request>()
                .HasOne(a => a.Address)
                .WithOne(a => a.Request)
                .HasForeignKey<Address>(a => a.RequestId);

            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Request>()
                .Property(r => r.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Alert>()
                .Property(a => a.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<TrainingProgram>()
                .Property(a => a.Cost)
                .HasPrecision(8, 2);

            base.OnModelCreating(modelBuilder);
        }


    }
}
