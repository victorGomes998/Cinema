using Microsoft.AspNetCore.Mvc;
using Cinema.Model;

namespace Cinema.FilmesController;

[ApiController]
[Route("[controller]")]
public class FilmesController : ControllerBase
{
    private static List<Filme> filmes = new List<Filme>();
    private static int Id = 0;
    private static int pagina = -1;

    [HttpPost]
    public IActionResult AdicionaFilme([FromBody] Filme filme)
    {
        filme.Id = Id++;
        filmes.Add(filme);

        return CreatedAtAction(nameof(BuscaFilmePorId), new { Id = filme.Id }, filme);
    }

    [HttpGet]
    public IEnumerable<Filme> BuscaFilmes()
    {
        pagina++;

        return filmes.Skip(10 * pagina).Take(10);
    }

    [HttpGet("{Id}")]
    public IActionResult BuscaFilmePorId(int Id)
    {
        var filme = filmes.FirstOrDefault(a => a.Id == Id);

        if (filme == null) return NotFound();

        return Ok(filme);
    }
}
