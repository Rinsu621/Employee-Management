using EmployeeManagement.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Commands
{
   public record DeleteDepartmentCommand(int Id):IRequest<bool>;
    public class DeleteDepartmentHandler(IDepartmentRepository departmentRepository)
        : IRequestHandler<DeleteDepartmentCommand, bool>
    {
        public async Task<bool> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            return await departmentRepository.DeleteDepartmentAsync(request.Id);
        }
    }
}
