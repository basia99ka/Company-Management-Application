using BaseLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace ServerLibrary.Data
{
    public class AppDbContext : DbContext
    {
        // Main
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Project> Projects { get; set; }

        // Department - Branch

        public DbSet<Department> Departments { get; set; }
        public DbSet<Branch> Branches { get; set; }
        
        //Authentication - Role - System Roles
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet <SystemRole> SystemRoles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet <RefreshTokenInfo> RefreshTokensInfos { get; set; }

        // Vactions
        public DbSet<Vacation> Vacations { get; set; }  
        public DbSet<VacationType> VacationTypes { get; set; }  


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasKey(e => e.Id);
        }
    }
}
