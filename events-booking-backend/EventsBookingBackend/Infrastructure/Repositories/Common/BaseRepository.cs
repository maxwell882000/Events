using EventsBookingBackend.Domain.Common.Entities;
using EventsBookingBackend.Domain.Common.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventsBookingBackend.Domain.Common.Specifications;
using EventsBookingBackend.Infrastructure.Persistence.DbContexts;

namespace EventsBookingBackend.Infrastructure.Repositories.Common
{
    public class BaseRepository<TEntity, TContext>(TContext context) : IBaseRepository<TEntity>
        where TEntity : BaseEntity
        where TContext : DbContext
    {
        protected readonly DbSet<TEntity> DbSet = context.Set<TEntity>();

        public async Task<TEntity> Create(TEntity entity)
        {
            await DbSet.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            context.Attach(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(TEntity entity)
        {
            var rows = await DbSet.Where(e => e.Id == entity.Id)
                .ExecuteUpdateAsync(setPropertyCalls => setPropertyCalls.SetProperty(e => e.IsDeleted, true));
            return rows >= 1;
        }

        public async Task<List<TEntity>> FindAll(ISpecification<TEntity>? specification = null)
        {
            if (specification != null)
                return await specification.Apply(context.Set<TEntity>()).ToListAsync();
            return await DbSet.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity?> FindFirst(ISpecification<TEntity>? specification = null)
        {
            if (specification != null)
                return await specification.Apply(context.Set<TEntity>()).AsNoTracking().FirstOrDefaultAsync();
            return await DbSet.AsNoTracking().FirstOrDefaultAsync();
        }
    }
}