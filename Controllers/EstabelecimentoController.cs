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
public class EstabelecimentoController : ControllerBase
{   
    private BancoContext _context;
    private IMapper _mapper;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="mapper"></param>
    public EstabelecimentoController(BancoContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="estabelecimentoDto"></param>
    /// <returns></returns>
    [HttpPost("v1")]
    public IActionResult AdicionaEstabelecimento([FromBody] CreateEstabelecimentoDto estabelecimentoDto)
    {
        Estabelecimento estabelecimento = _mapper.Map<Estabelecimento>(estabelecimentoDto);

        _context.Add(estabelecimento);
        _context.SaveChanges();

        return CreatedAtAction(nameof(BuscaEstabelcimentoPorId), new { Id = estabelecimento.Id }, estabelecimento);
    }

    /// <summary>
    /// 
    /// </summary>
    [HttpGet("v1")]
    public IEnumerable<ReadEstabelecimentoDto> BuscaEstabelcimentos(int pagina = 0)
    {
        return _mapper.Map<List<ReadEstabelecimentoDto>>(_context.Estabelecimentos.Skip(10 * pagina).Take(10).ToList());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    [HttpGet("v1/{id}")]
    public IActionResult BuscaEstabelcimentoPorId(int id)
    {
        var estabelecimento = _context.Estabelecimentos.FirstOrDefault(a => a.Id == id);

        if (estabelecimento == null) return NotFound();

        var estabelecimentoDto = _mapper.Map<ReadEstabelecimentoDto>(estabelecimento);

        return Ok(estabelecimentoDto);

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="estabelecimentoDto"></param>
    /// <returns></returns>
    [HttpPut("v1/{id}")]
    public IActionResult AtualizaEstabelecimento(int id, [FromBody] UpdateEstabelecimentoDto estabelecimentoDto)
    {
        var estabelecimento = _context.Estabelecimentos.FirstOrDefault(a => a.Id == id);

        if (estabelecimento == null) return NotFound();

        _mapper.Map(estabelecimentoDto, estabelecimento);
        _context.SaveChanges();

        return NoContent();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("v1/{id}")]
    public IActionResult DeletaEstabelecimento(int id)
    {
        var estabelecimento = _context.Estabelecimentos.FirstOrDefault(a => a.Id == id);
        
        if (estabelecimento == null) return NotFound();

        _context.Estabelecimentos.Remove(estabelecimento);
        _context.SaveChanges();

        return NoContent();
    }
}