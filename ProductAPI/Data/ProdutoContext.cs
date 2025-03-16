using Microsoft.EntityFrameworkCore;
using ProductAPI.Models;

namespace ProductAPI.Data;

public class ProdutoContext : DbContext
{
    public ProdutoContext(DbContextOptions<ProdutoContext> opts) : base(opts)
    {
    }

    public DbSet<Produto> Produtos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Produto>().HasData(
            new Produto { Id = 1, Nome = "SSD", Estoque = 18, Valor = 499.99m },
            new Produto { Id = 2, Nome = "Placa de Vídeo", Estoque = 8, Valor = 2300.99m },
            new Produto { Id = 3, Nome = "Processador", Estoque = 12, Valor = 800.99m },
            new Produto { Id = 4, Nome = "Memória RAM", Estoque = 22, Valor = 520.99m },
            new Produto { Id = 5, Nome = "Monitor", Estoque = 4, Valor = 1250.99m }
        );
    }
}
