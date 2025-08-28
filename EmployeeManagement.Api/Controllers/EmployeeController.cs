using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Application.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;

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
    }
}
