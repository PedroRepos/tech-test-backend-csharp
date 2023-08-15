using API_Produto.Controllers;
using API_Produto.Context;
using API_Produto.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace API_Produto_Tests
{
    /// <summary>
    /// Realiza os testes de todos os EndPoints do SQL | Tests all SQL EndPoints
    /// </summary>
    [TestClass]
    public class ProdutoControllerTests
    {
        private ProdutoController _controller;
        private ProdutoContext _context;

        [TestInitialize]
        public void Initialize()
        {
            var options = new DbContextOptionsBuilder<ProdutoContext>()
                .UseInMemoryDatabase(databaseName: "SQLMEMORYDATABASE")
                .Options;

            _context = new ProdutoContext(options);
            _controller = new ProdutoController(_context); // Injeta o contexto diretamente no controlador
        }

        [TestMethod]
        public void Test_CriarNovoProduto_Sucesso()
        {
            // Arrange
            var produto = new Produto
            {
                Nome = "Produto de Teste",
                Preco = 10.99,
                QuantidadeEmEstoque = 5,
                DataDeCriacao = DateTime.Now
            };

            var result = _controller.CriarNovoProduto(produto) as OkObjectResult;

            
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode); // Verifica o status code
            Assert.IsInstanceOfType(result.Value, typeof(Produto)); // Deve retornar um objeto Produto
            var produtoInserido = result.Value as Produto;
            Assert.AreEqual(produto.Nome, produtoInserido.Nome); // Verifica se o nome do produto foi inserido corretamente
            Assert.AreEqual(produto.Preco, produtoInserido.Preco); // Verifica se o preço do produto foi inserido corretamente
            // Adicione mais verificações conforme necessário
        }

    [TestMethod]
    public void Test_ListaProdutos_Sucesso()
    {
        // Arrange
        var produtos = new List<Produto>
            {
                new Produto
                {
                    Nome = "Produto 1",
                    Preco = 10.99,
                    QuantidadeEmEstoque = 5,
                    DataDeCriacao = DateTime.Now
                },
                new Produto
                {
                    Nome = "Produto 2",
                    Preco = 15.49,
                    QuantidadeEmEstoque = 8,
                    DataDeCriacao = DateTime.Now
                }
            };

        _context.Produtos.AddRange(produtos);
        _context.SaveChanges();

        // Act
        var result = _controller.ListaProdutos() as ObjectResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode); // Código de status Ok
        Assert.IsInstanceOfType(result.Value, typeof(List<Produto>)); // Deve retornar uma lista de produtos
        var produtosRetornados = result.Value as List<Produto>;
    }

    [TestMethod]
    public void Test_AtualizaDadosProduto_Sucesso()
    {
        // Arrange
        var produto = new Produto
        {
            Nome = "Produto Original",
            Preco = 10.99,
            QuantidadeEmEstoque = 5,
            DataDeCriacao = DateTime.Now
        };

        _context.Produtos.Add(produto);
        _context.SaveChanges();

        var produtoAtualizado = new Produto
        {
            Id = produto.Id,
            Nome = "Produto Atualizado",
            Preco = 15.99,
            QuantidadeEmEstoque = 8,
            DataDeCriacao = DateTime.Now.AddDays(-1)
        };

        var result = _controller.AtualizaDadosProduto(produto.Id, produtoAtualizado) as OkObjectResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode); 
        Assert.IsInstanceOfType(result.Value, typeof(Produto)); 
        var produtoAtualizadoRetornado = result.Value as Produto;
        Assert.AreEqual(produtoAtualizado.Nome, produtoAtualizadoRetornado.Nome); 
        Assert.AreEqual(produtoAtualizado.Preco, produtoAtualizadoRetornado.Preco); 
        Assert.AreEqual(produtoAtualizado.QuantidadeEmEstoque, produtoAtualizadoRetornado.QuantidadeEmEstoque); // Verifica se a quantidade em estoque do produto foi atualizada corretamente
        Assert.AreEqual(produtoAtualizado.DataDeCriacao, produtoAtualizadoRetornado.DataDeCriacao); // Verifica se a data de criação do produto foi atualizada corretamente
    }

    [TestMethod]
    public void Test_ApagaProduto_Sucesso()
    {
        // Arrange
        var produto = new Produto
        {
            Nome = "Produto a ser apagado",
            Preco = 10.99,
            QuantidadeEmEstoque = 5,
            DataDeCriacao = DateTime.Now
        };

        _context.Produtos.Add(produto);
        _context.SaveChanges();
        
        var result = _controller.ApagaProduto(produto.Id) as NoContentResult;
        
        Assert.IsNotNull(result);
        Assert.AreEqual(204, result.StatusCode); // Código de status NoContent
        var produtoNoBanco = _context.Produtos.Find(produto.Id);
        Assert.IsNull(produtoNoBanco); // Verifica se o produto foi removido do banco de dados
    }

    [TestMethod]
    public void Test_ApagaProduto_ProdutoNaoEncontrado()
    {
        
        var produtoIdInexistente = 123458586; // ID fictício que não existe no banco de dados

        var result = _controller.ApagaProduto(produtoIdInexistente) as NotFoundResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(404, result.StatusCode); // Código de status NotFound
    }
}
}
