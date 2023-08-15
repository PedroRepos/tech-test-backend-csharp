using System.Reflection.Metadata.Ecma335;

namespace API_Produto.Entities
{
    /// <summary>
    /// Entidade que foi aplicada ao banco de dados SQL | Entity that was applied to the database
    /// </summary>
    public class Produto
    {
        public int Id { get; set; }
        public string Nome {get; set; }
        public double Preco { get; set; }
        public int QuantidadeEmEstoque { get; set; }
        public DateTime DataDeCriacao { get; set; }

        internal Stream OpenReadStream()
        {
            throw new NotImplementedException();
        }
    }   
}
