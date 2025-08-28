using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Application.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using EmployeeManagement.Application.Queries;

namespace EmployeeManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController(ISender sender) : ControllerBase
    {
        [HttpPost("")]
        //From body means Json Body
        public async Task<IActionResult> AddEmployeeAsync([FromBody] Employee employee)
        {
            
            
            var result = await sender.Send(new AddEmployeeCommand(employee));

            return Ok(result);
        }

        [HttpGet("employees")]
        public async Task<IActionResult> GetAllEmployeesAsync()
        {
            var result= await sender.Send(new GetAllEmployeesQuery());
            if (result == null)
                return NotFound();
            return Ok(result);

        }

        [HttpGet("employee/{id}")]
        public async Task<IActionResult>GetEmployeeByIdAsync([FromBody] int empId)
        {
            var result = await sender.Send(new GetEmployeesByIdQuery(empId));
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
