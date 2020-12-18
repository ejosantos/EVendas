using System.Threading.Tasks;

namespace Avaliacao.EVenda.Dominio.Servicos
{
    public interface IProdutoVendidoServiceBus
    {
        Task EnviarMensagemProdutoVendido(int idProduto, int quantidade);
    }
}
