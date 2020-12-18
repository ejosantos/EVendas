using Avaliacao.EVenda.Dominio.Servicos;
using Avaliacao.EVenda.Helpes;
using Avaliacao.EVenda.ViewModel;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Avaliacao.EVenda.ServiceBus
{
    public class ProdutoVendidoServiceBus : IProdutoVendidoServiceBus
    {
        private string _endpointServiceBus;

        public ProdutoVendidoServiceBus(IConfiguration _configuration)
        {
            _endpointServiceBus = _configuration.GetConnectionString("EndpointServiceBusConnection");
        }

        public async Task EnviarMensagemProdutoVendido(int idProduto, int quantidade)
        {
            var serviceBusTopicClient = new TopicClient(_endpointServiceBus, "produtovendido");
            var produtoVendido = new VendaRequestViewModel { Id = idProduto, Quantidade = quantidade };
         
            var message = new Message(produtoVendido.ToJsonBytes())
            {
                ContentType = "application/json"
            };

            await serviceBusTopicClient.SendAsync(message);
        }
    }
}
