using EmployeeManagement.Application.DTOs;
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
    public record AddDepartmentCommand(DepartmentCreateDto DepartmentDto) : IRequest<Department>;

    public class AddDepartmentCommandHandler(IDepartmentRepository departmentRepository) : IRequestHandler<AddDepartmentCommand, Department>
    {
        public async Task<Department> Handle(AddDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = new Department
            {
                DepartmentName = request.DepartmentDto.DepartmentName
            };

            return await departmentRepository.AddDepartmentAsync(department);
        }
    }

}
