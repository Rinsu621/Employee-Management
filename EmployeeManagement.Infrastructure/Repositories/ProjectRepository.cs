using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces;
using EmployeeManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Infrastructure.Repositories
{
    public class ProjectRepository(AppDbContext dbContext): IProjectRepository
    {
        public async Task<Project> AddProjectAsync(Project project)
        {
            dbContext.Projects.Add(project);
            await dbContext.SaveChangesAsync();
            return project;
        }

        public async Task<IEnumerable<Project>> GetProjectAsync()
        {
            return await dbContext.Projects.Include(p => p.Employees).ToListAsync();
        }

        public async Task<Project> GetProjectByIdAsync(int projectId)
        {
            return await dbContext.Projects.Include(p=>p.Employees).FirstOrDefaultAsync(p => p.Id == projectId);
        }

        public async Task<Project> UpdateProjectAsync(int projectId, Project entity)
        {
            var project= await dbContext.Projects.FirstOrDefaultAsync(p => p.Id == projectId);
            if(project == null)
            {
                throw new KeyNotFoundException($"Project with ID {projectId} not found.");
            }
            project.ProjectName = entity.ProjectName;
            project.Description = entity.Description;
            await dbContext.SaveChangesAsync();
            return project;

        }

        public async Task<bool> DeleteProjectAsync(int projectId)
        {
            var project = await dbContext.Projects.FirstOrDefaultAsync(p => p.Id == projectId);
            if (project == null)
            {
                throw new KeyNotFoundException($"Project with ID {projectId} not found.");
            }
            dbContext.Projects.Remove(project);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Project> AssignEmployeeToProjectAsync(int projectId, int employeeId)
        {
            var project = await dbContext.Projects.Include(p => p.Employees).FirstOrDefaultAsync(p => p.Id == projectId);
            if (project == null)
            {
                throw new KeyNotFoundException($"Project with ID {projectId} not found.");
            }
            var employee = await dbContext.Employees.FirstOrDefaultAsync(e => e.Id == employeeId);
            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee with ID {employeeId} not found.");
            }
            if (project.Employees.Any(e => e.Id == employeeId))
            {
                throw new InvalidOperationException("Employee is already assigned to this project.");
            }
            project.Employees.Add(employee);
            await dbContext.SaveChangesAsync();
            return project;
        }

    }
}
