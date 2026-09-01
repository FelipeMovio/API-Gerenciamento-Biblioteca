using AutoMapper;
using Biblioteca.Data;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Biblioteca.Services;

public class LivrosService 
{
    private AppDbContext _context;
    private IMapper _mapper;

    public LivrosService(AppDbContext appDbContext, IMapper mapper)
    {
        this._context = appDbContext;
        this._mapper = mapper;

    }
}
