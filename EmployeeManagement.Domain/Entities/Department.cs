using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Domain.Entities
{
    public class Department
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }

        //one department can have many employees
        public ICollection<Employee> Employees { get; set; }=new List<Employee>();
    }
}
