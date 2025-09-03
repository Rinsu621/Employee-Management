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
    public record UpdateProjectCommand(int projectId, ProjectCreateDto Project) : IRequest<ProjectCreateDto>;

    public class UpdateProjectHandler(IProjectRepository projectRepository)
        : IRequestHandler<UpdateProjectCommand, ProjectCreateDto>
    {
        public async Task<ProjectCreateDto> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var entity = new Project
            {
                ProjectName = request.Project.ProjectName,
                Description = request.Project.Description,
               
            };
            var updated = await projectRepository.UpdateProjectAsync(request.projectId, entity);
            return new ProjectCreateDto
            {
                 Id= updated.Id,
                ProjectName = updated.ProjectName,
                Description = updated.Description,


            };
        }

    }
}
