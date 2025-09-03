using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Interfaces;
using EmployeeManagement.Infrastructure.Data;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Commands
{
    //This is command that represents the intent to add a new employee
    //Implements IRequest,Employee> because it will return an employee after execution.
    public record AddEmployeeCommand(EmployeeDto employee) : IRequest<EmployeeDto>;

    public class AddEmployeeCommandHandler : IRequestHandler<AddEmployeeCommand, EmployeeDto>
    {
        private readonly AppDbContext dbContext;
        private readonly IValidator<EmployeeDto> validator;

        public AddEmployeeCommandHandler(AppDbContext _dbContext, IValidator<EmployeeDto> _validator)
        {
            dbContext = _dbContext;
            validator= _validator;
        }

       
        public async Task<EmployeeDto> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
        {
            var validationresult = await validator.ValidateAsync(request.employee, cancellationToken);
            if(!validationresult.IsValid)
            {
                throw new ValidationException(validationresult.Errors);
            }

            var existingEmployee = await dbContext.Employees.FirstOrDefaultAsync(e => e.Email == request.employee.Email, cancellationToken);

            if (existingEmployee != null)
            {
                throw new InvalidOperationException($"An employee with email '{request.employee.Email}' already exists.");
            }
            var defaultPassword = "DefaultPassword"; 
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(defaultPassword);

            var entity = new Employee
            {
                EmpName = request.employee.EmpName,
                Email = request.employee.Email,
                Phone = request.employee.Phone,
                Role = request.employee.Role,           
                PasswordHash = hashedPassword     
            };

            dbContext.Employees.Add(entity);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new EmployeeDto
            {
                Id = entity.Id,
                EmpName = entity.EmpName,
                Email = entity.Email,
                Phone = entity.Phone,
                Role = entity.Role  ,
                Password= defaultPassword
            };
        }
    }
}

