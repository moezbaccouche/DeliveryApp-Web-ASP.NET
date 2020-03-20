using DeliveryApp.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PFEGestionConges.Data.Repo
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> Table { get; }
        IQueryable<T> TableNoTracking { get; }

        bool Delete(IList<T> entities, bool saveChange = true);
        bool Delete(T entity, bool saveChange = true);
        IQueryable<T> FindMany(Func<T, bool> expression, ICollection<string> includes = null);
        IList<T> GetAll();
        IQueryable GetAllQueryable();
        T GetById(int id);
        IList<T> Insert(IList<T> entities, bool saveChange = true);
        T Insert(T entity, bool saveChange = true);
        bool IsExists(Func<T, bool> expression);
        void SaveChanges();
        IList<T> Update(IList<T> entities, bool saveChange = true);
        T Update(T entity, bool saveChange = true);
    }
}
