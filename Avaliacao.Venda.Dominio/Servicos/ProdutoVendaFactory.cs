using Avaliacao.EVenda.Dominio.Repositorios;
using System.Threading.Tasks;

namespace Avaliacao.EVenda.Dominio.Servicos
{
    public class ProdutoVendaFactory : IProdutoVendaFactory
    {
        private readonly IProdutoRepositorio _produtoRepositorio;

        public ProdutoVendaFactory(IProdutoRepositorio produtoRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
        }

        public async Task<ProdutoVenda> Criar(int idProduto, int quantidade)
        {
            var produto = await _produtoRepositorio.GetAsync(idProduto);

            return new ProdutoVenda(produto, quantidade);
        }
    }
}
