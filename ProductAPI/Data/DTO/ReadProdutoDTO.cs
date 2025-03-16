using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;

namespace ProductAPI.Data.DTO
{
    public class ReadProdutoDTO
    {
        private decimal _valor;
        public string Nome { get; set; }

        public int Estoque { get; set; }

        public decimal Valor
        {
            get => _valor;
            set => _valor = Math.Round(value, 6);
        }

        public DateTime HoraDaConsulta { get; set; } = DateTime.Now;
    }
}
