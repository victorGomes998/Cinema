using System.ComponentModel.DataAnnotations;

namespace Cinema.Data.Dtos;

public class CreateFilmeDto
{
    [Required(ErrorMessage = "O título do filme é obrigatório")]
    public string Titulo { get; set; }

    [Required(ErrorMessage = "O gênero do filme é obrigatório")]
    public string Genero { get; set; }

    [Required(ErrorMessage = "a duração do filme é obrigatório")]
    [Range(60, 240, ErrorMessage = "Tamanho de filme inválido, este deve ser compreendido entre 60 e 240 minutos")]
    public int Duracao { get; set; }
}