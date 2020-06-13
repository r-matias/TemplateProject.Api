using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TemplateProject.Entities.Model.Base;

namespace TemplateProject.Domain.Interfaces
{
    public interface IBaseRepository<TEntity, TId> 
        where TEntity : BaseEntity<TId>
    {
        Task Insert(TEntity obj);

        Task Update(TEntity obj);

        Task Delete(TId id);

        Task<TEntity> Select(TId id);

        Task<IList<TEntity>> Select();

        Task<TEntity> FirstOrDefaultOnCondition(Expression<Func<TEntity, bool>> expression);

        Task<bool> AnyEntityOnCondition(Expression<Func<TEntity, bool>> expression);
    }
}
