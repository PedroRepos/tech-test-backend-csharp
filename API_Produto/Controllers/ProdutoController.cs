using API_Produto.Context;
using API_Produto.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API_Produto.Controllers
{
    /// <summary>
    /// Controladora incorporada com os métodos que realizam requisições HTTP para o servidor | Embedded controller with the methods that perform HTTP requests to the server.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoContext _context;

        public ProdutoController(ProdutoContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Realiza o Post criando uma nova informação no banco de dados| Performs the Post by creating a new information in the database
        /// </summary>
        /// <param name="produto"></param>
        /// <returns>StatusCode</returns>
        [HttpPost("CriaNovoProduto")]
        public IActionResult CriarNovoProduto(Produto produto)
        {
            var regrasECalculos = new RegrasController();
            regrasECalculos.ValidaPreco(produto);

            _context.Add(produto);
            _context.SaveChanges();
            return Ok(produto);
        }


        /// <summary>
        /// Realiza a consulta ao banco trazendo as informações atuais dos produtos | Performs the query to the bank bringing the current information of the products
        /// </summary>
        /// <returns>Lista de Produtos</returns>
        [HttpGet("ListaOsProdutos")]
        public IActionResult ListaProdutos()
        {
            var produtoBanco = _context.Produtos.ToList();

            if(produtoBanco == null)
            {
                NotFound();
            }

            return Ok(produtoBanco);
        }

        /// <summary>
        /// Realiza a consulta ao banco trazendo um produto desejado | Performs a bank consultation bringing a desired product with more details
        /// </summary>
        /// <returns></returns>
        [HttpGet("RetornaUmProduto/{id}")]
        public IActionResult DetalhaUmProduto(int id)
        {
            var produtoBanco = _context.Produtos.Find(id);

            var regrasECalculos = new RegrasController();
            var (sucesso, resultado) = regrasECalculos.CalculaPrecoEQuantidade(produtoBanco);

            if (!sucesso)
            {
                return NotFound();
            }

            return Ok(resultado);
        }

        /// <summary>
        /// Realiza o PUT atualizando os dados do banco | Performs the PUT updating the database data
        /// </summary>
        /// <returns></returns>
        [HttpPut("AtualizaDadosProduto/{id}")]
        public IActionResult AtualizaDadosProduto(int id, Produto produtos)
        {
            var produtoBanco = _context.Produtos.Find(id);

            var regrasECalculos = new RegrasController();
            regrasECalculos.ValidaPreco(produtoBanco);

            if (produtoBanco == null) 
            { 
                return NotFound();
            }

            produtoBanco.Nome = produtos.Nome;
            produtoBanco.Preco = produtos.Preco;
            produtoBanco.QuantidadeEmEstoque = produtos.QuantidadeEmEstoque;
            produtoBanco.DataDeCriacao = produtos.DataDeCriacao;

            _context.Produtos.Update(produtoBanco);
            _context.SaveChanges();

            return Ok(produtoBanco);
        }

        /// <summary>
        /// Deleta o produto do banco de dados | Delete the product from the database
        /// </summary>
        /// <returns></returns>
        [HttpDelete("ApagaProduto/{id}")]
        public IActionResult ApagaProduto(int id)
        {
            var produtoBanco = _context.Produtos.Find(id);

            if (produtoBanco == null)
                return NotFound();

            _context.Produtos.Remove(produtoBanco);
            _context.SaveChanges();

            return NoContent();
        }

    }
}
