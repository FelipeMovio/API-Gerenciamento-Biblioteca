using Biblioteca.Application.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Biblioteca.Infrastructure.Security;

public class JwtService(IConfiguration configuration) : IJwtService
{
    public string GerarToken(int usuarioId,string nome,string email)
    {
        string? jwtKey = configuration["Jwt:Key"];
        string? jwtIssuer = configuration["Jwt:Issuer"];
        string? jwtAudience = configuration["Jwt:Audience"];

        Claim[] claims = new[]
        {
            new Claim(
                JwtRegisteredClaimNames.Sub,
                usuarioId.ToString()),

            new Claim(
                JwtRegisteredClaimNames.Name,
                nome),

            new Claim(
                JwtRegisteredClaimNames.Email,
                email)
        };

        var chave = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtKey!));

        var credenciais = new SigningCredentials(
            chave,
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtIssuer,
            audience: jwtAudience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: credenciais);

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }
}