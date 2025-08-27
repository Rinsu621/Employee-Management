﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Domain
{
    public static class DependencyInjection
    { 
        public static IServiceCollection AppDomainDI(this IServiceCollection services)
        {
            // Register application services here
            return services;
        }
    }
}
