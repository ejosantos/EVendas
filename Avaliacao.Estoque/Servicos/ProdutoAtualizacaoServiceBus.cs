using Avaliacao.Estoque.Helpers;
using Avaliacao.Estoque.ViewModel;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Avaliacao.Estoque.Servicos
{
    public class ProdutoAtualizacaoServiceBus : IProdutoAtualizacaoServiceBus
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        private string _endpointServiceBus;
        private SubscriptionClient _serviceBusClientProdutoVenda;

        public ProdutoAtualizacaoServiceBus(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            _configuration = configuration;
            Inicializar();
        }

        private void Inicializar()
        {
            _endpointServiceBus = _configuration.GetConnectionString("EndpointServiceBusConnection");
            _serviceBusClientProdutoVenda = new SubscriptionClient(_endpointServiceBus, "produtovendido", "produtovendidosubscricao");
        }

        public void RegisterOnMessageHandlerAndReceiveMessagesProdutoVenda()
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            _serviceBusClientProdutoVenda.RegisterMessageHandler(ProcessMessageProdutoVendaAsync, messageHandlerOptions);
        }

        private async Task ProcessMessageProdutoVendaAsync(Message message, CancellationToken arg2)
        {
            var produtoVendaEnviado = message.Body.ParseJson<ProdutoAtualizacaoViewModel>();

            var scope = _serviceProvider.CreateScope();
            var produtoService = scope.ServiceProvider.GetRequiredService<IProdutoService>();

            await produtoService.AtualizarProdutoVendido(produtoVendaEnviado.Id, produtoVendaEnviado.Quantidade);
        }

        private static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs arg)
        {
            //throw new NotImplementedException();
            return null;
        }

    }
}
