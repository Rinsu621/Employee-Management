using EmployeeManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Domain.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetDepartment();
        Task<IEnumerable<Department>> GetAllDepartmentsWithEmpAsync();
        Task<Department> GetDepartmentByIdAsync(int deptId);
        Task<Department> AddDepartmentAsync(Department entity);
        Task<Department> UpdateDepartmentAsync(int deptId, Department entity);
        Task<bool> DeleteDepartmentAsync(int deptId);
    }
}
