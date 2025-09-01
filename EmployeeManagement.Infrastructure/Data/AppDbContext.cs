using EmployeeManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions <AppDbContext> options):DbContext(options)
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<Employee>(entity =>
            {
               //Primary Key
                entity.HasKey(e => e.Id);

               
                entity.Property(e => e.EmpName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.HasIndex(e => e.Email).IsUnique();

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(10);

               // Employee belongs to one Department
                entity.HasOne(e => e.Department)
                      .WithMany(d => d.Employees)
                      .HasForeignKey(e => e.DepartmentId)
                      .OnDelete(DeleteBehavior.SetNull); //When department delete in employee the department will be set null
            });

            modelBuilder.Entity<Department>(entity =>
            {
              
                entity.HasKey(d => d.Id);
              
                entity.Property(d => d.DepartmentName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
