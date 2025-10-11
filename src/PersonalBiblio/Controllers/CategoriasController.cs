using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalBiblio.Data;
using PersonalBiblio.Models;

namespace PersonalBiblio.Controllers;

public class CategoriasController : Controller
{
    private readonly BibliotecaContext _context;

    public CategoriasController(BibliotecaContext context)
    {
        _context = context;
    }

    // GET: Categorias
    public async Task<IActionResult> Index()
    {
        var categorias = await _context.Categorias
            .Include(c => c.Livros)
            .ToListAsync();
        return View(categorias);
    }

    // GET: Categorias/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Categorias/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Nome,Descricao")] Categoria categoria)
    {
        if (ModelState.IsValid)
        {
            _context.Add(categoria);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Categoria criada com sucesso!";
            return RedirectToAction(nameof(Index));
        }
        return View(categoria);
    }

    // GET: Categorias/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var categoria = await _context.Categorias.FindAsync(id);
        if (categoria == null)
        {
            return NotFound();
        }
        return View(categoria);
    }

    // POST: Categorias/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Descricao")] Categoria categoria)
    {
        if (id != categoria.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(categoria);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Categoria atualizada com sucesso!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaExists(categoria.Id))
                {
                    return NotFound();
                }

                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(categoria);
    }

    // GET: Categorias/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var categoria = await _context.Categorias
            .Include(c => c.Livros)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (categoria == null)
        {
            return NotFound();
        }

        return View(categoria);
    }

    // POST: Categorias/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var categoria = await _context.Categorias
            .Include(c => c.Livros)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (categoria == null)
        {
            return NotFound();
        }

        if (categoria.Livros.Count != 0)
        {
            TempData["Error"] = "Não é possível excluir esta categoria pois existem livros associados a ela.";
            return RedirectToAction(nameof(Index));
        }

        _context.Categorias.Remove(categoria);
        await _context.SaveChangesAsync();
        TempData["Success"] = "Categoria excluída com sucesso!";
        return RedirectToAction(nameof(Index));
    }

    private bool CategoriaExists(int id)
    {
        return _context.Categorias.Any(e => e.Id == id);
    }
}
