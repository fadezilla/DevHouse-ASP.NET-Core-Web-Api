using Microsoft.EntityFrameworkCore;
using devhouse.Models;

namespace devhouse.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<ProjectType> ProjectTypes { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Project>()
                .HasOne(p => p.ProjectType)
                .WithMany(pt => pt.Projects)
                .HasForeignKey(p => p.ProjectTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Project>()
                .HasOne(p => p.Team)
                .WithMany(t => t.Projects)
                .HasForeignKey(p => p.TeamId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Developer>()
                .HasKey(d => d.Id);

            modelBuilder.Entity<Developer>()
                .HasOne(d => d.Team)
                .WithMany(t => t.Developers)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Developer>()
                .HasOne(d => d.Role)
                .WithMany(r => r.Developers)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}