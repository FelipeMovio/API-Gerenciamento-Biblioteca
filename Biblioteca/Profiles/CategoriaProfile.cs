using AutoMapper;
using Biblioteca.Dtos;
using Biblioteca.Models;

namespace Biblioteca.Profiles;

public class CategoriaProfile : Profile
{
    public CategoriaProfile()
    {
        CreateMap<Categoria, CategoriaLivroDto>();
    }
}
