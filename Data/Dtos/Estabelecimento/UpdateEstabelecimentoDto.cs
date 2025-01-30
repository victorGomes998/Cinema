using System.ComponentModel.DataAnnotations;

namespace Cinema.Data;

public class UpdateEstabelecimentoDto
{
    [Required(ErrorMessage = "O campo Id é obrigatório")]
    public int Id { get; set; }
    [Required(ErrorMessage = "O campo Nome é obrigatório")]
    public string Nome { get; set; }
}