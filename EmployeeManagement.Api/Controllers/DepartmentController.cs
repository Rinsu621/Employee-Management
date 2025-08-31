using EmployeeManagement.Application.Commands;
using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Application.Queries;
using EmployeeManagement.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController(ISender sender) : ControllerBase
    {
        [HttpPost("")]
        public async Task<IActionResult> AddDepartmentAsync([FromBody] DepartmentCreateDto dto)
        {
            var result = await sender.Send(new AddDepartmentCommand(dto));
            return Ok(result);
        }
        [HttpPost("assign-employee")]
        public async Task<IActionResult> AddEmployeeToDepartmentAsync([FromBody] AddEmployeeToDepartmentDto assignment)
        {
            try
            {
                var result = await sender.Send(new AddEmployeeToDepartmentCommand(assignment));
                return Ok(new
                {
                    result.Id,
                    result.EmpName,
                    result.Email,
                    result.Phone,
                    result.DepartmentId,
                    DepartmentName = result.Department?.DepartmentName
                });
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

        [HttpGet("")]
        public async Task<IActionResult> GetAllDepartmentsAsync()
        {
            var result = await sender.Send(new GetAllDepartmentsQuery());
            return Ok(result);
        }

        [HttpGet("with-employees")]
        public async Task<IActionResult> GetAllDepartmentsWithEmployeesAsync()
        {
            var result = await sender.Send(new GetAllDepartmentWithEmpQuery());
            return Ok(result);
        }

        [HttpPut("update-department/{deptId}")]
        public async Task<IActionResult> UpdateDepartmentAsync(int deptId, [FromBody] DepartmentCreateDto dto)
        {
            try
            {
                var result = await sender.Send(new UpdateDepartmentCommand(deptId, dto));
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

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteDepartmentAsync(int id)
        {
            try
            {
                var result = await sender.Send(new DeleteDepartmentCommand(id));
                return Ok(new { Success = result });
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
