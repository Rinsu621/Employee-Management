using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Queries
{
    public record GetAllEmployeesQuery() : IRequest<IEnumerable<Employee>>;

    public class GetAllEmployeesHandler(IEmployeeRepository employeeRepository)
     : IRequestHandler<GetAllEmployeesQuery, IEnumerable<Employee>>
    {
        public async Task<IEnumerable<Employee>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            return await employeeRepository.GetEmployee();
        }
    }
}
