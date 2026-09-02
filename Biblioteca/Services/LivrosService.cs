using AutoMapper;
using Biblioteca.Data;
using Biblioteca.Dtos;
using Biblioteca.Models;
using Microsoft.AspNetCore.Http.HttpResults;
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

    public ReadLivroDto CreateLivro
        (CreateLivroDto livroDto)
    {
        Livro livro = _mapper.Map<Livro>(livroDto);
        
        _context.Livros.Add(livro);
        _context.SaveChanges();

        return _mapper.Map<ReadLivroDto>(livro);

    }

    public List<ReadLivroDto> GetLivros()
    {
        return _mapper.Map<List<ReadLivroDto>>
            (_context.Livros.ToList());
    }

    public ReadLivroDto GetLivroById(int id)
    {
        Livro livro = _context.Livros.FirstOrDefault
            (l => l.Id == id);

        return _mapper.Map<ReadLivroDto>(livro);

    }

    public void UpdateLivro(int id,
        UpdateLivroDto updateLivroDto)
    {
        Livro livro = _context.Livros.FirstOrDefault
            (l => l.Id == id);

        _mapper.Map(updateLivroDto, livro);
        _context.SaveChanges();

    }

    public void DeleteLivro(int id)
    {
        Livro livro = _context.Livros.FirstOrDefault
            (l => l.Id == id);

        _context.Remove(livro);
        _context.SaveChanges();
    }
}
