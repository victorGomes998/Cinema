using System.ComponentModel.DataAnnotations;

namespace Cinema.Model;

public class Estabelecimento
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo Nome é obrigatório")]
    public string Nome { get; set; }

    public int EnderecoId { get; set; }

    public virtual Endereco Endereco { get; set; }
}