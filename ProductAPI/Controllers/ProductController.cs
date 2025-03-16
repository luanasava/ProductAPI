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


    [HttpPost]
    public IActionResult AdicionaProduto([FromBody] CreateProdutoDTO produtoDto)
    {
        Produto produto = _mapper.Map<Produto>(produtoDto);
        _context.Produtos.Add(produto);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RetornaProdutoId), new { id = produto.Id}, produto);
    }


    [HttpGet]
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

    [HttpGet("id")]
    public IActionResult RetornaProdutoId(int Id)
    {
        var produto = _context.Produtos.FirstOrDefault(produto =>  produto.Id == Id);
        if (produto == null) return NotFound();
        var produtoDto = _mapper.Map<ReadProdutoDTO>(produto);
        return Ok(produto);
    }

    [HttpGet("nome/{nome}")]
    public ActionResult<Produto> RetornaProdutoPorNome(string nome)
    {
        var produto = _context.Produtos.FirstOrDefault(produto => produto.Nome == nome);
        if (produto == null) return NotFound("Produto não encontrado.");
        return Ok(produto);
    }

    [HttpPut("id")]
    public IActionResult AtualizaProduto(int id, [FromBody] UpdateProdutoDTO produtoDto)
    {
        var produto = _context.Produtos.FirstOrDefault(produto => produto.Id == id);
        if (produto == null) return NotFound(); 
        _mapper.Map(produtoDto, produto);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeletaProduto(int id)
    {
        var produto = _context.Produtos.FirstOrDefault(produto => produto.Id == id);
        if (produto == null) return NotFound();
        _context.Remove(produto);
        _context.SaveChanges();
        return NoContent();
    } 

}
