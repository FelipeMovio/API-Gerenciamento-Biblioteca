using Biblioteca.Application.Notifications;
using Biblioteca.Application.Security;
using Biblioteca.Domain._Core.Base;
using Biblioteca.Domain.Models.CategoriaMod;
using Biblioteca.Domain.Models.EmprestimoMod;
using Biblioteca.Domain.Models.LivroMod;
using Biblioteca.Domain.Models.UsuarioMod;
using Biblioteca.Infrastructure._Core.Base;
using Biblioteca.Infrastructure.Modulos._Core.Context;
using Biblioteca.Infrastructure.Modulos.CategoriaDb;
using Biblioteca.Infrastructure.Modulos.EmprestimoDb;
using Biblioteca.Infrastructure.Modulos.LivroDb;
using Biblioteca.Infrastructure.Modulos.UsuarioDb;
using Biblioteca.Infrastructure.Notifications;
using Biblioteca.Infrastructure.Security;
using Microsoft.AspNetCore.Identity;
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

        services.AddScoped<IPasswordHasher<Usuario>, PasswordHasher<Usuario>>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IJwtService, JwtService>();


        // Unit of Work

        services.AddScoped<IUnitOfWork, UnitOfWork>();


        // Repositories


        services.AddScoped<ICategoriaRepository, CategoriaRepository>();
        services.AddScoped<ILivroRepository, LivroRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IEmprestimoRepository,EmprestimoRepository>();

        return services;
    }
}