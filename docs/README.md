# Oficina: Sistema de Biblioteca Pessoal com ASP.NET Core MVC

## 📚 Sobre a Oficina

Bem-vindo à oficina de desenvolvimento de um Sistema de Biblioteca Pessoal usando **ASP.NET Core MVC**, **Entity Framework Core**, **PostgreSQL** e **Tailwind CSS v4**.

Esta oficina foi projetada para ser concluída em **50-90 minutos** e cobre os principais conceitos de desenvolvimento web com .NET.

## 🎯 Objetivos de Aprendizagem

Ao final desta oficina, você será capaz de:

- ✅ Criar um projeto ASP.NET Core MVC do zero
- ✅ Trabalhar com Models e Data Annotations
- ✅ Configurar Entity Framework Core com PostgreSQL
- ✅ Criar Migrations e popular banco de dados
- ✅ Implementar Controllers com CRUD completo
- ✅ Criar Views responsivas com Tailwind CSS v4
- ✅ Implementar busca e filtros
- ✅ Trabalhar com validações
- ✅ Criar um Dashboard com estatísticas

## 🛠️ Tecnologias Utilizadas

- **ASP.NET Core 9.0** - Framework web
- **Entity Framework Core** - ORM
- **PostgreSQL** - Banco de dados
- **Tailwind CSS v4** - Framework CSS (via CDN)
- **C# 12** - Linguagem de programação

## 📋 Pré-requisitos

Antes de começar, certifique-se de ter instalado:

- [.NET 8.0 SDK ou superior](https://dotnet.microsoft.com/download)
- [PostgreSQL 14 ou superior](https://www.postgresql.org/download/)
- Um editor de código ([Visual Studio](https://visualstudio.microsoft.com/), [VS Code](https://code.visualstudio.com/) ou [Rider](https://www.jetbrains.com/rider/))
- Cliente Git (opcional)

### Verificar Instalações

```bash
# Verificar .NET
dotnet --version

# Verificar PostgreSQL
psql --version
```

## 📂 Estrutura do Projeto

```
PersonalBiblio/
├── src/
│   └── PersonalBiblio/
│       ├── Controllers/        # Controllers MVC
│       ├── Data/               # DbContext
│       ├── Models/             # Entidades do domínio
│       ├── Views/              # Views Razor
│       │   ├── Home/
│       │   ├── Livros/
│       │   ├── Categorias/
│       │   └── Shared/
│       └── wwwroot/            # Arquivos estáticos
└── docs/                       # Documentação da oficina
```

## 🎓 Roteiro da Oficina

A oficina está dividida em módulos sequenciais:

### 1. [Setup Inicial](01-setup.md)
- Criar o projeto
- Instalar dependências
- Configurar PostgreSQL

### 2. [Models e Domain](02-models.md)
- Criar entidades (Livro, Categoria)
- Adicionar validações
- Criar Enums

### 3. [Database e EF Core](03-database.md)
- Configurar DbContext
- Criar Migrations
- Popular dados iniciais

### 4. [Controllers](04-controllers.md)
- HomeController (Dashboard)
- LivrosController (CRUD completo)
- CategoriasController (CRUD)

### 5. [Views](05-views.md)
- Layout com Tailwind CSS v4
- Views de Livros
- Views de Categorias
- Dashboard

### 6. [Testes e Deploy](06-testes.md)
- Executar a aplicação
- Testar funcionalidades
- Próximos passos

## 🚀 Quick Start

Se você quer apenas executar o projeto pronto:

```bash
# 1. Clone o repositório (ou extraia o ZIP)
cd PersonalBiblio

# 2. Configure a connection string
# Edite src/PersonalBiblio/appsettings.json

# 3. Execute as migrations
cd src/PersonalBiblio
dotnet ef database update

# 4. Execute a aplicação
dotnet run
```

Acesse: `https://localhost:5000`

## 🎨 Funcionalidades do Sistema

### Dashboard
- Estatísticas gerais (total de livros, lidos, lendo, quero ler)
- Lista de livros recentes
- Acesso rápido às funcionalidades

### Gerenciamento de Livros
- ✅ Criar novos livros
- ✅ Listar todos os livros
- ✅ Visualizar detalhes
- ✅ Editar informações
- ✅ Excluir livros
- ✅ Buscar por título/autor
- ✅ Filtrar por categoria
- ✅ Filtrar por status (Quero Ler, Lendo, Lido)

### Gerenciamento de Categorias
- ✅ Criar categorias
- ✅ Listar categorias com contagem de livros
- ✅ Editar categorias
- ✅ Excluir categorias (com proteção)

## 💡 Conceitos Abordados

### ASP.NET Core MVC
- Padrão MVC (Model-View-Controller)
- Routing
- Tag Helpers
- ViewBag/ViewData
- TempData para mensagens

### Entity Framework Core
- Code First
- DbContext
- Migrations
- Relacionamentos (1:N)
- Seed Data
- LINQ Queries

### Front-end
- Tailwind CSS v4 via CDN
- Design responsivo
- Componentes UI modernos
- Forms e validações client-side

### Boas Práticas
- Data Annotations para validação
- Repository Pattern (simplificado)
- Separation of Concerns
- DRY (Don't Repeat Yourself)

## 📚 Recursos Adicionais

- [Documentação ASP.NET Core](https://docs.microsoft.com/aspnet/core)
- [Entity Framework Core Docs](https://docs.microsoft.com/ef/core)
- [Tailwind CSS Docs](https://tailwindcss.com/docs)
- [PostgreSQL Docs](https://www.postgresql.org/docs/)

## 🤝 Melhorias Futuras

Após concluir a oficina, você pode expandir o projeto com:

- 🔐 Autenticação e Autorização (Identity)
- 📱 API REST para consumo mobile
- 🔍 Busca avançada (full-text search)
- 📊 Relatórios e gráficos
- 📖 Avaliações e resenhas de livros
- 🏷️ Tags personalizadas
- 📚 Empréstimos de livros
- 🌐 Integração com APIs de livros (Google Books)
- 📧 Notificações por email
- 🎨 Temas personalizáveis

## 📝 Licença

Este projeto é distribuído sob a licença MIT para fins educacionais.

## ✨ Começar Oficina

Pronto para começar? Vá para o [Módulo 1: Setup Inicial](01-setup.md)

---

**Boa oficina! 🚀**
