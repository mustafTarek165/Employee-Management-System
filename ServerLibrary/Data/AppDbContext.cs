using BaseLibrary.Entities;
using BaseLibrary.Entities.IdentityEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
       : base(options)
        {

        }
        public DbSet<Employee> Employees {  get; set; }

        // GeneralDepartment/Department/Branch
        public DbSet<GeneralDepartment> GeneralDepartments { get; set; }
        public  DbSet<Department>Departments  { get; set; }
      public  DbSet<Branch> Branchs { get; set; }

       
       //Country/City/Town
        public DbSet<Town> Towns { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }

        //Authentication/Role/SystemRoles
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<SystemRole> SystemRoles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RefreshTokenInfo>RefreshTokenInfos { get; set; }

        //Other/Vacation/Sanction/Doctor/Overtime
        public DbSet<Vacation> Vacations { get; set; }
        public DbSet<VacationType> VacationTypes { get; set; }
        public DbSet<Overtime> Overtimes { get; set; }
        public DbSet<OvertimeType> OvertimeTypes { get; set; }
        public DbSet<Sanction> Sanctions { get; set; }
        public DbSet<SanctionType> SanctionTypes { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
  
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Department>()
              .HasOne(d => d.generalDepartment)
              .WithMany(gd => gd.departments)
              .HasForeignKey(d => d.GeneralDepartmentId)
              .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Branch>()
             .HasOne(d => d.Department)
             .WithMany(gd => gd.Branches)
             .HasForeignKey(d => d.DepartmentId)
             .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
