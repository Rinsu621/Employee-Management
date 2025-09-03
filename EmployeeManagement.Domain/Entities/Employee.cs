using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Domain.Entities
{
    public class Employee
    { 
        public int Id { get; set; }
        public string? EmpName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }

        //Foreign Key to Department
        public int? DepartmentId { get; set; }

        public Department? Department { get; set; }

        //Authentication and Authorization purpose
        public string? PasswordHash { get; set; }
        public string? Role { get; set; }


        //Many to Many relationship with Project entity can be added here in future
        public ICollection<Project> Projects { get; set; } = new List<Project>();
    }
}
