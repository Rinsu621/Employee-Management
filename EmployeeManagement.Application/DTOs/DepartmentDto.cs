using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.DTOs
{
    public class DepartmentDto
    {
        public int Id { get; set; }
        public string? departmentName { get; set; }
        public List<EmployeeDto> Employees { get; set; }
    }
}
