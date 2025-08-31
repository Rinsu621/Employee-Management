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
    //This is command that represents the intent to add a new employee
    //Implements IRequest,Employee> because it will return an employee after execution.
    public record AddEmployeeCommand(EmployeeDto employee) : IRequest<EmployeeDto>;

    public class AddEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        : IRequestHandler<AddEmployeeCommand, EmployeeDto>
    {
        public async Task<EmployeeDto> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
        {
            var existingEmployee = await employeeRepository.GetEmployeeByEmailAsync(request.employee.Email);
            if (existingEmployee != null)
            {
                throw new InvalidOperationException($"An employee with email '{request.employee.Email}' already exists.");
            }

            var entity = new Employee
            {
                EmpName = request.employee.EmpName,
                Email = request.employee.Email,
                Phone = request.employee.Phone
            };

            var added = await employeeRepository.AddEmployeeByAsync(entity);

            return new EmployeeDto
            {
                Id = added.Id,
                EmpName = added.EmpName,
                Email = added.Email,
                Phone = added.Phone
            };
        }
    }
}

