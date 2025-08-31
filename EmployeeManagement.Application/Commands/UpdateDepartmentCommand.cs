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
    public record UpdateDepartmentCommand(int Id, DepartmentCreateDto Department) : IRequest<DepartmentCreateDto>;

    public class UpdateDepartmentHandler(IDepartmentRepository departmentRepository)
        : IRequestHandler<UpdateDepartmentCommand, DepartmentCreateDto>
    {
        public async Task<DepartmentCreateDto> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var entity = new Department
            {
                Id = request.Id,
                DepartmentName = request.Department.DepartmentName,
              
            };

            var updated = await departmentRepository.UpdateDepartmentAsync(request.Id, entity);

            return new DepartmentCreateDto
            {
                DepartmentName = updated.DepartmentName,
            };
        }

      
    }
}
