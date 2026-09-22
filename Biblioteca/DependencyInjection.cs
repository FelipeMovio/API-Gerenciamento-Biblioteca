using Biblioteca.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Biblioteca;

public static class DependencyInjection
{
    public static IServiceCollection AddAppDI(
        this IServiceCollection services)
    {
        services.AddApplicationDI();

        return services;
    }
}