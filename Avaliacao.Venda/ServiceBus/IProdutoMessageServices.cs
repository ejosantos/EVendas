using System.Threading.Tasks;

namespace Avaliacao.EVenda.ServiceBus
{
    public interface IProdutoMessageServices
    {
        void RegisterOnMessageHandlerAndReceiveMessagesProdutoCriado();
        void RegisterOnMessageHandlerAndReceiveMessagesProdutoAtualizado();
        Task CloseQueueAsync();
    }
}
