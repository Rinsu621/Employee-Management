using EmployeeManagement.Application.Commands;
using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Application.Queries;
using EmployeeManagement.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController(ISender sender) : ControllerBase
    {
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] EmployeeLoginDto loginDto)
        {
            var result = await sender.Send(new EmployeeLoginCommand(loginDto));
            if (result == null)
                return Unauthorized(new { message = "Invalid email or password" });

            return Ok(result);
        }

        [HttpPost("")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddEmployeeAsync([FromBody] EmployeeDto employee)
        {
            var result = await sender.Send(new AddEmployeeCommand(employee));
            return Ok(result);
        }
        [HttpGet("employees")]
        [Authorize]
        public async Task<IActionResult> GetAllEmployeesAsync()
        {
            var employees = await sender.Send(new GetAllEmployeesQuery());
            if (employees == null || !employees.Any())
                return NotFound();

            // No mapping needed here, handler already returns DTOs
            return Ok(employees);
        }

        [HttpGet("employees/{empId}")]
        [Authorize]
        public async Task<IActionResult> GetEmployeeByIdAsync([FromRoute] int empId)
        {
            var employee = await sender.Send(new GetEmployeesByIdQuery(empId));
            if (employee == null)
                return NotFound();

            // No mapping needed, handler already returns DTO
            return Ok(employee);
        }

        [HttpPut("employees/{empId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateEmployeeAsync([FromRoute] int empId, [FromBody] EmployeeForDepartment employee)
        {
            var result = await sender.Send(new UpdateEmployeeCommand(empId, employee));
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("employees/{empId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEmployeeAsync([FromRoute] int empId)
        {
            var result = await sender.Send(new DeleteEmployeeCommand(empId));
            if (!result)
                return NotFound();
            return Ok(result);
        }

        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto dto)
        {
            var result= await sender.Send(new ChangePasswordCommand(dto, User));
            if (!result)
            {
                return BadRequest(new { message = "Password change failed. Please check your current password and try again." });
            }
            return Ok(new { message = "Password changed successfully." });
        }
    }
}
