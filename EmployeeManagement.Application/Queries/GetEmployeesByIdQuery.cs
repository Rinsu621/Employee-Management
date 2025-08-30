using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Queries
{
    public record GetEmployeesByIdQuery(int empId) : IRequest<Employee>;

    public class GetEmployeesByIdHandler(IEmployeeRepository employeeRepository)
        : IRequestHandler<GetEmployeesByIdQuery, Employee>
    {
        public async Task<Employee> Handle(GetEmployeesByIdQuery request, CancellationToken cancellationToken)
        {
            return await employeeRepository.GetEmployeeByIdAsync(request.empId);
        }
    }
}
