using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TemplateProject.Domain.Interfaces;
using TemplateProject.Domain.Entities.Model.Base;
using TemplateProject.Infra.Data.Context;

namespace TemplateProject.Infra.Data.Repository
{
    public class BaseRepository<TEntity, TId> : IBaseRepository<TEntity, TId>
        where TEntity : BaseEntity<TId>
    {
        private readonly SqlContext _context;

        public BaseRepository(SqlContext context)
        {
            _context = context;
        }

        public async Task<TEntity> FirstOrDefaultOnCondition(Expression<Func<TEntity, bool>> expression)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
        }

        public async Task<bool> AnyEntityOnCondition(Expression<Func<TEntity, bool>> expression)
        {
            return await _context.Set<TEntity>().AnyAsync(expression);
        }

        public async Task Insert(TEntity obj)
        {
            _context.Set<TEntity>().Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(TId id)
        {
            _context.Set<TEntity>().Remove(await Select(id));
            await _context.SaveChangesAsync();
        }

        public async Task<TEntity> Select(TId id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IList<TEntity>> Select()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task Update(TEntity obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
