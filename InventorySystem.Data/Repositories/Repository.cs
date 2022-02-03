using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InventorySystem.Core.Repositories;

namespace InventorySystem.Data.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public readonly DbContext Context;

        public Repository(DbContext context)
        {
            this.Context = context;
        }

        public abstract IRepository<TEntity> IncludeAll();

        public TEntity Add(TEntity entity)
        {
            Context.Set<TEntity>().AddAsync(entity);
            return entity;
        }

        public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRangeAsync(entities);
            return entities;
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = Context.Set<TEntity>().Where(predicate).ToList();
            //Context.ChangeTracker.Clear();
            return entities;
        }

        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }


        // This doesn't include entities inside collection entity, example: List<Product> Products, you cannot include Products.Something
        // Use it to include non-collection types and collection itself, without going deeper
        // Format: Entity.Include(x => x.Type, x => x.AnotherType)
        public IRepository<TEntity> Include(params Expression<Func<TEntity, object>>[] childrens)
        {
            childrens.ToList().ForEach(x => Context.Set<TEntity>().Include(x).Load());
            return this;
        }


        // This one includes everything :)
        // Format: Entity.Include(new[] {"Type", "AnotherType", "List.Type"})
        public IRepository<TEntity> Include(string[] includes)
        {
            var query = Context.Set<TEntity>().AsQueryable();
            foreach (var include in includes)
                query.Include(include).Load();
            return this;
        }

        public IEnumerable<TEntity> GetAll()
        {
            var entities = Context.Set<TEntity>().ToList();
            //Context.ChangeTracker.Clear();
            return entities;
        }

        public TEntity GetById(int id)
        {
            var entity = Context.Set<TEntity>().Find(id);
            //Context.ChangeTracker.Clear();
            return entity;
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            var entity = Context.Set<TEntity>().SingleOrDefault(predicate);
            //Context.ChangeTracker.Clear();
            return entity;
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            var entity = Context.Set<TEntity>().FirstOrDefault(predicate);
            //Context.ChangeTracker.Clear();
            return entity;
        }
    }
}