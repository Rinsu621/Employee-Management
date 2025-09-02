using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Queries
{
    public record GetEmployeesByIdQuery(int empId) : IRequest<EmployeeWithDepartmentDto>;

    public class GetEmployeesByIdHandler(IEmployeeRepository employeeRepository)
        : IRequestHandler<GetEmployeesByIdQuery, EmployeeWithDepartmentDto>
    {
        public async Task<EmployeeWithDepartmentDto> Handle(GetEmployeesByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await employeeRepository.GetEmployeeByIdAsync(request.empId);

            if (employee == null)
            {
                return null; 
            }

            // Map to DTO
      
           var dto = new EmployeeWithDepartmentDto
           {
               Id = employee.Id,
               EmpName = employee.EmpName,
               Email = employee.Email,
               Phone = employee.Phone,
               DepartmentId = employee.DepartmentId,  // Use the foreign key property
               DepartmentName = employee.Department != null ? employee.Department.DepartmentName : null
           };

            return dto;
        }
    }
}
