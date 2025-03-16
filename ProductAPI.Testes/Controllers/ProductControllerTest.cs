using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Controllers;
using ProductAPI.Data;
using ProductAPI.Data.DTO;
using ProductAPI.Models;
using ProductAPI.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ProductAPI.Testes.Controllers
{
    public class ProductControllerTest
    {
        private readonly ProdutoContext _context;
        private readonly ProductController _controller;
        private readonly IMapper _mapper;

        public ProductControllerTest()
        {
            var options = new DbContextOptionsBuilder<ProdutoContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;

            _context = new ProdutoContext(options);

            _context.Produtos.RemoveRange(_context.Produtos);
            _context.SaveChanges();

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateProdutoDTO, ProdutoContext>();
                cfg.CreateMap<Produto, ReadProdutoDTO>();
                cfg.AddProfile<ProdutoProfile>();
            });
            _mapper = mockMapper.CreateMapper();

            _controller = new ProductController(_context, _mapper);

            _context.Produtos.AddRange(new List<Produto>
            {
                new Produto { Nome = "Teclado", Estoque = 120, Valor = 99.99m },
                new Produto { Nome = "Mouse", Estoque = 140, Valor = 199.99m },
                new Produto { Nome = "Headset", Estoque = 160, Valor = 299.99m },
            });
            _context.SaveChanges();

   
        }

        [Fact]
        public void RetornaTodosProdutos_ListaOrdenadaNome()
        {
            var resultado = _controller.RetornaTodosProdutos();


            Assert.NotNull(resultado);
            Assert.Equal(3,resultado.Count());
            Assert.Equal("Headset", resultado.First().Nome);
        }
        [Fact]
        public void RetornaProdutoId_ProdutoExistente()
        {

            var produto = _context.Produtos.FirstOrDefault(p => p.Nome == "Teclado");
            Assert.NotNull(produto); 
            int produtoId = produto.Id; 

            var resultado = _controller.RetornaProdutoId(produtoId) as OkObjectResult;

            Assert.NotNull(resultado);
            Assert.Equal(produtoId, ((Produto)resultado.Value).Id);
        }


        [Fact]
        public void RetornaProdutoId_ProdutoInexistente()
        {
            var resultado = _controller.RetornaProdutoId(99);


            Assert.IsType<NotFoundResult>(resultado);
        }
        [Fact]
        public void RetornaProdutoPorNome_ProdutoExistente()
        {

            var resultado = _controller.RetornaProdutoPorNome("Teclado");

            var actionResult = Assert.IsType<ActionResult<Produto>>(resultado);

            var okResult = actionResult.Result as OkObjectResult;
            Assert.NotNull(okResult);

            var produtoRetornado = Assert.IsType<Produto>(okResult.Value);
            Assert.Equal("Teclado", produtoRetornado.Nome);
        }
        [Fact]
        public void RetornaProdutoPorNome_ProdutoInexistente()
        {
            var resultado = _controller.RetornaProdutoPorNome("ProdutoInexistente");

            var actionResult = Assert.IsType<ActionResult<Produto>>(resultado);

            var notFoundResult = actionResult.Result as NotFoundObjectResult;
            Assert.NotNull(notFoundResult);
        }

        [Fact]  
        public void AdicionaProduto_ProdutoValido()
        {
            var novoProduto = new CreateProdutoDTO
            {
                Nome = "Pen drive",
                Estoque = 88,
                Valor = 49.99m,
            };

            var resultado = _controller.AdicionaProduto(novoProduto) as CreatedAtActionResult;

            Assert.NotNull(resultado);
            Assert.Equal(201, resultado.StatusCode);
            Assert.NotNull(resultado.Value);
            Assert.IsType<Produto>(resultado.Value);
        }

        [Fact]
        public void AtualizaProduto_ProdutoExistente()
        {

            var produto = new Produto { Nome = "Teclado", Estoque = 120, Valor = 99.99m };
            _context.Produtos.Add(produto);
            _context.SaveChanges();

            var produtoDto = new UpdateProdutoDTO { Nome = "Teclado Atualizado", Estoque = 130, Valor = 110.00m };

            var resultado = _controller.AtualizaProduto(produto.Id, produtoDto) as NoContentResult;

            Assert.NotNull(resultado);
            var produtoAtualizado = _context.Produtos.FirstOrDefault(p => p.Id == produto.Id);
            Assert.NotNull(produtoAtualizado);
            Assert.Equal("Teclado Atualizado", produtoAtualizado.Nome);
            Assert.Equal(130, produtoAtualizado.Estoque);
            Assert.Equal(110.00m, produtoAtualizado.Valor);
        }

        [Fact]
        public void AtualizaProduto_ProdutoInexistente()
        {

            var produtoDto = new UpdateProdutoDTO { Nome = "Produto Não Existente", Estoque = 0, Valor = 0m };

            var resultado = _controller.AtualizaProduto(99999, produtoDto) as NotFoundResult;

            Assert.NotNull(resultado);
        }

        [Fact]
        public void DeletaProduto_ProdutoExistente()
        {
            var resultado = _controller.DeletaProduto(101);

            Assert.IsType<NotFoundResult>(resultado);
            Assert.Null(_context.Produtos.Find(100));
        }

        [Fact]
        public void DeletaProduto_ProdutoInexistente()
        {
            var resultado = _controller.DeletaProduto(90);

            Assert.IsType<NotFoundResult>(resultado);
        }
    }
}
