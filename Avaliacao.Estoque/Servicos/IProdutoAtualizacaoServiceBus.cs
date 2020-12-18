namespace Avaliacao.Estoque.Servicos
{
    public interface IProdutoAtualizacaoServiceBus
    {
        void RegisterOnMessageHandlerAndReceiveMessagesProdutoVenda();
    }
}