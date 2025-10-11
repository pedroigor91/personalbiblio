using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PersonalBiblio.Data;
using PersonalBiblio.Models;

namespace PersonalBiblio.Controllers;

public class LivrosController : Controller
{
    private readonly BibliotecaContext _context;

    public LivrosController(BibliotecaContext context)
    {
        _context = context;
    }

    // GET: Livros
    public async Task<IActionResult> Index(string? busca, int? categoriaId, StatusLeitura? status)
    {
        var query = _context.Livros.Include(l => l.Categoria).AsQueryable();

        // Filtro por busca (título ou autor)
        if (!string.IsNullOrEmpty(busca))
        {
            query = query.Where(l => 
                l.Titulo.Contains(busca) || 
                l.Autor.Contains(busca));
            ViewBag.Busca = busca;
        }

        // Filtro por categoria
        if (categoriaId.HasValue)
        {
            query = query.Where(l => l.CategoriaId == categoriaId.Value);
            ViewBag.CategoriaId = categoriaId.Value;
        }

        // Filtro por status
        if (status.HasValue)
        {
            query = query.Where(l => l.Status == status.Value);
            ViewBag.Status = status.Value;
        }

        // Populate dropdown lists
        ViewBag.Categorias = new SelectList(await _context.Categorias.ToListAsync(), "Id", "Nome");

        var livros = await query.OrderByDescending(l => l.DataCadastro).ToListAsync();
        return View(livros);
    }

    // GET: Livros/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var livro = await _context.Livros
            .Include(l => l.Categoria)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (livro == null)
        {
            return NotFound();
        }

        return View(livro);
    }

    // GET: Livros/Create
    public async Task<IActionResult> Create()
    {
        ViewData["CategoriaId"] = new SelectList(await _context.Categorias.ToListAsync(), "Id", "Nome");
        return View();
    }

    // POST: Livros/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Titulo,Autor,ISBN,Ano,Status,CategoriaId")] Livro livro)
    {
        if (ModelState.IsValid)
        {
            livro.DataCadastro = DateTime.UtcNow;
            _context.Add(livro);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Livro adicionado com sucesso!";
            return RedirectToAction(nameof(Index));
        }
        ViewData["CategoriaId"] = new SelectList(await _context.Categorias.ToListAsync(), "Id", "Nome", livro.CategoriaId);
        return View(livro);
    }

    // GET: Livros/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var livro = await _context.Livros.FindAsync(id);
        if (livro == null)
        {
            return NotFound();
        }
        ViewData["CategoriaId"] = new SelectList(await _context.Categorias.ToListAsync(), "Id", "Nome", livro.CategoriaId);
        return View(livro);
    }

    // POST: Livros/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Autor,ISBN,Ano,Status,CategoriaId")] Livro livro)
    {
        if (id != livro.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var livroExistente = await _context.Livros.AsNoTracking().FirstOrDefaultAsync(l => l.Id == id);
                if (livroExistente == null)
                {
                    return NotFound();
                }

                livro.DataCadastro = livroExistente.DataCadastro;
                livro.DataAtualizacao = DateTime.UtcNow;
                _context.Update(livro);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Livro atualizado com sucesso!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LivroExists(livro.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["CategoriaId"] = new SelectList(await _context.Categorias.ToListAsync(), "Id", "Nome", livro.CategoriaId);
        return View(livro);
    }

    // GET: Livros/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var livro = await _context.Livros
            .Include(l => l.Categoria)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (livro == null)
        {
            return NotFound();
        }

        return View(livro);
    }

    // POST: Livros/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var livro = await _context.Livros.FindAsync(id);
        if (livro != null)
        {
            _context.Livros.Remove(livro);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Livro excluído com sucesso!";
        }

        return RedirectToAction(nameof(Index));
    }

    // POST: Livros/UpdateStatus/5
    [HttpPost]
    public async Task<IActionResult> UpdateStatus(int id, StatusLeitura novoStatus)
    {
        var livro = await _context.Livros.FindAsync(id);
        if (livro == null)
        {
            return NotFound();
        }

        livro.Status = novoStatus;
        livro.DataAtualizacao = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return Ok(new { success = true, message = "Status atualizado com sucesso!" });
    }

    private bool LivroExists(int id)
    {
        return _context.Livros.Any(e => e.Id == id);
    }
}
