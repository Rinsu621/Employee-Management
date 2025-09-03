using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Application.DTOs.Project;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Queries.ProjectQuery
{
   public record GetAllProjectWithEmployeeQuery():IRequest<IEnumerable<ProjectDto>>;

    public class GetAllProjectWithEmployeeHandler(IProjectRepository projectRepository) : IRequestHandler<GetAllProjectWithEmployeeQuery, IEnumerable<ProjectDto>>
    {
        public async Task<IEnumerable<ProjectDto>> Handle(GetAllProjectWithEmployeeQuery request, CancellationToken cancellationToken)
        {
            var projects = await projectRepository.GetProjectAsync();
            return projects.Select(p => new ProjectDto
            {
                Id = p.Id,
                ProjectName = p.ProjectName,
                Description = p.Description,
                Employees = p.Employees.Select(e => new EmployeeWithDepartmentDto
                {
                    Id = e.Id,
                    EmpName = e.EmpName,
                    Email = e.Email,
                    Phone = e.Phone
                }).ToList() ?? new List<EmployeeWithDepartmentDto>()
            }).ToList();
        }
    }
}
