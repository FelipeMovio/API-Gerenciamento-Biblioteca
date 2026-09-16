using AutoMapper;
using Biblioteca.Application.Dtos;
using Biblioteca.Domain.Models.LivroMod;

namespace Biblioteca.Application.Profiles;

public class LivroProfile : Profile
{
    public LivroProfile()
    {
        CreateMap<CreateLivroDto, Livro>();
        CreateMap<UpdateLivroDto, Livro>();
        CreateMap<Livro, ReadLivroDto>();
    }
}
