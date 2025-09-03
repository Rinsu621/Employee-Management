using EmployeeManagement.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Commands.ProjectCommand
{
    public record DeleteProjectCommand(int projectId) : IRequest<bool>;

    public class DeleteProjectCommandHandler(IProjectRepository projectRepository) : IRequestHandler<DeleteProjectCommand, bool>
    {
        public async Task<bool> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var existingProject = await projectRepository.GetProjectByIdAsync(request.projectId);
            if (existingProject == null)
            {
                return false;
            }
            await projectRepository.DeleteProjectAsync(request.projectId);
            return true; 
        }
    }
}
