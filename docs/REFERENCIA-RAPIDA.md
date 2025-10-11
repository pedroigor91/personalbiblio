# Referência Rápida - Comandos e Conceitos

## Comandos .NET CLI Essenciais

### Projeto e Pacotes
```bash
# Criar novo projeto MVC
dotnet new mvc -n NomeProjeto

# Adicionar pacote NuGet
dotnet add package NomePacote

# Restaurar pacotes
dotnet restore

# Compilar projeto
dotnet build

# Executar aplicação
dotnet run

# Limpar build
dotnet clean
```

### Entity Framework Core
```bash
# Instalar ferramenta EF
dotnet tool install --global dotnet-ef

# Criar migration
dotnet ef migrations add NomeDaMigration

# Aplicar migrations no banco
dotnet ef database update

# Reverter última migration
dotnet ef migrations remove

# Ver SQL que seria executado
dotnet ef migrations script

# Listar migrations
dotnet ef migrations list

# Criar banco a partir do modelo
dotnet ef database update

# Dropar banco (cuidado!)
dotnet ef database drop
```

### Scaffolding (Geração de código)
```bash
# Instalar ferramenta de scaffolding
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design

# Gerar Controller com Views
dotnet aspnet-codegenerator controller -name LivrosController \
  -m Livro -dc BibliotecaContext --relativeFolderPath Controllers \
  --useDefaultLayout --referenceScriptLibraries
```

---

## Connection Strings

### PostgreSQL
```json
"Host=localhost;Port=5432;Database=nome_db;Username=usuario;Password=senha"
```

### SQL Server
```json
"Server=localhost;Database=nome_db;User Id=usuario;Password=senha;"
```

### SQLite
```json
"Data Source=app.db"
```

### MySQL
```json
"Server=localhost;Database=nome_db;User=usuario;Password=senha;"
```

---

## Data Annotations Mais Usadas

### Validações
```csharp
[Required]                          // Campo obrigatório
[Required(ErrorMessage = "msg")]    // Com mensagem customizada
[StringLength(100)]                 // Tamanho máximo
[StringLength(100, MinimumLength = 3)] // Tamanho mín e máx
[Range(1, 100)]                     // Valor entre 1 e 100
[EmailAddress]                      // Validar email
[Phone]                             // Validar telefone
[Url]                               // Validar URL
[RegularExpression("regex")]        // Expressão regular
[Compare("OutroField")]             // Comparar com outro campo
[CreditCard]                        // Validar cartão de crédito
```

### Configuração de Banco
```csharp
[Key]                               // Chave primária
[Required]                          // NOT NULL
[MaxLength(100)]                    // VARCHAR(100)
[Column("nome_coluna")]             // Nome da coluna
[Table("nome_tabela")]              // Nome da tabela
[DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-increment
[ForeignKey("CategoriaId")]         // Chave estrangeira
[Index]                             // Criar índice
[NotMapped]                         // Não mapear para o banco
```

### Display
```csharp
[Display(Name = "Nome para Exibir")]
[DisplayFormat(DataFormatString = "{0:C}")]  // Formato moeda
[DataType(DataType.Date)]                    // Tipo de dado
[DataType(DataType.EmailAddress)]
[DataType(DataType.Password)]
```

---

## LINQ Queries Comuns

### Seleção
```csharp
// Buscar todos
var todos = await _context.Livros.ToListAsync();

// Buscar com condição
var filtrados = await _context.Livros
    .Where(l => l.Status == StatusLeitura.Lido)
    .ToListAsync();

// Buscar um item
var livro = await _context.Livros.FindAsync(id);
var livro = await _context.Livros.FirstOrDefaultAsync(l => l.Id == id);
var livro = await _context.Livros.SingleOrDefaultAsync(l => l.ISBN == isbn);

// Include (Join)
var livros = await _context.Livros
    .Include(l => l.Categoria)
    .ToListAsync();

// Select (projeção)
var titulos = await _context.Livros
    .Select(l => l.Titulo)
    .ToListAsync();
```

### Ordenação
```csharp
// Crescente
.OrderBy(l => l.Titulo)

// Decrescente
.OrderByDescending(l => l.DataCadastro)

// Múltiplas ordenações
.OrderBy(l => l.Categoria.Nome)
.ThenBy(l => l.Titulo)
```

### Agregação
```csharp
// Contar
var total = await _context.Livros.CountAsync();
var lidos = await _context.Livros.CountAsync(l => l.Status == StatusLeitura.Lido);

// Máximo/Mínimo
var maisNovo = await _context.Livros.MaxAsync(l => l.Ano);
var maisAntigo = await _context.Livros.MinAsync(l => l.Ano);

// Média/Soma
var mediaAno = await _context.Livros.AverageAsync(l => l.Ano);
var soma = await _context.Livros.SumAsync(l => l.Paginas);

// Existe
var existe = await _context.Livros.AnyAsync(l => l.ISBN == isbn);
```

### Paginação
```csharp
int pageSize = 10;
int pageNumber = 1;

var livros = await _context.Livros
    .Skip((pageNumber - 1) * pageSize)
    .Take(pageSize)
    .ToListAsync();
```

---

## Tailwind CSS - Classes Mais Usadas

