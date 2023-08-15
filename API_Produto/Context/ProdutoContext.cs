using API_Produto.Entities;
using Microsoft.EntityFrameworkCore;

namespace API_Produto.Context
{
    /// <summary>
    /// Contexto do Banco de Dados onde definimos seus acessos com o Entity Framework | Database context where we define its accesses with the Entity Framework.
    /// </summary>
    public class ProdutoContext : DbContext
    {
        public ProdutoContext(DbContextOptions<ProdutoContext> options) : base(options) 
        {
            
        }

        public DbSet<Produto> Produtos { get; set; }
    }
}
