using Avaliacao.EVenda.Dominio;
using Avaliacao.EVenda.Dominio.Repositorios;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Avaliacao.EVenda.Repositorio.Repositorios
{
    public class BaseRepositorio<TEntity> : IRepositorio<TEntity> where TEntity : class
    {
        protected readonly EVendaContext _Context;
        protected readonly DbSet<TEntity> _DbSet;

        public BaseRepositorio(EVendaContext context)
        {
            _Context = context;
            _DbSet = _Context.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity obj)
        {
            await _DbSet.AddAsync(obj);
            return obj;
        }

        public async Task<TEntity> DeleteAsync(int id)
        {
            var obj = await _DbSet.FindAsync(id);

            if (obj == null) return null;

            _DbSet.Remove(obj);
            return obj;
        }

        public async Task<TEntity> GetAsync(int id) => await _DbSet.FindAsync(id);

        public async Task<IEnumerable<TEntity>> GetAllAsync() => await Task.FromResult<IEnumerable<TEntity>>(_DbSet.AsNoTracking());

        public async Task<TEntity> UpdateAsync(TEntity obj)
        {
            await Task.Run(() =>
            {
                _Context.Entry(obj).State = EntityState.Modified;
            });
            return obj;

        }
    }
}
