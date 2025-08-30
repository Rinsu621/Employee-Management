using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Commands
{
  public record UpdateEmployeeCommand(int empId, Employee employee):IRequest<Employee>;
    //Handler take input and expected output
    public class UpdateEmployeeHandler(IEmployeeRepository employeeRepository):IRequestHandler<UpdateEmployeeCommand,Employee>
    {
        public async Task<Employee> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
           return await employeeRepository.UpdateEmployeeAsync(request.empId,request.employee);
        }

    }
}
