using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Application.Interface;
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
    public record EmployeeLoginCommand(EmployeeLoginDto loginDto) : IRequest<EmployeeLoginResponseDto>;

    public class EmployeeLoginCommandHandler(
    IEmployeeRepository employeeRepository,
    IJwtTokenService jwtTokenService
) : IRequestHandler<EmployeeLoginCommand, EmployeeLoginResponseDto>
    {
        public async Task<EmployeeLoginResponseDto> Handle(EmployeeLoginCommand request, CancellationToken cancellationToken)
        {
            var employee = await employeeRepository.GetEmployeeByEmailAsync(request.loginDto.Email);
            if (employee == null || !BCrypt.Net.BCrypt.Verify(request.loginDto.Password, employee.PasswordHash))
            {
                return null;
            }

            var token = jwtTokenService.GenerateToken(employee);

            return new EmployeeLoginResponseDto
            {
                Id = employee.Id,
                EmpName = employee.EmpName,
                Email = employee.Email,
                Role = employee.Role,
                Token = token
            };
        }
    }

}
