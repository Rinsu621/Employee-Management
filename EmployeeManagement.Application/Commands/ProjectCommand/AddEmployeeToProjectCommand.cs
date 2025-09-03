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

namespace EmployeeManagement.Application.Commands.ProjectCommand
{
    public record AddEmployeeToProjectCommand(int projectId, int employeeId) : IRequest<ProjectDto>;
    
    public class AddEmployeeToProjectHandler(IProjectRepository projectRepository, IEmployeeRepository employeeRepository)
        : IRequestHandler<AddEmployeeToProjectCommand, ProjectDto>
    {
        public async Task<ProjectDto> Handle(AddEmployeeToProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await projectRepository.AssignEmployeeToProjectAsync(request.projectId, request.employeeId);
            return new ProjectDto
            {
                Id = project.Id,
                ProjectName = project.ProjectName,
                Description = project.Description,
                Employees = project.Employees.Select(e => new EmployeeWithDepartmentDto
                {
                    Id = e.Id,
                    EmpName = e.EmpName,
                    Email = e.Email,
                    Phone = e.Phone,
                    DepartmentId = e.DepartmentId,
                    DepartmentName = e.Department?.DepartmentName
                }).ToList()
            };
        }
    }
}
