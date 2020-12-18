
using Avaliacao.EVenda.Repositorio.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Avaliacao.EVenda.Repositorio.Repositorios
{
    public class EVendaContext : DbContext
    {
        public EVendaContext(DbContextOptions<EVendaContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProdutoConfiguration());
            builder.ApplyConfiguration(new VendaConfiguration());
            builder.ApplyConfiguration(new ProdutoVendaConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
