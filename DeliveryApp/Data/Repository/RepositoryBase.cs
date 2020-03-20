using DeliveryApp.Data;
using DeliveryApp.Models.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFEGestionConges.Data.Repo
{
    public class RepositoryBase<T> : IRepository<T> where T : BaseEntity
    {

        private ApplicationDbContext context;
        private DbSet<T> entities;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="context">Object context</param>
        public RepositoryBase(ApplicationDbContext context)
        {
            this.context = context;
        }

        public virtual T GetById(int id)
        {
            return this.Entities.SingleOrDefault(x => x.Id == id);
        }

        public virtual T Insert(T entity, bool saveChange = true)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                var result = this.Entities.Add(entity);

                if (saveChange)
                    this.context.SaveChanges();
                return result.Entity;
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }

        public virtual T Update(T entity, bool saveChange = true)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }


                this.context.Set<T>().Attach(entity);
                this.context.Entry<T>(entity).State = EntityState.Modified;

                if (saveChange)
                    this.context.SaveChanges();
                return entity;
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }

        public virtual bool Delete(T entity, bool saveChange = true)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                this.Entities.Remove(entity);

                if (saveChange)
                    this.context.SaveChanges();
                return true;
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }

        public virtual IList<T> Insert(IList<T> entities, bool saveChange = true)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException("entity");

                this.Entities.AddRange(entities);

                if (saveChange)
                    this.context.SaveChanges();

                return entities.ToList();
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }

        public virtual IList<T> Update(IList<T> entities, bool saveChange = true)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException("entity");

                if (saveChange)
                    this.context.SaveChanges();
                return entities;
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }

        public virtual bool Delete(IList<T> entities, bool saveChange = true)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException("entity");

                this.Entities.RemoveRange(entities);
                if (saveChange)
                    this.context.SaveChanges();
                return true;
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }

        public virtual IQueryable<T> Table
        {
            get
            {
                return this.Entities;
            }
        }

        protected virtual DbSet<T> Entities
        {
            get
            {
                if (entities == null)
                    entities = context.Set<T>();
                return entities;
            }
        }

        public IQueryable<T> TableNoTracking
        {
            get
            {
                return this.Entities.AsNoTracking();
            }
        }

        public IList<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        public IQueryable GetAllQueryable()
        {
            return context.Set<T>().AsQueryable();
        }

        public bool IsExists(Func<T, bool> expression)
        {
            var query = context.Set<T>().AsQueryable().Where(expression);
            return query != null;
        }

        public IQueryable<T> FindMany(Func<T, bool> expression, ICollection<string> includes = null)
        {
            var query = context.Set<T>().AsQueryable();
            query = query.Where(expression).AsQueryable();
            return query;
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

    }
}


