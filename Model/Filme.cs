using System.ComponentModel.DataAnnotations;

namespace Cinema.Model;

public class Filme
{
    public int Id { get;set; }
    public string Titulo { get; set; }
    public string Genero { get; set; }
    public int Duracao { get; set; }
}
