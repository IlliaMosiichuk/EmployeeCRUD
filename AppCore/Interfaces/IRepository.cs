using AppCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        void Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);

        void Update(TEntity entity);

        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null);

        TEntity GetById(Guid id);
    }
}
