using Education.DAL.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.DAL
{
    public class EducationManageDbContext : DbContext
    {
        public EducationManageDbContext() : base("name=Education")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<EducationManageDbContext, Configuration>("Education"));
        }
        protected override void OnModelCreating(DbModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Course>()
            .HasOptional<User>(s => s.User)
            .WithMany()
            .HasForeignKey(s => s.UserId)
            .WillCascadeOnDelete(false);
            
            modelbuilder.Entity<LearningInfo>()
            .HasOptional<ClassRoom>(s => s.ClassRoom)
            .WithMany()
            .HasForeignKey(s => s.RoomId)
            .WillCascadeOnDelete(false);

            modelbuilder.Entity<LearningInfo>()
            .HasOptional<Exam>(s => s.Exam)
            .WithMany()
            .HasForeignKey(s => s.ExamId)
            .WillCascadeOnDelete(false);

            modelbuilder.Entity<LearningInfo>()
            .HasOptional<User>(s => s.User)
            .WithMany()
            .HasForeignKey(s => s.UserId)
            .WillCascadeOnDelete(false);
        }
        public virtual DbSet<ClassRoom> ClassRooms { get; set; }

        public virtual DbSet<Blog> Blogs { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Course> Courses { get; set; }

        public virtual DbSet<GroupUser> GroupUsers { get; set; }

        public virtual DbSet<Exam> Exams { get; set; }

        public virtual DbSet<Candicate> Candicates { get; set; }

        public virtual DbSet<Faulty> Faulties { get; set; }
    }

}
