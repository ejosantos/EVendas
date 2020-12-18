using Avaliacao.Estoque.Entidades;
using Avaliacao.Estoque.Helpers;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Avaliacao.Estoque.Servicos
{
    public class ProdutoServiceBus : IProdutoServiceBus
    {
        private readonly IConfiguration _configuration;
        private readonly string _endpointServiceBus;

        public ProdutoServiceBus(IConfiguration configuration)
        {
            _configuration = configuration;
            _endpointServiceBus = _configuration.GetConnectionString("EndpointServiceBusConnection");
        }
      
        public async Task EnviarProdutoMessage(Produto produto)
        {
            var serviceBusTopicClient = new TopicClient(_endpointServiceBus, "produtocriado");

            var message = new Message(produto.ToJsonBytes())
            {
                ContentType = "application/json"
            };

            await serviceBusTopicClient.SendAsync(message);
        }

        public async Task EnviarMensagemAtualizacaoProduto(Produto produto)
        {
            var serviceBusTopicClient = new TopicClient(_endpointServiceBus, "produtoatualizado");

            var message = new Message(produto.ToJsonBytes())
            {
                ContentType = "application/json"
            };

            await serviceBusTopicClient.SendAsync(message);
        }
    }
}
