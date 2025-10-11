# Guia Completo da Oficina - Biblioteca Pessoal

## Módulo 1: Setup Inicial (10min)

### 1.1 Criar o Projeto

```bash
# Criar pasta do projeto
mkdir PersonalBiblio
cd PersonalBiblio

# Criar estrutura de pastas
mkdir -p src docs

# Criar projeto MVC
cd src
dotnet new mvc -n PersonalBiblio
cd PersonalBiblio
```

### 1.2 Instalar Dependências

```bash
# Instalar pacotes necessários
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add package Microsoft.EntityFrameworkCore.Design
```

### 1.3 Configurar PostgreSQL

Crie um banco de dados no PostgreSQL:

```sql
CREATE DATABASE biblioteca_pessoal;
```

Edite `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=biblioteca_pessoal;Username=postgres;Password=postgres"
  }
}
```

---

## Módulo 2: Models (15min)

### 2.1 Criar Enum StatusLeitura

Crie `Models/StatusLeitura.cs`:

```csharp
namespace PersonalBiblio.Models;

public enum StatusLeitura
{
    QueroLer = 0,
    Lendo = 1,
    Lido = 2
}
```

### 2.2 Criar Model Categoria

Crie `Models/Categoria.cs`:

```csharp
using System.ComponentModel.DataAnnotations;

namespace PersonalBiblio.Models;

public class Categoria
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome da categoria é obrigatório")]
    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Descricao { get; set; }

    public ICollection<Livro> Livros { get; set; } = new List<Livro>();
}
```

### 2.3 Criar Model Livro

Crie `Models/Livro.cs`:

```csharp
using System.ComponentModel.DataAnnotations;

namespace PersonalBiblio.Models;

public class Livro
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O título é obrigatório")]
    [StringLength(200)]
    public string Titulo { get; set; } = string.Empty;

    [Required(ErrorMessage = "O autor é obrigatório")]
    [StringLength(150)]
    public string Autor { get; set; } = string.Empty;

    [StringLength(13, MinimumLength = 10)]
    public string? ISBN { get; set; }

    [Range(1000, 9999)]
    public int? Ano { get; set; }

    [Required]
    public StatusLeitura Status { get; set; } = StatusLeitura.QueroLer;

    [Required]
    public int CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }

    public DateTime DataCadastro { get; set; } = DateTime.Now;
    public DateTime? DataAtualizacao { get; set; }
}
```

---

## Módulo 3: Database (10min)

### 3.1 Criar DbContext

Crie a pasta `Data` e o arquivo `Data/BibliotecaContext.cs`:

```csharp
using Microsoft.EntityFrameworkCore;
using PersonalBiblio.Models;

namespace PersonalBiblio.Data;

public class BibliotecaContext : DbContext
{
    public BibliotecaContext(DbContextOptions<BibliotecaContext> options) 
        : base(options)
    {
    }

    public DbSet<Livro> Livros { get; set; }
    public DbSet<Categoria> Categorias { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configurações das entidades
        modelBuilder.Entity<Livro>(entity =>
        {
            entity.ToTable("livros");
            entity.HasOne(e => e.Categoria)
                .WithMany(c => c.Livros)
                .HasForeignKey(e => e.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.ToTable("categorias");
        });

        // Seed Data
        modelBuilder.Entity<Categoria>().HasData(
            new Categoria { Id = 1, Nome = "Ficção" },
            new Categoria { Id = 2, Nome = "Não-Ficção" },
            new Categoria { Id = 3, Nome = "Tecnologia" },
            new Categoria { Id = 4, Nome = "Biografias" },
            new Categoria { Id = 5, Nome = "Autoajuda" }
        );
    }
}
```

### 3.2 Configurar no Program.cs

Edite `Program.cs`:

```csharp
using Microsoft.EntityFrameworkCore;
using PersonalBiblio.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Configurar DbContext
builder.Services.AddDbContext<BibliotecaContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// ... resto do código
```

### 3.3 Criar e Executar Migrations

```bash
# Instalar ferramenta EF (se necessário)
dotnet tool install --global dotnet-ef

# Criar migration
dotnet ef migrations add InicialCreate

# Aplicar no banco
dotnet ef database update
```

---

## Módulo 4: Controllers (20min)

### 4.1 HomeController

Crie `Controllers/HomeController.cs`:

```csharp
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
        ViewBag.TotalLivros = await _context.Livros.CountAsync();
        ViewBag.LivrosLidos = await _context.Livros
            .CountAsync(l => l.Status == StatusLeitura.Lido);
        ViewBag.LivrosLendo = await _context.Livros
            .CountAsync(l => l.Status == StatusLeitura.Lendo);
        ViewBag.LivrosQueroLer = await _context.Livros
            .CountAsync(l => l.Status == StatusLeitura.QueroLer);

        var livrosRecentes = await _context.Livros
            .Include(l => l.Categoria)
            .OrderByDescending(l => l.DataCadastro)
            .Take(5)
            .ToListAsync();

        return View(livrosRecentes);
    }
}
```

