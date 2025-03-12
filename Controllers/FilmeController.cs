using Microsoft.AspNetCore.Mvc;
using Cinema.Model;
using Microsoft.EntityFrameworkCore;
using Cinema.Data;
using Cinema.Data.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;

namespace Cinema.Controllers;

/// <summary>
/// Filme
/// </summary>
[ApiController]
[Route("[controller]")]
public class FilmeController : ControllerBase
{
    private BancoContext _context;
    private IMapper _mapper;

    /// <summary>
    /// Filme
    /// </summary>
    public FilmeController(BancoContext context, IMapper mapper)
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
    [HttpPost("v1")]
    public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto)
    {
        Filme filme = _mapper.Map<Filme>(filmeDto);

        _context.Filmes.Add(filme);
        _context.SaveChanges();

        return CreatedAtAction(nameof(BuscaFilmePorId), new { Id = filme.Id }, filme);
    }

    /// <summary>
    /// Recupera os filmes do banco de dados
    /// </summary>
    /// <returns>IEnumerable</returns>
    [HttpGet("v1")]
    public IEnumerable<ReadFilmeDto> BuscaFilmes(int pagina = 0)
    {
        return _mapper.Map<List<ReadFilmeDto>>(_context.Filmes.Skip(10 * pagina).Take(10));
    }

    /// <summary>
    /// Recupera um filme do banco de dados com base no id
    /// </summary>
    /// <param name="id">Número único do filme cadastrado</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso o filme esteja cadastrado</response>
    /// <response code="404">Caso não encontre o filme</response>
    [HttpGet("v1/{id}")]
    public IActionResult BuscaFilmePorId(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(a => a.Id == id);

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
    [HttpPut("v1/{id}")]
    public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
    {
        var filme = _context.Filmes.FirstOrDefault(a => a.Id == id);

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
    [HttpPatch("v1/{id}")]
    public IActionResult AtualizaFilmeParcial(int id, [FromBody] JsonPatchDocument<UpdateFilmeDto> patch)
    {
        var filme = _context.Filmes.FirstOrDefault(a => a.Id == id);

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
    [HttpDelete("v1/{id}")]
    public IActionResult DeletaFilme(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(a => a.Id == id);

        if (filme == null) return NotFound();

        _context.Remove(filme);
        _context.SaveChanges();

        return NoContent();
    }
}
