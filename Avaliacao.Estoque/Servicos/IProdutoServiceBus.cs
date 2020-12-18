using Avaliacao.Estoque.Entidades;
using System.Threading.Tasks;

namespace Avaliacao.Estoque.Servicos
{
    public interface IProdutoServiceBus
    {
        Task EnviarProdutoMessage(Produto produto);

        Task EnviarMensagemAtualizacaoProduto(Produto produto);
    }
}