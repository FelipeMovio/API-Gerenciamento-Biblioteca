using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Biblioteca.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationDI(
        this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(
                typeof(DependencyInjection).Assembly));

        services.AddValidatorsFromAssembly(
            typeof(DependencyInjection).Assembly);

        return services;
    }
}