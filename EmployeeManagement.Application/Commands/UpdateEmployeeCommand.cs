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
    public record UpdateEmployeeCommand(int empId, EmployeeForDepartment employee) : IRequest<EmployeeForDepartment>;

    public class UpdateEmployeeHandler(IEmployeeRepository employeeRepository)
        : IRequestHandler<UpdateEmployeeCommand, EmployeeForDepartment>
    {
        public async Task<EmployeeForDepartment> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var entity = new Employee
            {
                EmpName = request.employee.EmpName,
                Email = request.employee.Email,
                Phone = request.employee.Phone
            };

            var updated = await employeeRepository.UpdateEmployeeAsync(request.empId, entity);

            return new EmployeeForDepartment
            {
                Id = updated.Id,
                EmpName = updated.EmpName,
                Email = updated.Email,
                Phone = updated.Phone
            };
        }
    }
}
