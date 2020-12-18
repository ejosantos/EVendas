using System.Collections.Generic;
using System.Threading.Tasks;

namespace Avaliacao.EVenda.Dominio.Repositorios
{
    public interface IProdutoRepositorio : IRepositorio<Produto>
    {
        Task<IEnumerable<Produto>> GetAllParaVenda();
    }
}
