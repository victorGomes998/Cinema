namespace Cinema.Data.Dtos;

public class ReadFilmeDto
{
    public string Titulo { get; set; }
    public string Genero { get; set; }
    public int Duracao { get; set; }   
    public string HoraDaConsulta { get; set; } = DateTime.Now.ToString("dd/MM/yyyy hh:mm");
}