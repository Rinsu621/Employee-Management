using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Commands
{
    public record ChangePasswordCommand(ChangePasswordDto PasswordDto, ClaimsPrincipal User): IRequest<bool>;


    public class ChangePasswordCommandHandler(IEmployeeRepository employeeRepository):IRequestHandler<ChangePasswordCommand, bool>
    {
        public async Task<bool> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var claim = request.User.FindFirst("id"); 
            if (claim == null)
                return false; 

            var userId = int.Parse(claim.Value);
            var employee = await employeeRepository.GetEmployeeByIdAsync(userId);
            if(employee ==null || !BCrypt.Net.BCrypt.Verify(request.PasswordDto.currentPassword, employee.PasswordHash))
            {
                return false;
            }

            employee.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.PasswordDto.newPassword);
            await employeeRepository.UpdateEmployeeAsync(employee.Id, employee);
            return true;
        }

    }
}
