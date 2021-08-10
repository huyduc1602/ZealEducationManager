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
        //protected override void OnModelCreating(ModelBuilder modelbuilder)
        //{
        //    foreach (var relationship in modelbuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        //    {
        //        relationship.DeleteBehavior = DeleteBehavior.Restrict;
        //    }

        //    base.OnModelCreating(modelbuilder);
        //}
        public virtual DbSet<Batch> Batch { get; set; }

        public System.Data.Entity.DbSet<Education.DAL.Blog> Blogs { get; set; }

        public System.Data.Entity.DbSet<Education.DAL.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<Education.DAL.User> Users { get; set; }

        public System.Data.Entity.DbSet<Education.DAL.Course> Courses { get; set; }

        public System.Data.Entity.DbSet<Education.DAL.GroupUser> GroupUsers { get; set; }

        public System.Data.Entity.DbSet<Education.DAL.Exam> Exams { get; set; }

        public System.Data.Entity.DbSet<Education.DAL.Candicate> Candicates { get; set; }

        public System.Data.Entity.DbSet<Education.DAL.Faulty> Faulties { get; set; }
        public System.Data.Entity.DbSet<Education.DAL.Bussiness> Bussinesses { get; set; }
        public System.Data.Entity.DbSet<Education.DAL.Permission> Permissions { get; set; }
        public System.Data.Entity.DbSet<Education.DAL.GroupPermission> GroupPermissions { get; set; }
    }

}
