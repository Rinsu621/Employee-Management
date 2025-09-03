using EmployeeManagement.Application.DTOs.Project;
using EmployeeManagement.Domain.Interfaces;
using EmployeeManagement.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Commands.ProjectCommand
{
    public record AddProjectCommand(ProjectCreateDto ProjectDto) : IRequest<ProjectCreateDto>;

    public  class AddProjectHandler(IProjectRepository projectRepository) : IRequestHandler<AddProjectCommand, ProjectCreateDto>
    {
        public async Task<ProjectCreateDto> Handle(AddProjectCommand request, CancellationToken cancellationToken)
        {
            var project = new Project
            {
                ProjectName = request.ProjectDto.ProjectName,
                Description = request.ProjectDto.Description,
            };

            var addedProject= await projectRepository.AddProjectAsync(project);
            return new ProjectCreateDto
            {
                Id = addedProject.Id,
                ProjectName = addedProject.ProjectName,
                Description = addedProject.Description,
            };
        }
    }
}
