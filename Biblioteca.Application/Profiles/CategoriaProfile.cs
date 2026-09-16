using AutoMapper;
using Biblioteca.Application.Dtos;
using Biblioteca.Domain.Models.CategoriaMod;

namespace Biblioteca.Application.Profiles;

public class CategoriaProfile : Profile
{
    public CategoriaProfile()
    {
        CreateMap<Categoria, CategoriaLivroDto>();
    }
}
