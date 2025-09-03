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
    public record GetProjectByIdWithEmployeeQuery(int projectId) : IRequest<ProjectDto>;

    public class GetProjectByIdWithEmployeeHandler(IProjectRepository projectRepository)
        : IRequestHandler<GetProjectByIdWithEmployeeQuery, ProjectDto>
    {
        public async Task<ProjectDto> Handle(GetProjectByIdWithEmployeeQuery request, CancellationToken cancellationToken)
        {
            var project= await projectRepository.GetProjectByIdAsync(request.projectId);
            return new ProjectDto
            {
                Id = project.Id,
                ProjectName = project.ProjectName,
                Employees = project.Employees.Select(e => new EmployeeWithDepartmentDto
                {
                    Id = e.Id,
                    EmpName = e.EmpName,
                    Email = e.Email,
                    Phone = e.Phone
                }).ToList()
            };
        }
    }
}
