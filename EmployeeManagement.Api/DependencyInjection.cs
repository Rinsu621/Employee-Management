using EmployeeManagement.Application;
using EmployeeManagement.Infrastructure;

namespace EmployeeManagement.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiDI(this IServiceCollection services)
        {
            services.AddApplicationDI()
                .AddInfrastructureDI();

            return services;
        }
    }
}
