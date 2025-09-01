using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Commands
{
    //Assignment is being sent and  employee is being return
    public record AddEmployeeToDepartmentCommand(AddEmployeeToDepartmentDto Assignment) : IRequest<Employee>;

    public class AddEmployeeToDepartmentCommandHandler( IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository)
        : IRequestHandler<AddEmployeeToDepartmentCommand, Employee>
    {
        public async Task<Employee> Handle(AddEmployeeToDepartmentCommand request, CancellationToken cancellationToken)
        {
            // Check if the employee exists
            var employee = await employeeRepository.GetEmployeeByIdAsync(request.Assignment.EmployeeId);
            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee with ID {request.Assignment.EmployeeId} not found.");
            }
            //Check if the department exists
            var department = await departmentRepository.GetDepartmentByIdAsync(request.Assignment.DepartmentId);
            if (department == null)
            {
                throw new KeyNotFoundException($"Department with ID {request.Assignment.DepartmentId} not found.");
            }
            //Assign department to employee
            employee.DepartmentId = request.Assignment.DepartmentId;
            //Update Employee in DB and return it
            var updatedEmployee = await employeeRepository.UpdateEmployeeAsync(employee.Id, employee);

            return updatedEmployee;
        }
    }
}
