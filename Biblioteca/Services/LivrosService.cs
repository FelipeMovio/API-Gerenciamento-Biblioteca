using AutoMapper;
using Biblioteca.Data;
using Biblioteca.Dtos;
using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using static System.Net.WebRequestMethods;

namespace Biblioteca.Services;

public class LivrosService 
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public LivrosService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public ReadLivroDto CriarLivro
        (CreateLivroDto livroDto)
    {
        Livro livro = _mapper.Map<Livro>(livroDto);
        
        _context.Livros.Add(livro);
        _context.SaveChanges();

        return _mapper.Map<ReadLivroDto>(livro);

    }
}
