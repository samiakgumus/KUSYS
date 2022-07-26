using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        T Get(Expression<Func<T, bool>> filter);
      
        IList<T> GetList(Expression<Func<T, bool>> filter = null, Expression<Func<T, int>> orderby = null, string orderByType = "asc");
        int Add(T entity);
        int Update(T entity);
        void Delete(T entity);
        void DeleteRange(IList<T> entity);

    }
}
