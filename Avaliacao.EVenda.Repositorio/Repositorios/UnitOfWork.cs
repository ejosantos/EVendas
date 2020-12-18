using Avaliacao.EVenda.Dominio.Servicos;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Avaliacao.EVenda.Repositorio.Repositorios
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EVendaContext _Context;

        public UnitOfWork(EVendaContext context) { _Context = context; }

        public Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            return _Context.SaveChangesAsync();
        }
        public Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            _Context.ChangeTracker.Entries()
                .Where(e => e.Entity != null).ToList()
                .ForEach(e => e.State = EntityState.Detached);

            return Task.CompletedTask;
        }
    }
}
