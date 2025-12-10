using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebApi.Models;

namespace WebApi.DAL.DB
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<User> users { get; set; }
        public DbSet<Project> projects { get; set; }
        public DbSet<TaskItem> tasks { get; set; }
        public DbSet<ActivityLog> activityLog { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Project>().ToTable("Projects");
            modelBuilder.Entity<TaskItem>().ToTable("Tasks");
            modelBuilder.Entity<ActivityLog>().ToTable("ActivityLogs");

            foreach (IMutableForeignKey fk in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                fk.DeleteBehavior = DeleteBehavior.ClientCascade;
            }
        }
    }
}