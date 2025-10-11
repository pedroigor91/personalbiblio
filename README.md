# 📚 Biblioteca Pessoal - Sistema de Gerenciamento de Livros

Sistema web completo para gerenciamento de biblioteca pessoal desenvolvido com **ASP.NET Core MVC**, **Entity Framework Core**, **PostgreSQL** e **Tailwind CSS v4**.

![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-9.0-blue)
![Entity Framework](https://img.shields.io/badge/Entity%20Framework-9.0-green)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-14+-blue)
![Tailwind CSS](https://img.shields.io/badge/Tailwind%20CSS-4.0-38bdf8)

## 🎯 Sobre o Projeto

Este é um sistema completo de gerenciamento de biblioteca pessoal desenvolvido para a oficina de **ASP.NET Core MVC**. O projeto demonstra os principais conceitos e boas práticas de desenvolvimento web com .NET.

### Funcionalidades

✅ **Dashboard Interativo**
- Estatísticas em tempo real (total de livros, lidos, lendo, quero ler)
- Visualização de livros recentes
- Acesso rápido às funcionalidades principais

✅ **Gerenciamento Completo de Livros**
- CRUD completo (Create, Read, Update, Delete)
- Busca por título e autor
- Filtros por categoria e status de leitura
- Validações robustas
- Status de leitura: Quero Ler, Lendo, Lido

✅ **Gerenciamento de Categorias**
- CRUD de categorias
- Visualização de quantidade de livros por categoria
- Proteção contra exclusão de categorias com livros

✅ **Interface Moderna**
- Design responsivo com Tailwind CSS v4
- Interface intuitiva e amigável
- Feedback visual de ações (mensagens de sucesso/erro)

## 🛠️ Tecnologias Utilizadas

- **ASP.NET Core 9.0** - Framework web
- **Entity Framework Core 9.0** - ORM para acesso a dados
- **PostgreSQL 14+** - Banco de dados relacional
- **Tailwind CSS v4** - Framework CSS (via CDN, sem build)
- **C# 12** - Linguagem de programação

## 📋 Pré-requisitos

Antes de executar o projeto, certifique-se de ter:

- [.NET 8.0 SDK ou superior](https://dotnet.microsoft.com/download)
- **PostgreSQL 14 ou superior** (escolha uma opção):
  - [PostgreSQL instalado localmente](https://www.postgresql.org/download/)
  - **OU** [Docker](https://www.docker.com/get-started) + Docker Compose
- Editor de código (VS Code, Visual Studio ou Rider)

## 🚀 Como Executar

### 1. Clone o repositório

```bash
git clone <url-do-repositorio>
cd PersonalBiblio
```

### 2. Configure o banco de dados

#### Opção A: Usando Docker (Recomendado para a Oficina) 🐳

```bash
# Subir o PostgreSQL
docker-compose up -d

# Verificar se está rodando
docker-compose ps
```

A connection string já está configurada! Pule para o passo 3.

📖 **Documentação completa**: [docs/DOCKER.md](docs/DOCKER.md)

#### Opção B: PostgreSQL Local

Crie um banco no PostgreSQL:

```sql
CREATE DATABASE biblioteca_pessoal;
```

Edite a connection string em `src/PersonalBiblio/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=biblioteca_pessoal;Username=seu_usuario;Password=sua_senha"
  }
}
```

### 3. Execute as migrations

```bash
cd src/PersonalBiblio
dotnet ef database update
```

### 4. Execute a aplicação

```bash
dotnet run
```

Acesse: `https://localhost:5001`

## 📂 Estrutura do Projeto

```
PersonalBiblio/
├── src/
│   └── PersonalBiblio/
│       ├── Controllers/          # Controllers MVC
│       │   ├── HomeController.cs
│       │   ├── LivrosController.cs
│       │   └── CategoriasController.cs
│       ├── Data/                 # DbContext e configurações
│       │   └── BibliotecaContext.cs
│       ├── Models/               # Entidades do domínio
│       │   ├── Livro.cs
│       │   ├── Categoria.cs
│       │   └── StatusLeitura.cs
│       ├── Views/                # Views Razor
│       │   ├── Home/
│       │   ├── Livros/
│       │   ├── Categorias/
│       │   └── Shared/
│       ├── wwwroot/              # Arquivos estáticos
│       ├── Program.cs            # Configuração da aplicação
│       └── appsettings.json      # Configurações
├── docs/                         # Documentação da oficina
│   ├── README.md
│   ├── GUIA-COMPLETO.md
│   └── REFERENCIA-RAPIDA.md
└── README.md
```

## 📖 Documentação

A documentação completa da oficina está disponível na pasta `docs/`:

- **[README.md](docs/README.md)** - Visão geral da oficina
- **[GUIA-COMPLETO.md](docs/GUIA-COMPLETO.md)** - Guia passo a passo completo
- **[REFERENCIA-RAPIDA.md](docs/REFERENCIA-RAPIDA.md)** - Referência rápida de comandos e conceitos
- **[DOCKER.md](docs/DOCKER.md)** - Guia de uso do PostgreSQL com Docker

## 🎓 Conceitos Abordados

### ASP.NET Core MVC
- Padrão MVC (Model-View-Controller)
- Routing e navegação
- Tag Helpers
- ViewBag, ViewData e TempData
- Validações server-side

### Entity Framework Core
- Code First approach
- DbContext e configuração
- Migrations
- Relacionamentos (1:N)
- LINQ Queries
- Seed Data

### Front-end
- Tailwind CSS v4 via CDN (sem build/npm)
- Design responsivo
- Componentes UI modernos
- Forms e validações

### Boas Práticas
- Separation of Concerns
- Data Annotations
- Async/Await
- DRY (Don't Repeat Yourself)
- Clean Code

## 🧪 Testando a Aplicação

### Fluxo de Teste Recomendado

1. **Criar Categorias**
   - Acesse "Categorias"
   - Crie algumas categorias (Ficção, Tecnologia, etc.)

2. **Adicionar Livros**
   - Acesse "Livros"
   - Adicione livros com diferentes status
   - Teste as validações (campos obrigatórios)

3. **Testar Busca e Filtros**
   - Busque por título ou autor
   - Filtre por categoria
   - Filtre por status de leitura

4. **Verificar Dashboard**
   - Volte ao Dashboard
   - Veja as estatísticas atualizadas
   - Visualize os livros recentes

5. **Editar e Excluir**
   - Edite informações de um livro
   - Exclua um livro
   - Tente excluir uma categoria com livros

## 🔧 Comandos Úteis

```bash
# Compilar
dotnet build

# Executar
dotnet run

# Criar migration
dotnet ef migrations add NomeDaMigration

# Aplicar migrations
dotnet ef database update

# Reverter última migration
dotnet ef migrations remove

# Limpar build
dotnet clean
```

## 🚀 Melhorias Futuras

Sugestões para expandir o projeto:

- [ ] Autenticação e autorização (ASP.NET Identity)
- [ ] Upload de capas de livros
- [ ] Sistema de avaliações e resenhas
- [ ] API REST para consumo mobile
- [ ] Relatórios em PDF
- [ ] Integração com APIs externas (Google Books)
- [ ] Sistema de empréstimos
- [ ] Tags personalizadas
- [ ] Busca avançada (full-text search)
- [ ] Temas customizáveis
- [ ] Notificações por email

## 📝 Licença

Este projeto é distribuído sob a licença MIT para fins educacionais.

## 🤝 Contribuindo

Contribuições são bem-vindas! Sinta-se à vontade para:

1. Fazer um fork do projeto
2. Criar uma branch para sua feature (`git checkout -b feature/NovaFeature`)
3. Commit suas mudanças (`git commit -m 'Adiciona nova feature'`)
4. Push para a branch (`git push origin feature/NovaFeature`)
5. Abrir um Pull Request

## 📧 Contato

Para dúvidas ou sugestões sobre a oficina, entre em contato!

---

**Desenvolvido para a Oficina de ASP.NET Core MVC** 🚀

Aprenda, pratique e construa aplicações web modernas com .NET!
