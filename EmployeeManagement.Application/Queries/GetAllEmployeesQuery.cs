using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Queries
{
    public record GetAllEmployeesQuery() : IRequest<IEnumerable<EmployeeWithDepartmentDto>>;

    public class GetAllEmployeesHandler(IEmployeeRepository employeeRepository)
        : IRequestHandler<GetAllEmployeesQuery, IEnumerable<EmployeeWithDepartmentDto>>
    {
        public async Task<IEnumerable<EmployeeWithDepartmentDto>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = await employeeRepository.GetEmployee(); 

            // Map Employee entities to EmployeeWithDepartmentDto
            var result = employees.Select(e => new EmployeeWithDepartmentDto
            {
                Id = e.Id,
                EmpName = e.EmpName,
                Email = e.Email,
                Phone = e.Phone,
                DepartmentId = e.DepartmentId,
                DepartmentName = e.Department != null ? e.Department.DepartmentName : null
            }).ToList();

            return result;
        }
    }
}
