using System.Threading.Tasks;

namespace Avaliacao.EVenda.Dominio.Servicos
{
    public interface IProdutoVendaFactory
    {
        Task<ProdutoVenda> Criar(int idProduto, int quantidade);
    }
}