using System.ComponentModel.DataAnnotations;

namespace Cinema.Data;

public class ReadEnderecoDto
{
    public int Id { get; set; }

    public string Logradouro { get; set; }

    public int Numero { get; set; }
    
    public string Bairro { get; set; }
}