### 4.2 LivrosController

Crie `Controllers/LivrosController.cs` com os métodos:
- `Index` - Lista com busca e filtros
- `Create` (GET e POST) - Adicionar livro
- `Edit` (GET e POST) - Editar livro
- `Delete` (GET e POST) - Excluir livro
- `Details` - Ver detalhes

**Exemplo do método Index:**

```csharp
public async Task<IActionResult> Index(string? busca, int? categoriaId, StatusLeitura? status)
{
    var query = _context.Livros.Include(l => l.Categoria).AsQueryable();

    if (!string.IsNullOrEmpty(busca))
        query = query.Where(l => l.Titulo.Contains(busca) || l.Autor.Contains(busca));

    if (categoriaId.HasValue)
        query = query.Where(l => l.CategoriaId == categoriaId.Value);

    if (status.HasValue)
        query = query.Where(l => l.Status == status.Value);

    ViewBag.Categorias = new SelectList(
        await _context.Categorias.ToListAsync(), "Id", "Nome");

    return View(await query.ToListAsync());
}
```

### 4.3 CategoriasController

Similar ao LivrosController, com CRUD completo.

---

## Módulo 5: Views (25min)

### 5.1 Configurar Tailwind CSS v4

Crie `Views/Shared/_Layout.cshtml`:

```html
<!DOCTYPE html>
<html lang="pt-BR">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>@ViewData["Title"] - Biblioteca Pessoal</title>
    
    <!-- Tailwind CSS v4 via CDN -->
    <script src="https://cdn.tailwindcss.com?plugins=forms"></script>
</head>
<body class="bg-gray-50">
    <nav class="bg-white shadow-lg">
        <div class="max-w-7xl mx-auto px-4">
            <div class="flex justify-between h-16">
                <div class="flex space-x-8">
                    <a asp-controller="Home" asp-action="Index" 
                       class="text-2xl font-bold text-blue-600">
                        📚 Biblioteca Pessoal
                    </a>
                    <a asp-controller="Livros" asp-action="Index">Livros</a>
                    <a asp-controller="Categorias" asp-action="Index">Categorias</a>
                </div>
            </div>
        </div>
    </nav>

    <main class="max-w-7xl mx-auto px-4 py-8">
        @RenderBody()
    </main>
</body>
</html>
```

### 5.2 Criar Views

Crie as seguintes views:

**Estrutura de Pastas:**
```
Views/
├── _ViewStart.cshtml
├── _ViewImports.cshtml
├── Shared/
│   ├── _Layout.cshtml
│   └── _ValidationScriptsPartial.cshtml
├── Home/
│   └── Index.cshtml (Dashboard)
├── Livros/
│   ├── Index.cshtml (Lista com filtros)
│   ├── Create.cshtml
│   ├── Edit.cshtml
│   ├── Delete.cshtml
│   └── Details.cshtml
└── Categorias/
    ├── Index.cshtml
    ├── Create.cshtml
    ├── Edit.cshtml
    └── Delete.cshtml
```

**_ViewStart.cshtml:**
```cshtml
@{
    Layout = "_Layout";
}
```

**_ViewImports.cshtml:**
```cshtml
@using PersonalBiblio
@using PersonalBiblio.Models
@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
```

---

## Módulo 6: Testes (10min)

### 6.1 Executar a Aplicação

```bash
dotnet run
```

Acesse: `https://localhost:5001`

### 6.2 Testar Funcionalidades

**Checklist de Testes:**

1. ✅ Dashboard exibe estatísticas corretas
2. ✅ Criar uma nova categoria
3. ✅ Adicionar livros com diferentes status
4. ✅ Buscar livros por título/autor
5. ✅ Filtrar por categoria
6. ✅ Filtrar por status
7. ✅ Editar um livro
8. ✅ Visualizar detalhes
9. ✅ Excluir um livro
10. ✅ Tentar excluir categoria com livros (deve falhar)

### 6.3 Troubleshooting Comum

**Erro: "A network-related or instance-specific error"**
- Verifique se o PostgreSQL está rodando
- Verifique a connection string

**Erro: "No migrations configuration type was found"**
- Execute: `dotnet ef migrations add Initial`

**Views não encontradas:**
- Verifique se criou _ViewStart.cshtml
- Verifique o nome das pastas (case-sensitive no Linux)

---

## 🎯 Parabéns!

Você completou a oficina e criou um sistema completo com:
- ✅ CRUD completo
- ✅ Relacionamentos entre entidades
- ✅ Busca e filtros
- ✅ Dashboard com estatísticas
- ✅ Interface moderna com Tailwind CSS
- ✅ Validações

## 🚀 Próximos Passos

1. Adicionar autenticação (ASP.NET Identity)
2. Criar API REST
3. Adicionar testes unitários
4. Deploy no Azure/AWS
5. Adicionar upload de capas de livros
6. Implementar avaliações e resenhas

---

**Fim da Oficina!** 🎉
