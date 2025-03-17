using AutoMapper;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Data;
using ProductAPI.Data.DTO;
using ProductAPI.Models;

namespace ProductAPI.Controllers;


[ApiController]
[Route("produto")]
public class ProductController : ControllerBase
{

    private ProdutoContext _context;
    private IMapper _mapper;

    public ProductController(ProdutoContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Adiciona um produto 
    /// </summary>
    /// <param name="produtoDto"></param>
    /// <returns>IActionResult</returns>
    /// <response code="201"></response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AdicionaProduto([FromBody] CreateProdutoDTO produtoDto)
    {
        Produto produto = _mapper.Map<Produto>(produtoDto);
        _context.Produtos.Add(produto);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RetornaProdutoId), new { id = produto.Id}, produto);
    }

    /// <summary>
    /// Retorna uma lista com todos os produtos
    /// </summary>
    /// <returns>IActionResult</returns>
    /// <response code="200"></response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<ReadProdutoDTO> RetornaTodosProdutos(
    [FromQuery] string? busca = null,
    [FromQuery] string? ordenarPor = "nome")
    {
        IQueryable<Produto> query = _context.Produtos;

        if (!string.IsNullOrWhiteSpace(busca))
        {
            query = query.Where(p => p.Nome.Contains(busca));
        }

        query = ordenarPor.ToLower() switch
        {
            "valor" => query.OrderBy(p => p.Valor),
            "estoque" => query.OrderBy(p => p.Estoque),
            _ => query.OrderBy(p => p.Nome) 
        };

        return _mapper.Map<List<ReadProdutoDTO>>(query.ToList());
    }
    /// <summary>
    /// Retorna um produto específico por ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns>IActionResult</returns>
    /// <response code="200"></response>
    /// <response code="404"></response>
    [HttpGet("id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult RetornaProdutoId(int Id)
    {
        var produto = _context.Produtos.FirstOrDefault(produto =>  produto.Id == Id);
        if (produto == null) return NotFound();
        var produtoDto = _mapper.Map<ReadProdutoDTO>(produto);
        return Ok(produto);
    }

    /// <summary>
    /// Retorna um produto específico por nome
    /// </summary>
    /// <param name="nome"></param>
    /// <returns>IActionResult</returns>
    /// <response code="200"></response>
    /// <response code="404"></response>
    [HttpGet("nome")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Produto> RetornaProdutoPorNome(string nome)
    {
        var produto = _context.Produtos.FirstOrDefault(produto => produto.Nome == nome);
        if (produto == null) return NotFound("Produto não encontrado.");
        return Ok(produto);
    }
    /// <summary>
    /// Atualiza um produto existente por ID
    /// </summary>
    /// <param name="id"></param>
    /// <param name="produtoDto"></param>
    /// <returns>IActionResult</returns>
    /// <response code="204"></response>
    /// <response code="404"></response>
    [HttpPut("id")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult AtualizaProduto(int id, [FromBody] UpdateProdutoDTO produtoDto)
    {
        var produto = _context.Produtos.FirstOrDefault(produto => produto.Id == id);
        if (produto == null) return NotFound(); 
        _mapper.Map(produtoDto, produto);
        _context.SaveChanges();
        return NoContent();
    }


    /// <summary>
    /// Remove um produto por ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns>IActionResult</returns>
    /// <response code="204"></response>
    /// <response code="404"></response>
    [HttpDelete("id")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeletaProduto(int id)
    {
        var produto = _context.Produtos.FirstOrDefault(produto => produto.Id == id);
        if (produto == null) return NotFound();
        _context.Remove(produto);
        _context.SaveChanges();
        return NoContent();
    }


}
