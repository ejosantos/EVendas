using Avaliacao.EVenda.Dominio;
using Avaliacao.EVenda.Dominio.Repositorios;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avaliacao.EVenda.Repositorio.Repositorios
{
    public class ProdutoRepositorio : BaseRepositorio<Produto>, IProdutoRepositorio
    {
        public ProdutoRepositorio(EVendaContext ctx) : base(ctx)
        {
        }

        public async Task<IEnumerable<Produto>> GetAllParaVenda()
            => await Task.FromResult<IEnumerable<Produto>>(_DbSet.AsNoTracking().Where(p => p.Quantidade > 0));
    }
}
