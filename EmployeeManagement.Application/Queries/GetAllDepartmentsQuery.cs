using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Queries
{

    public record GetAllDepartmentsQuery():IRequest<IEnumerable<DepartmentCreateDto>>;

    public  class GetAllDepartmentsQueryHandler(IDepartmentRepository departmentRepository) : IRequestHandler<GetAllDepartmentsQuery, IEnumerable<DepartmentCreateDto>>
    {
        public async Task<IEnumerable<DepartmentCreateDto>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
        {
            var departments = await departmentRepository.GetDepartment();
            return departments.Select(d => new DepartmentCreateDto
            {
                Id = d.Id,
                DepartmentName = d.DepartmentName
            }).ToList();
        }
    }


}
