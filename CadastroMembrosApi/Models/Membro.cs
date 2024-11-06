using System.ComponentModel.DataAnnotations;

namespace CadastroMembrosApi.Models;

public class Membro
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(80)]
    public string? NomeMembro { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string? Email { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime DataDeNascimento { get; set; }

    [StringLength(80)]
    public string? Cargo { get; set; }

    [StringLength(80)]
    public string? Departamento { get; set; }

    [StringLength(300)]
    public string? Endereco { get; set; }
}
