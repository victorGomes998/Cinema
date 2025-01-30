using System.ComponentModel.DataAnnotations;
using Microsoft.Net.Http.Headers;

namespace Cinema.Model;

public class Endereco
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public string Logradouro { get; set; }

    [Required]
    public int Numero { get; set; }

    [Required]
    public string Bairro { get; set; }

    public virtual Estabelecimento Estabelecimento { get; set; }
}