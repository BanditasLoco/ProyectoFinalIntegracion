using ExamenGrupalIntegracion.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ExamenGrupalIntegracion.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<DeptEmp> DeptEmps { get; set; }
        public DbSet<DeptManager> DeptManagers { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        public DbSet<Title> Titles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar clave compuesta para DeptEmp
            modelBuilder.Entity<DeptEmp>()
                .HasKey(de => new { de.EmpNo, de.DeptNo });

            // Configurar clave compuesta para DeptManager
            modelBuilder.Entity<DeptManager>()
                .HasKey(dm => new { dm.EmpNo, dm.DeptNo });

            // Configurar relación User -> Employee (1:1)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Employee)
                .WithOne(e => e.User)
                .HasForeignKey<User>(u => u.EmpNo);
        }
    }
}