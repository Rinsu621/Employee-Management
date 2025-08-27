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
        public string EmpName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
