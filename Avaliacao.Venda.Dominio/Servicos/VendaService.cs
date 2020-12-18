using Avaliacao.EVenda.Dominio.Repositorios;
using System.Threading.Tasks;

namespace Avaliacao.EVenda.Dominio.Servicos
{
    public class VendaService : IVendaService
    {
        private readonly IVendaRepositorio _vendaRepositorio;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProdutoVendidoServiceBus _produtoVendidoServiceBus;

        public VendaService(IVendaRepositorio vendaRepositorio, IUnitOfWork unitOfWork, IProdutoVendidoServiceBus produtoVendidoServiceBus)
        {
            _vendaRepositorio = vendaRepositorio;
            _unitOfWork = unitOfWork;
            _produtoVendidoServiceBus = produtoVendidoServiceBus;
        }

        public async Task<Venda> RealizarVenda(ProdutoVenda produto)
        {
            var venda = await _vendaRepositorio.AddAsync(new Venda(produto));
            await _unitOfWork.CommitAsync();

            await _produtoVendidoServiceBus.EnviarMensagemProdutoVendido(produto.Produto.Id, produto.Quantidade);

            return venda;
        }
    }
}
