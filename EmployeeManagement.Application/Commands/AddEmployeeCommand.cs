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
    //This is command that represents the intent to add a new employee
    //Implements IRequest,Employee> because it will return an employee after execution.
    public record AddEmployeeCommand(Employee employee):IRequest<Employee>;


    //Logic to handle the AddEmployeeCommand
    public class AddEmployeeCommandHandler(IEmployeeRepository employeeRepository) : IRequestHandler<AddEmployeeCommand, Employee>
    {
        public async Task<Employee> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
        {

            //Call the repository to add the employee to the database
            //the repository handles the actual EF core to save change 
            return await employeeRepository.AddEmployeeByAsync(request.employee);
        }
    }
}

