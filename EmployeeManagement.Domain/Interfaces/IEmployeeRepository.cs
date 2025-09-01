using EmployeeManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Domain.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployee();

        Task<Employee> GetEmployeeByIdAsync(int id);

        Task<Employee> AddEmployeeByAsync(Employee entity);

        Task<Employee> UpdateEmployeeAsync(int empId, Employee entity);

        Task<bool> DeleteEmployeeAsync(int empId);

        Task<Employee> GetEmployeeByEmailAsync(string email);

    }
}
