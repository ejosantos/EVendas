using System.Threading.Tasks;

namespace Avaliacao.EVenda.Dominio.Servicos
{
    public interface IVendaService
    {
        Task<Venda> RealizarVenda(ProdutoVenda produto);
    }
}