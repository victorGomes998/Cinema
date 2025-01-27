using Microsoft.AspNetCore.Mvc;
using Cinema.Model;

namespace Cinema.FilmesController
{
    [ApiController]
    [Route("[controller]")]
    public class FilmesController : ControllerBase
    {
        private static List<Filme> filmes = new List<Filme>();

        [HttpPost]
        public void AdicionaFilme([FromBody] Filme filme)
        {
            filmes.Add(filme);
        }
    }
}