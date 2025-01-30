using System.ComponentModel.DataAnnotations;

namespace Cinema.Data;

public class CreateEnderecoDto
{
    [Required]
    public string Logradouro { get; set; }
    
    [Required]
    public int Numero { get; set; }

    [Required]
    public string Bairro { get; set; }
}