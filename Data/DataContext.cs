using Microsoft.EntityFrameworkCore;
using devhouse.Models;

namespace devhouse.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<ProjectType> ProjectTypes { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectType>()
            .HasMany(a => a.Projects)
            .WithOne(p => p.ProjectType)
            .HasForeignKey(a => a.ProjectTypeId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Team>()
            .HasMany(a => a.Projects)
            .WithOne(p => p.Team)
            .HasForeignKey(a => a.TeamId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Team>()
            .HasMany(a => a.Developers)
            .WithOne(p => p.Team)
            .HasForeignKey(a => a.TeamId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Role>()
            .HasMany(a => a.Developers)
            .WithOne(p => p.Role)
            .HasForeignKey(a => a.RoleId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Developer>()
            .HasKey(ma => new { ma.RoleId, ma.TeamId });

            modelBuilder.Entity<Developer>()
            .HasOne(ma => ma.Role)
            .WithMany(m => m.Developers)
            .HasForeignKey(ma => ma.RoleId);

            modelBuilder.Entity<Developer>()
            .HasOne(ma => ma.Team)
            .WithMany(a => a.Developers)
            .HasForeignKey(ma => ma.TeamId);

             modelBuilder.Entity<Project>()
            .HasKey(ma => new { ma.TeamId, ma.ProjectTypeId });

            modelBuilder.Entity<Project>()
            .HasOne(ma => ma.ProjectType)
            .WithMany(m => m.Projects)
            .HasForeignKey(ma => ma.ProjectTypeId);

            modelBuilder.Entity<Project>()
            .HasOne(ma => ma.Team)
            .WithMany(a => a.Projects)
            .HasForeignKey(ma => ma.TeamId);
        }
    }
}