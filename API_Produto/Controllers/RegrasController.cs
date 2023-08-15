using API_Produto.Context;
using API_Produto.Entities;
using API_Produto.Entities.EntitiesMongoDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace API_Produto.Controllers
{
    public class RegrasController
    {
        /// <summary>
        /// Realiza a validação para impedir que o usuário cadastre um valor menor do que zero | Validates to prevent the user from registering a value less than zero.
        /// </summary>
        /// <param name="produto"></param>
        /// <returns>bool sucess, object result</returns>
        /// <exception cref="Exception"></exception>
        public (bool sucess, object result) ValidaPreco(Produto produto)
        {
            if (produto.Preco <= 0)
            {
                throw new Exception($"Não é possível adicionar um novo produto em que o valor seja menor do que 0. Por favor, tente novamente.");
            }

            return (true, produto);
        }

        /// <summary>
        /// Realiza a multiplicação entre o preço do produto e quantidade no estoque |  Performs the multiplication between the price of the product and quantity in stock
        /// </summary>
        /// <param name="produto"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public (bool sucesso, object resultado) CalculaPrecoEQuantidade(Produto produto)
        {
            if (produto == null)
            {
                throw new ArgumentException("Dado não encontrado");
            }

            var calculoProduto = produto.QuantidadeEmEstoque * produto.Preco;

            var retornaValores = new
            {
                produto,
                ValorTotal = $"${calculoProduto:N2}"
            };

            return (true, retornaValores);
        }
    }
}
