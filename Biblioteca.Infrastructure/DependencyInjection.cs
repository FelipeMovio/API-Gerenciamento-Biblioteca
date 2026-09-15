using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureDI(this IServiceCollection service)
    {
        return service;
    }
}
