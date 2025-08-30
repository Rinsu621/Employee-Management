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
    public record UpdateEmployeeCommand(int empId, EmployeeDto employee) : IRequest<EmployeeDto>;

    public class UpdateEmployeeHandler(IEmployeeRepository employeeRepository)
        : IRequestHandler<UpdateEmployeeCommand, EmployeeDto>
    {
        public async Task<EmployeeDto> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var entity = new Employee
            {
                Id = request.empId,
                EmpName = request.employee.EmpName,
                Email = request.employee.Email,
                Phone = request.employee.Phone
            };

            var updated = await employeeRepository.UpdateEmployeeAsync(request.empId, entity);

            return new EmployeeDto
            {
                Id = updated.Id,
                EmpName = updated.EmpName,
                Email = updated.Email,
                Phone = updated.Phone
            };
        }
    }
}
