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
    public  class EmployeeRepository(AppDbContext dbContext): IEmployeeRepository
    {
        public async Task <IEnumerable<Employee>> GetEmployee()
        {
            return await dbContext.Employees.ToListAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            var employee= await dbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if(employee == null)
            {
                throw new KeyNotFoundException($"Employee with ID {id} not found.");
            }

            return await dbContext.Employees.FindAsync(id);
        }

        public async Task<Employee> AddEmployeeByAsync(Employee entity)
        {
            dbContext.Employees.Add(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Employee>UpdateEmployeeAsync(int empId, Employee entity)
        {
            var employee = await dbContext.Employees.FirstOrDefaultAsync(x => x.Id == empId);
           if(employee == null)
            {
                throw new KeyNotFoundException($"Employee with ID {empId} not found.");
            }
                employee.EmpName =entity.EmpName;
                employee.Email = entity.Email;
                employee.Phone = entity.Phone;
          await dbContext.SaveChangesAsync();
            return employee;
        }

        public async Task<bool> DeleteEmployeeAsync(int empId)
        {
            var employee = await dbContext.Employees.FirstOrDefaultAsync(x => x.Id == empId);
            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee with ID {empId} not found.");
            }
            dbContext.Employees.Remove(employee);
            await dbContext.SaveChangesAsync();
            return true;
        }

    }
   
}
