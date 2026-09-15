using Biblioteca.Application;

namespace Biblioteca;

public static class DependencyInjection
{
    public static IServiceCollection AddAppDI(this IServiceCollection service)
    {
        service.AddApplicationDI()
            .AddInfrastructureDI();
        return service;
    }
}
