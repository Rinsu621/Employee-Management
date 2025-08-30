using EmployeeManagement.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Commands
{
    public record DeleteEmployeeCommand(int empId) : IRequest<bool>;

    public class DeleteEmployeeHandler(IEmployeeRepository employeeRepository) : IRequestHandler<DeleteEmployeeCommand, bool>
    {
        public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            return await employeeRepository.DeleteEmployeeAsync(request.empId);
        }
    }
}
