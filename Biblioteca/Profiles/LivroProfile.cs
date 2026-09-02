using AutoMapper;
using Biblioteca.Dtos;
using Biblioteca.Models;
using static System.Net.WebRequestMethods;

namespace Biblioteca.Profiles;

public class LivroProfile : Profile
{
    public LivroProfile()
    {
        CreateMap<CreateLivroDto, Livro>();
        CreateMap<LivroUpdateDto, Livro>();
        CreateMap<Livro, ReadLivroDto>();
    }
}
