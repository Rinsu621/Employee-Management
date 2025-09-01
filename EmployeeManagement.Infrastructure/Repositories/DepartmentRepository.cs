using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces;
using EmployeeManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Infrastructure.Repositories
{
    public class DepartmentRepository(AppDbContext dbContext):IDepartmentRepository
    {
        public async Task<IEnumerable<Department>> GetDepartment()
        {
            return await dbContext.Departments.ToListAsync();
        }

        public async Task<IEnumerable<Department>> GetAllDepartmentsWithEmpAsync()
        {
            return await dbContext.Departments.Include(d => d.Employees).ToListAsync();
        }

        public async Task<Department> GetDepartmentByIdAsync(int deptId)
        {
            var department=await dbContext.Departments.Include(d => d.Employees).FirstOrDefaultAsync(d => d.Id == deptId);
            if(department == null)
            {
                throw new KeyNotFoundException($"Department with ID {deptId} not found.");
            }
            return department;
        }

        public async Task<Department> AddDepartmentAsync(Department entity)
        {
            dbContext.Departments.Add(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Department> UpdateDepartmentAsync(int deptId, Department entity)
        {
            var department = await dbContext.Departments.FirstOrDefaultAsync(d => d.Id == deptId);
            if (department == null)
            {
                throw new KeyNotFoundException($"Department with ID {deptId} not found.");
            }
            department.DepartmentName = entity.DepartmentName;
            await dbContext.SaveChangesAsync();
            return department;
        }

        public async Task<bool> DeleteDepartmentAsync(int deptId)
        {
            var department = await dbContext.Departments.FirstOrDefaultAsync(d => d.Id == deptId);
            if (department == null)
            {
                throw new KeyNotFoundException($"Department with ID {deptId} not found.");
            }
            dbContext.Departments.Remove(department);
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
