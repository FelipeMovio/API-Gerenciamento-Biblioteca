using AutoMapper;
using Biblioteca.Data;
using Biblioteca.Dtos;
using Biblioteca.Exceptions;
using Biblioteca.Models;

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

    public ReadLivroDto CreateLivro(CreateLivroDto livroDto)
    {
        bool categoriaExiste = _context.Categorias
            .Any(c => c.Id == livroDto.CategoriaId);

        if (!categoriaExiste)
            throw new CategoriaNaoEncontradaException(livroDto.CategoriaId);

        Livro livro = _mapper.Map<Livro>(livroDto);

        _context.Livros.Add(livro);
        _context.SaveChanges();

        return _mapper.Map<ReadLivroDto>(livro);
    }

    public List<ReadLivroDto> GetLivros()
    {
        return _mapper.Map<List<ReadLivroDto>>(
            _context.Livros.ToList());
    }

    public ReadLivroDto? GetLivroById(int id)
    {
        Livro? livro = _context.Livros
            .FirstOrDefault(l => l.Id == id);

        if (livro == null)
            return null;

        return _mapper.Map<ReadLivroDto>(livro);
    }

    public bool UpdateLivro(
        int id,
        UpdateLivroDto updateLivroDto)
    {
        Livro? livro = _context.Livros
            .FirstOrDefault(l => l.Id == id);

        if (livro == null)
            return false;

        bool categoriaExiste = _context.Categorias
            .Any(c => c.Id == updateLivroDto.CategoriaId);

        if (!categoriaExiste)
            throw new CategoriaNaoEncontradaException(
                updateLivroDto.CategoriaId);

        _mapper.Map(updateLivroDto, livro);

        _context.SaveChanges();

        return true;
    }

    public bool DeleteLivro(int id)
    {
        Livro? livro = _context.Livros
            .FirstOrDefault(l => l.Id == id);

        if (livro == null)
            return false;

        _context.Livros.Remove(livro);
        _context.SaveChanges();

        return true;
    }
}