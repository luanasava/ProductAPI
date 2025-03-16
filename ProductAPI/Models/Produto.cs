using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductAPI.Models;

public class Produto
{
    private decimal _valor;

    [Key]

    [Required(ErrorMessage = "É necessário informar o ID do produto.")]
    public int Id { get; set; }


    [Required (ErrorMessage = "O nome do produto é obrigatório.")]
    public string Nome { get; set; }


    [Required]
    [Range(1, 1000, ErrorMessage = "O estoque do produto deve ter no mínimo 1 unidade e no máximo 1000 unidades.")]
    public int Estoque { get; set; }


    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "O valor não pode ser negativo.")]
    public decimal Valor 
    {
        get => _valor;
        set => _valor = Math.Round(value, 6); 
    }
}
