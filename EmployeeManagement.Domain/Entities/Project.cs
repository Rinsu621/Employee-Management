using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Domain.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string? ProjectName { get; set; }

        public string? Description { get; set; }

        public ICollection<Employee> Employees { get; set; } =new List<Employee>(); // List used to avoid null reference exception
    }
}
