using Avaliacao.EVenda.Dominio;
using Avaliacao.EVenda.Dominio.Servicos;
using Avaliacao.EVenda.Helpes;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Avaliacao.EVenda.ServiceBus
{
    public class ProdutoMessageServices : IProdutoMessageServices
    {
        private readonly string _endpointServiceBus;
        private IProdutoService _produtoService;
        private readonly SubscriptionClient _serviceBusClientProdutoCriado;
        private readonly SubscriptionClient _serviceBusClientProdutoAtualizado;
        private readonly IServiceProvider _serviceProvider;

        public ProdutoMessageServices(IServiceProvider serviceProvider, IConfiguration _configuration)
        {
            _endpointServiceBus = _configuration.GetConnectionString("EndpointServiceBusConnection");

            _serviceBusClientProdutoCriado = new SubscriptionClient(_endpointServiceBus, "produtocriado", "produtocriadosubscricao");
            _serviceBusClientProdutoAtualizado = new SubscriptionClient(_endpointServiceBus, "produtoatualizado", "produtoatualizadosubscricao");
            _serviceProvider = serviceProvider;
        }

        public void RegisterOnMessageHandlerAndReceiveMessagesProdutoCriado()
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            _serviceBusClientProdutoCriado.RegisterMessageHandler(ProcessMessageProdutoCriadoAsync, messageHandlerOptions);
        }

        public void RegisterOnMessageHandlerAndReceiveMessagesProdutoAtualizado()
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            _serviceBusClientProdutoAtualizado.RegisterMessageHandler(ProcessMessageProdutoAtualizadoAsync, messageHandlerOptions);
        }

        private async Task ProcessMessageProdutoAtualizadoAsync(Message message, CancellationToken arg2)
        {
            var produtoEnviado = message.Body.ParseJson<Produto>();
            var scope = _serviceProvider.CreateScope();
            _produtoService = scope.ServiceProvider.GetService<IProdutoService>();
            await _produtoService.ProcessarAtualizacao(produtoEnviado);
            await _serviceBusClientProdutoAtualizado.CompleteAsync(message.SystemProperties.LockToken);
        }

        private async Task ProcessMessageProdutoCriadoAsync(Message message, CancellationToken arg2)
        {
            var produto = message.Body.ParseJson<Produto>();
            var scope = _serviceProvider.CreateScope();
            _produtoService = scope.ServiceProvider.GetService<IProdutoService>();
            await _produtoService.ProcessarCriacao(produto);
            await _serviceBusClientProdutoCriado.CompleteAsync(message.SystemProperties.LockToken);
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs arg)
        {
            var scope = _serviceProvider.CreateScope();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger>();

            logger.LogError(arg.Exception, "Message handler encountered an exception");

            return Task.CompletedTask;
        }

        public async Task CloseQueueAsync()
        {
            await _serviceBusClientProdutoAtualizado.CloseAsync();
            await _serviceBusClientProdutoCriado.CloseAsync();
        }
    }
}
