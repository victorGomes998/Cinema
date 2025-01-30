using AutoMapper;
using Cinema.Data;
using Cinema.Model;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers;

/// <summary>
/// 
/// </summary>
[ApiController]
[Route("[controller]")]
public class EnderecoController : ControllerBase
{
    private BancoContext _context;
    private IMapper _mapper;

    public EnderecoController(BancoContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost("v1/{id}")]
    public IActionResult AdicionaEndereco([FromBody] CreateEnderecoDto enderecoDto)
    {
        Endereco endereco = _mapper.Map<Endereco>(enderecoDto);

        _context.Add(endereco);
        _context.SaveChanges();

        return CreatedAtAction(nameof(BuscaEnderecoPorId), new { Id = endereco.Id }, endereco);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pagina"></param>
    /// <returns></returns>
    [HttpGet("v1")]
    public IEnumerable<ReadEnderecoDto> BuscaEndereco(int pagina = 0)
    {
        return _mapper.Map<List<ReadEnderecoDto>>(_context.Enderecos.Skip(10 * pagina).Take(10));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    [HttpGet("v1/{id}")]
    public IActionResult BuscaEnderecoPorId(int id)
    {
        var endereco = _context.Enderecos.FirstOrDefault(a => a.Id == id);

        if (endereco == null) return NotFound();

        var enderecoDtoDto = _mapper.Map<ReadEnderecoDto>(endereco);

        return Ok(enderecoDtoDto);

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="enderecoDto"></param>
    /// <returns></returns>
    [HttpPut("v1/{id}")]
    public IActionResult AtualizaEstabelecimento(int id, [FromBody] UpdateEnderecoDto enderecoDto)
    {
        var endereco = _context.Enderecos.FirstOrDefault(a => a.Id == id);

        if (endereco == null) return NotFound();

        _mapper.Map(enderecoDto, endereco);
        _context.SaveChanges();

        return NoContent();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    public IActionResult DeletaEstabelecimento(int id)
    {
        var endereco = _context.Enderecos.FirstOrDefault(a => a.Id == id);
        
        if (endereco == null) return NotFound();

        _context.Enderecos.Remove(endereco);
        _context.SaveChanges();

        return NoContent();
    }   
}