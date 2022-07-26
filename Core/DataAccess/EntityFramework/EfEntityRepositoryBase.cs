using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
    where TEntity : class, IEntity, new()
    where TContext : DbContext, new()
    {
        public int Add(TEntity entity)
        {
            using (var context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                return context.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using (var context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public void DeleteRange(IList<TEntity> entity)
        {
            using (var context = new TContext())
            {
                context.RemoveRange(entity);
                context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var context = new TContext())
            {
                return context.Set<TEntity>().FirstOrDefault(filter);
            }
        }

        public IList<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, int>> orderby = null, string orderByType="asc")
        {
            using (var context = new TContext())
            {
                if (filter == null && orderby==null )
                {
                    return context.Set<TEntity>().ToList();
                }
                else
                {
                    if (filter != null && orderby==null )
                    {
                  
                         return context.Set<TEntity>().Where(filter).ToList();
                    }
                    else if( filter!=null && orderby!=null)
                    {
                        if (orderByType=="desc")
                        {
                            return context.Set<TEntity>().Where(filter).OrderByDescending(orderby).ToList();
                        }
                        else
                        {
                            return context.Set<TEntity>().Where(filter).OrderBy(orderby).ToList();
                        }
                    }
                    else
                    {
                        if (orderByType == "desc")
                        {
                            return context.Set<TEntity>().OrderByDescending(orderby).ToList();
                        }
                        else
                        {
                            return context.Set<TEntity>().OrderBy(orderby).ToList();
                        }
                    }
                }

              
            }
        }

        public int Update(TEntity entity)
        {
            try
            {
                using (var context = new TContext())
                {
                    var updatedEntity = context.Entry(entity);
                    updatedEntity.State = EntityState.Modified;
                    return context.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                return 0;
            }
        }
    }
}
