using EmployeeManagement.Application.Commands.ProjectCommand;
using EmployeeManagement.Application.DTOs.Project;
using EmployeeManagement.Application.Queries.ProjectQuery;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController(ISender sender) : ControllerBase
    {
        [HttpPost("Add-Project")]
        public async Task<IActionResult> AddProjectAsync([FromBody] ProjectCreateDto project)
        {
            var result = await sender.Send(new AddProjectCommand(project));
            return Ok(result);
        }

        [HttpGet("Get-All-Projects")]
        public async Task<IActionResult> GetAllProjectsAsync()
        {
            var result = await sender.Send(new GetAllProjectWithEmployeeQuery());
            return Ok(result);
        }

        [HttpGet("Get-Project-By-Id/{projectId}")]
        public async Task<IActionResult> GetProjectByIdAsync([FromRoute] int projectId)
        {
            var result = await sender.Send(new GetProjectByIdWithEmployeeQuery(projectId));
            return Ok(result);
        }
        [HttpPut("Update-Project/{projectId}")]
        public async Task<IActionResult> UpdateProjectAsync([FromRoute] int projectId, [FromBody] ProjectCreateDto project)
        {
           
                var result = await sender.Send(new UpdateProjectCommand(projectId, project));
                return Ok(result);
        }

        [HttpDelete("Delete-Project/{projectId}")]
        public async Task<IActionResult> DeleteProjectAsync([FromRoute] int projectId)
        {
            try
            {
                var result = await sender.Send(new DeleteProjectCommand(projectId));
                return Ok(new { message = "Project deleted successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("Assign-Employee-To-Project/{projectId}/{employeeId}")]

        public async Task<IActionResult> AssignEmployeeToProjectAsync([FromRoute] int projectId,[FromRoute] int employeeId)
        {
            try
            {
                var result = await sender.Send(new AddEmployeeToProjectCommand(projectId, employeeId));
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
