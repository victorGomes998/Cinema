using Microsoft.AspNetCore.Mvc;
using Cinema.Model;
using Microsoft.EntityFrameworkCore;
using Cinema.Data;
using Cinema.Data.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;

namespace Cinema.FilmesController;

[ApiController]
[Route("[controller]")]
public class FilmesController : ControllerBase
{
    private FilmeContext _context;
    private IMapper _mapper;

    public FilmesController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Adiciona um filme ao banco de dados
    /// </summary>
    /// <param name="filmeDto">Objeto com os campos necessários para criação de um filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost]
    public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto)
    {
        Filme filme = _mapper.Map<Filme>(filmeDto);

        _context.Filme.Add(filme);
        _context.SaveChanges();

        return CreatedAtAction(nameof(BuscaFilmePorId), new { Id = filme.Id }, filme);
    }

    /// <summary>
    /// Recupera os filmes do banco de dados
    /// </summary>
    /// <returns>IEnumerable</returns>
    [HttpGet]
    public IEnumerable<ReadFilmeDto> BuscaFilmes(int pagina = 0)
    {
        return _mapper.Map<List<ReadFilmeDto>>(_context.Filme.Skip(10 * pagina).Take(10));
    }

    /// <summary>
    /// Recupera um filme do banco de dados com base no id
    /// </summary>
    /// <param name="id">Número único do filme cadastrado</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso o filme esteja cadastrado</response>
    /// <response code="404">Caso não encontre o filme</response>
    [HttpGet("{Id}")]
    public IActionResult BuscaFilmePorId(int Id)
    {
        var filme = _context.Filme.FirstOrDefault(a => a.Id == Id);

        if (filme == null) return NotFound();

        var filmeDto = _mapper.Map<ReadFilmeDto>(filme);

        return Ok(filmeDto);
    }

    /// <summary>
    /// Atualiza um filme do banco de dados com base no id
    /// </summary>
    /// <param name="id">Número único do filme cadastrado</param>
    /// <param name="filmeDto">Objeto com os campos para serem atualizados em um filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso o filme seja atualizado</response>
    /// <response code="404">Caso não encontre o filme</response>
    [HttpPut("{id}")]
    public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
    {
        var filme = _context.Filme.FirstOrDefault(a => a.Id == id);

        if (filme == null) return NotFound();

        _mapper.Map(filmeDto, filme);
        _context.SaveChanges();
        
        return NoContent();
    }

    /// <summary>
    /// Atualiza um filme do banco de dados com base no id
    /// </summary>
    /// <param name="id">Número único do filme cadastrado</param>
    /// <param name="patch">Objeto com os campos parciais de um filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso o filme seja atualizado</response>
    /// <response code="404">Caso não encontre o filme</response>
    [HttpPatch("{id}")]
    public IActionResult AtualizaFilmeParcial(int id, [FromBody] JsonPatchDocument<UpdateFilmeDto> patch)
    {
        var filme = _context.Filme.FirstOrDefault(a => a.Id == id);

        if (filme == null) return NotFound();

        var filmeParaAtualizar = _mapper.Map<UpdateFilmeDto>(filme);

        patch.ApplyTo(filmeParaAtualizar, ModelState);

        if(!TryValidateModel(ModelState)) return ValidationProblem(ModelState);

        _mapper.Map(filmeParaAtualizar, filme);
        _context.SaveChanges();
        
        return NoContent();
    } 

    /// <summary>
    /// Deleta um filme do banco de dados com base no id
    /// </summary>
    /// <param name="id">Número único do filme cadastrado</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso o filme seja deletado</response>
    /// <response code="404">Caso não encontre o filme</response>
    [HttpDelete("{id}")]
    public IActionResult DeletaFilme(int id)
    {
        var filme = _context.Filme.FirstOrDefault(a => a.Id == id);

        if (filme == null) return NotFound();

        _context.Remove(filme);
        _context.SaveChanges();

        return NoContent();
    }
}
