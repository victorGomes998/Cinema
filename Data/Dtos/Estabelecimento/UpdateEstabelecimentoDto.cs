using System.ComponentModel.DataAnnotations;

namespace Cinema.Data;

public class UpdateEstabelecimentoDto
{
    [Required(ErrorMessage = "O campo Nome é obrigatório")]
    public string Nome { get; set; }
    public int EnderecoId { get; set; }
}