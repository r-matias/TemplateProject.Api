using TemplateProject.Domain.Entities.Model;
using TemplateProject.Domain.Entities.Model.Base;
using System;

namespace TemplateProject.Domain.Interfaces
{
    public interface IUnitOfWork<TEntity, TId> where TEntity : BaseEntity<TId>
    {
        IBaseRepository<TEntity, TId> Repository { get; }
        IBaseRepository<User, Guid> User { get; }
    }
}
