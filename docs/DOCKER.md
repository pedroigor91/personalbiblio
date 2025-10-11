# Docker - PostgreSQL para a Oficina

## 🐳 Executar PostgreSQL com Docker

### Iniciar o Banco de Dados

```bash
# Na raiz do projeto PersonalBiblio
docker-compose up -d
```

### Verificar Status

```bash
# Ver logs
docker-compose logs -f postgres

# Verificar se está rodando
docker-compose ps
```

### Parar o Banco

```bash
# Parar containers
docker-compose down

# Parar e remover volumes (apaga dados!)
docker-compose down -v
```

## 🔌 Conectar ao PostgreSQL

### Credenciais

- **Host**: localhost
- **Port**: 5432
- **Database**: biblioteca_pessoal
- **Username**: postgres
- **Password**: postgres

### Connection String (já configurada no projeto)

```
Host=localhost;Port=5432;Database=biblioteca_pessoal;Username=postgres;Password=postgres
```

## 🛠️ Comandos Úteis

### Acessar o PostgreSQL via CLI

```bash
docker exec -it biblioteca-postgres psql -U postgres -d biblioteca_pessoal
```

### Executar comandos SQL

```bash
# Listar tabelas
docker exec -it biblioteca-postgres psql -U postgres -d biblioteca_pessoal -c "\dt"

# Ver dados
docker exec -it biblioteca-postgres psql -U postgres -d biblioteca_pessoal -c "SELECT * FROM categorias;"
```

### Backup e Restore

```bash
# Backup
docker exec biblioteca-postgres pg_dump -U postgres biblioteca_pessoal > backup.sql

# Restore
docker exec -i biblioteca-postgres psql -U postgres biblioteca_pessoal < backup.sql
```

## 📝 Sequência Recomendada para a Oficina

```bash
# 1. Subir o PostgreSQL
docker-compose up -d

# 2. Aguardar alguns segundos
sleep 5

# 3. Executar migrations do projeto
cd src/PersonalBiblio
dotnet ef database update

# 4. Rodar a aplicação
dotnet run
```

## 🔍 Troubleshooting

### Porta 5432 já está em uso

Se você já tem PostgreSQL instalado localmente:

**Opção 1**: Parar o PostgreSQL local
```bash
sudo systemctl stop postgresql
```

**Opção 2**: Mudar a porta no docker-compose.yml
```yaml
ports:
  - "5433:5432"  # Usar porta 5433 no host
```

E ajustar a connection string:
```
Host=localhost;Port=5433;Database=biblioteca_pessoal;Username=postgres;Password=postgres
```

### Container não inicia

```bash
# Ver logs detalhados
docker-compose logs postgres

# Remover volumes e recriar
docker-compose down -v
docker-compose up -d
```

### Erro de permissão no volume

```bash
# Linux: ajustar permissões
sudo chown -R $USER:$USER ~/.docker

# Ou usar sudo
sudo docker-compose up -d
```

## 🧹 Limpeza Completa

```bash
# Parar tudo
docker-compose down

# Remover volumes (apaga dados!)
docker-compose down -v

# Remover imagens
docker rmi postgres:16-alpine
```

---

**Pronto para usar!** 🚀
