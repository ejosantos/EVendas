using Avaliacao.EVenda.Dominio;
using Avaliacao.EVenda.Dominio.Repositorios;

namespace Avaliacao.EVenda.Repositorio.Repositorios
{
    public class VendaRepositorio : BaseRepositorio<Venda>, IVendaRepositorio
    {
        public VendaRepositorio(EVendaContext ctx) : base(ctx)
        {
        }
    }
}
