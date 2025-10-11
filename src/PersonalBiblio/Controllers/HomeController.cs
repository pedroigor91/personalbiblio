using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalBiblio.Data;
using PersonalBiblio.Models;

namespace PersonalBiblio.Controllers;

public class HomeController : Controller
{
    private readonly BibliotecaContext _context;

    public HomeController(BibliotecaContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var totalLivros = await _context.Livros.CountAsync();
        var livrosLidos = await _context.Livros.CountAsync(l => l.Status == StatusLeitura.Lido);
        var livrosLendo = await _context.Livros.CountAsync(l => l.Status == StatusLeitura.Lendo);
        var livrosQueroLer = await _context.Livros.CountAsync(l => l.Status == StatusLeitura.QueroLer);
        var totalCategorias = await _context.Categorias.CountAsync();

        ViewBag.TotalLivros = totalLivros;
        ViewBag.LivrosLidos = livrosLidos;
        ViewBag.LivrosLendo = livrosLendo;
        ViewBag.LivrosQueroLer = livrosQueroLer;
        ViewBag.TotalCategorias = totalCategorias;

        // Livros recentes
        var livrosRecentes = await _context.Livros
            .Include(l => l.Categoria)
            .OrderByDescending(l => l.DataCadastro)
            .Take(5)
            .ToListAsync();

        return View(livrosRecentes);
    }
}
