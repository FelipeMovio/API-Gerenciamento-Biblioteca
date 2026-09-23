using Biblioteca.Domain._Core.Base;
using Biblioteca.Domain.Models.CategoriaMod;
using Biblioteca.Infrastructure._Core.Base;
using Biblioteca.Infrastructure.Modulos._Core.Context;
using Biblioteca.Infrastructure.Modulos.CategoriaDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Biblioteca.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureDI(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration
            .GetConnectionString("BibliotecaConnection");

        services.AddDbContext<AppDbContext>(options =>
        {
            options
                .UseLazyLoadingProxies()
                .UseMySql(
                    connectionString,
                    ServerVersion.AutoDetect(connectionString));
        });


        // Unit of Work

        services.AddScoped<IUnitOfWork, UnitOfWork>();


        // Repositories

        services.AddScoped<ICategoriaRepository, CategoriaRepository>();


        return services;
    }
}