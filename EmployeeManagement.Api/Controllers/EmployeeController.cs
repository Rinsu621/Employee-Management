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
    public class EmployeeController(ISender sender) : ControllerBase
    {
        [HttpPost("")]
        public async Task<IActionResult> AddEmployeeAsync([FromBody] EmployeeDto employee)
        {
            var result = await sender.Send(new AddEmployeeCommand(employee));
            return Ok(result);
        }
        [HttpGet("employees")]
        public async Task<IActionResult> GetAllEmployeesAsync()
        {
            var employees = await sender.Send(new GetAllEmployeesQuery());
            if (employees == null || !employees.Any())
                return NotFound();
            var result = employees.Select(e => new
            {
                e.Id,
                e.EmpName,
                e.Email,
                e.Phone,
                e.DepartmentId,
                DepartmentName = e.Department?.DepartmentName
            }).ToList();
            return Ok(result);
        }

        [HttpGet("employees/{empId}")]
        public async Task<IActionResult> GetEmployeeByIdAsync([FromRoute] int empId)
        {
            var employee = await sender.Send(new GetEmployeesByIdQuery(empId));
            if (employee == null)
                return NotFound();
            var result = new
            {
                employee.Id,
                employee.EmpName,
                employee.Email,
                employee.Phone,
                employee.DepartmentId,
                DepartmentName = employee.Department?.DepartmentName
            };
            return Ok(result);
        }

        


        [HttpPut("employees/{empId}")]
        public async Task<IActionResult> UpdateEmployeeAsync([FromRoute] int empId, [FromBody] EmployeeDto employee)
        {
            var result = await sender.Send(new UpdateEmployeeCommand(empId, employee));
            if (result == null)
                return NotFound();
            return Ok(new
            {
                result.Id,
                result.EmpName,
                result.Email,
                result.Phone,
            });
        }

        [HttpDelete("employees/{empId}")]
        public async Task<IActionResult> DeleteEmployeeAsync([FromRoute] int empId)
        {
            var result = await sender.Send(new DeleteEmployeeCommand(empId));
            if (!result)
                return NotFound();
            return Ok(result);
        }
    }
}
