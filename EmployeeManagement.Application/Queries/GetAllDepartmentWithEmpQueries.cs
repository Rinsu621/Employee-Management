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
    public record GetAllDepartmentWithEmpQuery : IRequest<IEnumerable<DepartmentDto>>;


    public class GetAllDepartmentsWithEmployeesHandler(IDepartmentRepository departmentRepository)
    : IRequestHandler<GetAllDepartmentWithEmpQuery, IEnumerable<DepartmentDto>>
    {
        public async Task<IEnumerable<DepartmentDto>> Handle(GetAllDepartmentWithEmpQuery request, CancellationToken cancellationToken)
        {
            var departments = await departmentRepository.GetAllDepartmentsWithEmpAsync();

            return departments.Select(d => new DepartmentDto
            {
                Id = d.Id,
                departmentName = d.DepartmentName,
                Employees = d.Employees?.Select(e => new EmployeeForDepartment
                {
                    Id = e.Id,
                    EmpName = e.EmpName,
                    Email = e.Email,
                    Phone = e.Phone
                }).ToList() ?? new List<EmployeeForDepartment>()
            }).ToList();
        }
}
}