### Layout
```html
<!-- Container -->
<div class="container mx-auto px-4">

<!-- Grid -->
<div class="grid grid-cols-1 md:grid-cols-3 gap-4">

<!-- Flex -->
<div class="flex justify-between items-center">

<!-- Espaçamento -->
<div class="p-4">         <!-- padding: 1rem -->
<div class="m-4">         <!-- margin: 1rem -->
<div class="space-y-4">   <!-- espaço entre filhos -->
```

### Cores
```html
<!-- Backgrounds -->
<div class="bg-blue-500">
<div class="bg-gray-100">

<!-- Text -->
<p class="text-blue-600">
<p class="text-gray-900">
```

### Tipografia
```html
<h1 class="text-3xl font-bold">
<p class="text-sm text-gray-600">
<a class="text-blue-600 hover:text-blue-800">
```

### Botões
```html
<button class="px-4 py-2 bg-blue-600 text-white rounded-md hover:bg-blue-700">
    Botão
</button>
```

### Formulários
```html
<input type="text" 
       class="w-full rounded-md border-gray-300 shadow-sm 
              focus:border-blue-500 focus:ring focus:ring-blue-200" />
```

---

## Tag Helpers ASP.NET Core

### Links e URLs
```html
<!-- Link para action -->
<a asp-controller="Livros" asp-action="Index">Livros</a>
<a asp-action="Edit" asp-route-id="@livro.Id">Editar</a>

<!-- Imagem -->
<img asp-append-version="true" src="~/images/logo.png" />

<!-- Script/CSS -->
<script src="~/js/site.js" asp-append-version="true"></script>
```

### Formulários
```html
<!-- Form -->
<form asp-controller="Livros" asp-action="Create" method="post">

<!-- Input -->
<input asp-for="Titulo" class="form-control" />
<span asp-validation-for="Titulo" class="text-danger"></span>

<!-- Select -->
<select asp-for="CategoriaId" asp-items="ViewBag.Categorias"></select>

<!-- Textarea -->
<textarea asp-for="Descricao" rows="3"></textarea>

<!-- Checkbox -->
<input asp-for="Aceito" type="checkbox" />

<!-- Hidden -->
<input type="hidden" asp-for="Id" />

<!-- Validation Summary -->
<div asp-validation-summary="All" class="text-danger"></div>
```

---

## Padrões de Projeto Úteis

### Repository Pattern (Simplificado)
```csharp
public interface ILivroRepository
{
    Task<List<Livro>> GetAllAsync();
    Task<Livro?> GetByIdAsync(int id);
    Task AddAsync(Livro livro);
    Task UpdateAsync(Livro livro);
    Task DeleteAsync(int id);
}
```

### ViewModels
```csharp
public class LivroListViewModel
{
    public List<Livro> Livros { get; set; }
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
    public string? SearchTerm { get; set; }
}
```

### DTOs (Data Transfer Objects)
```csharp
public class LivroCreateDto
{
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public int CategoriaId { get; set; }
}
```

---

## Dicas de Performance

### Entity Framework
```csharp
// ✅ BOM: Usar AsNoTracking para leitura
var livros = await _context.Livros.AsNoTracking().ToListAsync();

// ✅ BOM: Filtrar no banco, não na memória
var lidos = await _context.Livros.Where(l => l.Status == StatusLeitura.Lido).ToListAsync();

// ❌ RUIM: Trazer tudo para memória
var lidos = (await _context.Livros.ToListAsync()).Where(l => l.Status == StatusLeitura.Lido);

// ✅ BOM: Projetar apenas campos necessários
var titulos = await _context.Livros.Select(l => new { l.Id, l.Titulo }).ToListAsync();
```

### Views
```csharp
// ✅ Use ViewBag/ViewData para dados simples
ViewBag.Total = total;

// ✅ Use ViewModels para dados complexos
return View(new LivroListViewModel { Livros = livros, Total = total });
```

---

## Segurança Básica

### Validação
```csharp
// Sempre validar no servidor
if (!ModelState.IsValid)
{
    return View(model);
}
```

### Anti-Forgery Token
```html
<!-- Sempre incluir em forms POST -->
<form asp-action="Create" method="post">
    @Html.AntiForgeryToken()
    <!-- ... -->
</form>
```

### SQL Injection
```csharp
// ✅ BOM: Usar LINQ/EF
var livros = await _context.Livros.Where(l => l.Titulo == titulo).ToListAsync();

// ❌ NUNCA: Concatenar SQL
var sql = $"SELECT * FROM Livros WHERE Titulo = '{titulo}'"; // VULNERÁVEL!
```

---

## Debugging

### Logging
```csharp
private readonly ILogger<LivrosController> _logger;

_logger.LogInformation("Buscando livros");
_logger.LogWarning("Livro não encontrado: {Id}", id);
_logger.LogError(ex, "Erro ao salvar livro");
```

### Breakpoints no VS Code
- F9: Adicionar/remover breakpoint
- F5: Iniciar debug
- F10: Step over
- F11: Step into
- Shift+F11: Step out

---

## Recursos Online

- [ASP.NET Core Docs](https://docs.microsoft.com/aspnet/core)
- [EF Core Docs](https://docs.microsoft.com/ef/core)
- [C# Docs](https://docs.microsoft.com/dotnet/csharp)
- [Tailwind CSS](https://tailwindcss.com/docs)
- [Stack Overflow](https://stackoverflow.com/questions/tagged/asp.net-core)

---

**Mantenha esta referência à mão durante o desenvolvimento!** 📖
