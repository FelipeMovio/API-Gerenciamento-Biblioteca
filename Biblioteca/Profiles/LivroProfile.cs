using AutoMapper;
using Biblioteca.Dtos;
using Biblioteca.Models;

namespace Biblioteca.Profiles;

public class LivroProfile : Profile
{
    public LivroProfile()
    {
        CreateMap<CreateLivroDto, Livro>();
        CreateMap<UpdateLivroDto, Livro>();
        CreateMap<Livro, ReadLivroDto>();
    }
}
