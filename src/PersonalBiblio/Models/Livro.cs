using System.ComponentModel.DataAnnotations;

namespace PersonalBiblio.Models;

public class Livro
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O título é obrigatório")]
    [StringLength(200, ErrorMessage = "O título deve ter no máximo 200 caracteres")]
    public string Titulo { get; set; } = string.Empty;

    [Required(ErrorMessage = "O autor é obrigatório")]
    [StringLength(150, ErrorMessage = "O nome do autor deve ter no máximo 150 caracteres")]
    public string Autor { get; set; } = string.Empty;

    [StringLength(13, MinimumLength = 10, ErrorMessage = "O ISBN deve ter entre 10 e 13 caracteres")]
    public string? ISBN { get; set; }

    [Range(1000, 9999, ErrorMessage = "Informe um ano válido")]
    public int? Ano { get; set; }

    [Required]
    public StatusLeitura Status { get; set; } = StatusLeitura.QueroLer;

    // Foreign Key
    [Required(ErrorMessage = "Selecione uma categoria")]
    public int CategoriaId { get; set; }

    // Navigation Property
    public Categoria? Categoria { get; set; }

    // Campos de auditoria
    public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
    public DateTime? DataAtualizacao { get; set; }
}
