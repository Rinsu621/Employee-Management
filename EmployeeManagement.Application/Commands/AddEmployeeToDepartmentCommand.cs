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
    public record AddEmployeeToDepartmentCommand(AddEmployeeToDepartmentDto Assignment) : IRequest<Employee>;

    public class AddEmployeeToDepartmentCommandHandler(
        IEmployeeRepository employeeRepository,
        IDepartmentRepository departmentRepository)
        : IRequestHandler<AddEmployeeToDepartmentCommand, Employee>
    {
        public async Task<Employee> Handle(AddEmployeeToDepartmentCommand request, CancellationToken cancellationToken)
        {
            // Validate that the employee exists
            var employee = await employeeRepository.GetEmployeeByIdAsync(request.Assignment.EmployeeId);
            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee with ID {request.Assignment.EmployeeId} not found.");
            }

            // Validate that the department exists
            var department = await departmentRepository.GetDepartmentByIdAsync(request.Assignment.DepartmentId);
            if (department == null)
            {
                throw new KeyNotFoundException($"Department with ID {request.Assignment.DepartmentId} not found.");
            }

            // Update the employee's DepartmentId
            employee.DepartmentId = request.Assignment.DepartmentId;

            // Update the employee in the database
            var updatedEmployee = await employeeRepository.UpdateEmployeeAsync(employee.Id, employee);

            return updatedEmployee;
        }
    }
}
