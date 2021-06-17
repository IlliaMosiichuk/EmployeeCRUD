using AppCore.Entities;
using AppCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly AppDbContext _context;

        protected DbSet<TEntity> _set
        {
            get { return _context.Set<TEntity>(); }
        }

        public EfRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(TEntity entity)
        {
            _set.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _set.AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            _set.Remove(entity);
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
                return _set;

            return _set.Where(predicate);
        }

        public TEntity GetById(Guid id)
        {
            return _set.Find(id);
        }
    }
}
