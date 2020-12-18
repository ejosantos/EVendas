using Avaliacao.Estoque.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Avaliacao.Estoque.Infra.Contexto
{
    public class EstoqueContext : DbContext
    {
        public EstoqueContext(DbContextOptions<EstoqueContext> options)
           : base(options)
        {
        }

        public DbSet<Produto> Produtos { get; set; }
    }
}
