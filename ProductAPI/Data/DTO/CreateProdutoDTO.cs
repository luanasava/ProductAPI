using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;

namespace ProductAPI.Data.DTO;

public class CreateProdutoDTO
{
    
private decimal _valor;

    [Required(ErrorMessage = "O nome do produto é obrigatório.")]
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
