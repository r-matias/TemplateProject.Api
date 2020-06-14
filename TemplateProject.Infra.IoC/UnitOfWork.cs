using TemplateProject.Domain.Interfaces;
using TemplateProject.Domain.Entities.Model;
using TemplateProject.Domain.Entities.Model.Base;
using TemplateProject.Infra.Data.Context;
using TemplateProject.Infra.Data.Repository;
using System;

namespace TemplateProject.Infra.IoC
{
    public class UnitOfWork<TEntity, TId> : IUnitOfWork<TEntity, TId>
        where TEntity : BaseEntity<TId>
    {
        private readonly SqlContext _sqlContext;

        private BaseRepository<TEntity, TId> _baseRepository;
        private BaseRepository<User, Guid> _userRepository;

        public UnitOfWork(SqlContext sqlContext)
        {
            _sqlContext = sqlContext;
        }

        public IBaseRepository<User, Guid> User 
        { 
            get
            {
                if(_userRepository == null)
                    _userRepository = new BaseRepository<User, Guid>(_sqlContext);

                return _userRepository;
            }
        }

        public IBaseRepository<TEntity, TId> Repository
        {
            get
            {
                if (_baseRepository == null)
                    _baseRepository = new BaseRepository<TEntity, TId>(_sqlContext);

                return _baseRepository;
            }
        }
    }
}
