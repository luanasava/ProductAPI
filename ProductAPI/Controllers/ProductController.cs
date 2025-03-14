using Microsoft.AspNetCore.Mvc;
using ProductAPI.Models;

namespace ProductAPI.Controllers;


[ApiController]
[Route("produto")]
public class ProductController : ControllerBase
{

    private static List<Produto> produtos = new List<Produto>();

    [HttpPost]
    public void AdicionaProduto([FromBody] Produto produto)
    {
        produtos.Add(produto);
        Console.WriteLine(produto.Modelo);
        Console.WriteLine(produto.Nome);
    }

}
