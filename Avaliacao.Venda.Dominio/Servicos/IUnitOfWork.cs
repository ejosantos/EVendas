using System.Threading;
using System.Threading.Tasks;

namespace Avaliacao.EVenda.Dominio.Servicos
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
        Task RollbackAsync(CancellationToken cancellationToken = default);
    }
}

