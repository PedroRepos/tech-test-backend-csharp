using API_Produto.Controllers;
using API_Produto.Entities;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/produtos")]
public class ProdutosMongoDBController : ControllerBase
{
    private readonly IMongoCollection<Produto> _produtoCollection;

    public ProdutosMongoDBController(IMongoDatabase database)
    {
        _produtoCollection = database.GetCollection<Produto>("Produtos");
    }

    [HttpPost("CriaNovoProduto")]
    public async Task<IActionResult> CreateProduto(Produto produto)
    {
        produto.DataDeCriacao = DateTime.UtcNow;

        var regrasECalculos = new RegrasController();
        regrasECalculos.ValidaPreco(produto);

        await _produtoCollection.InsertOneAsync(produto);

        return CreatedAtAction(nameof(GetProdutoById), new { id = produto.Id }, produto);
    }

    [HttpGet("ListaTodosOsProdutos")]
    public async Task<IActionResult> GetAllProdutos()
    {
        var produtos = await _produtoCollection.Find(_ => true).ToListAsync();
        return Ok(produtos);
    }

    [HttpGet("RetornaUmProduto/{id}")]
    public async Task<IActionResult> GetProdutoById(int id)
    {
        var produto = await _produtoCollection.Find(p => p.Id == id).FirstOrDefaultAsync();

        if (produto == null)
        {
            return NotFound();
        }

        return Ok(produto);
    }

    

    [HttpPut("AtualizaDadosProduto/{id}")]
    public async Task<IActionResult> UpdateProduto(int id, Produto updatedProduto)
    {
        var existingProduto = await _produtoCollection.FindOneAndUpdateAsync(
            Builders<Produto>.Filter.Eq(p => p.Id, id),
            Builders<Produto>.Update.Set(p => p.Nome, updatedProduto.Nome)
                                    .Set(p => p.Preco, updatedProduto.Preco)
                                    .Set(p => p.QuantidadeEmEstoque, updatedProduto.QuantidadeEmEstoque),
            new FindOneAndUpdateOptions<Produto> { ReturnDocument = ReturnDocument.After });

        if (existingProduto == null)
        {
            return NotFound();
        }

        return Ok(existingProduto);
    }

    [HttpDelete("ApagaProduto/{id}")]
    public async Task<IActionResult> DeleteProduto(int id)
    {
        var result = await _produtoCollection.DeleteOneAsync(p => p.Id == id);

        if (result.DeletedCount == 0)
        {
            return NotFound();
        }

        return NoContent();
    }
}
