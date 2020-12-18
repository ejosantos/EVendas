using System.Collections.Generic;
using System.Threading.Tasks;

namespace Avaliacao.EVenda.Dominio.Servicos
{
    public interface IProdutoService
    {
        Task ProcessarAtualizacao(Produto produtoEnviado);
        Task ProcessarCriacao(Produto produto);
        Task<IEnumerable<Produto>> GetAllParaVenda();
    }
}