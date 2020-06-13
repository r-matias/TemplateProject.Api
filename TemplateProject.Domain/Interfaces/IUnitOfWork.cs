using TemplateProject.Entities.Model;
using TemplateProject.Entities.Model.Base;
using System;

namespace TemplateProject.Domain.Interfaces
{
    public interface IUnitOfWork<TEntity, TId> where TEntity : BaseEntity<TId>
    {
        IBaseRepository<TEntity, TId> Repository { get; }
        IBaseRepository<User, Guid> User { get; }
    }
}
