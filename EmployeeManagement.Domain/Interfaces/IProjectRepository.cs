using EmployeeManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Domain.Interfaces
{
    public interface IProjectRepository
    {
        Task<Project> AddProjectAsync(Project project);
        Task<IEnumerable<Project>> GetProjectAsync();
        Task<Project> GetProjectByIdAsync(int projectId);
        Task<Project> UpdateProjectAsync(int projectId, Project entity);
        Task<bool> DeleteProjectAsync(int projectId);
        Task<Project> AssignEmployeeToProjectAsync(int projectId, int employeeId);
    }
}
