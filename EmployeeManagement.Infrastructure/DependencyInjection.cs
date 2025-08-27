using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services)
        {
            services.AddDbContext<Data.AppDbContext>(options =>
            {
                options.UseSqlServer("Server=.;Database=EmployeeCRUD; Trusted_Connection=True; TrustServerCertificate=true;MultipleActiveResultSets=true");
            });
            return services;
        }
    }
}
