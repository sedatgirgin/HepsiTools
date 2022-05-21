using HepsiTools.GenericRepositories.Abstract;
using HepsiTools.GenericRepositories.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HepsiTools.GenericRepositories.Concrate
{
    public class GenericRepository<T> : RepositoryBase, IGenericRepository<T> where T : class, new()
    {

        DbSet<T> entities;
        public GenericRepository()
        {
            entities = _context.Set<T>();
        }

        #region Standar CRUD işlemleri için metoto işlemleri
        public List<T> GetList()
        {
            return entities.ToList();
        }

        public List<T> GetList(Expression<Func<T, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException("ListExpression", "filter is null");
            }
            return entities.Where(filter).ToList();
        }
        public IEnumerable<T> GetList(Func<T, object> orderByFilter, Sorted _sort = Sorted.ASC)
        {
            IEnumerable<T> query = _sort == Sorted.ASC ? entities.OrderBy(orderByFilter) : entities.OrderByDescending(orderByFilter);

            return query.ToList();
        }

        public IEnumerable<T> GetList(Func<T, object> orderByFilter, Sorted _sort = Sorted.ASC, int _count = 0, int _skip = 0)
        {
            IEnumerable<T> query = _sort == Sorted.ASC ? entities.OrderBy(orderByFilter).Skip(_skip).Take(_count) : entities.OrderByDescending(orderByFilter).Skip(_skip).Take(_count);

            return query.ToList();
        }
        public IEnumerable<T> GetList(Expression<Func<T, bool>> filter, Func<T, object> orderByFilter, Sorted _sort = Sorted.ASC, int _skip = 0, int _count = 0)
        {
            IEnumerable<T> query = _sort == Sorted.ASC ? entities.Where(filter).OrderBy(orderByFilter).Skip(_skip).Take(_count) : entities.OrderByDescending(orderByFilter).Skip(_skip).Take(_count);
            return query.ToList();
        }


        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }

        public T Get(int id)
        {
            return entities.Find(id);
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            return entities.Where(filter).FirstOrDefault();
        }

        public T Insert(T entity)
        {
            entities.Add(entity);
            Save();
            return entity;
        }

        public T Update(T entity)
        {
            entities.Update(entity);
            Save();
            return entity;
        }

        public virtual T Delete(object id)
        {
            return Delete(_context.Set<T>().Find(id));
        }

        public virtual T Delete(T entity)
        {
            entities.Remove(entity);
            _context.Entry<T>(entity).State = EntityState.Deleted;
            return entity;
        }
        #endregion BULK Işlemleri Metot imzaları işlemleri

        #region BULK Işlemleri Metot imzaları işlemleri

        public void BulkInsert(IEnumerable<T> bulkInsetData)
        {
            entities.AddRange(bulkInsetData);
            Save();
        }

        public void BulkUpdate(IEnumerable<T> bulkUpdateData)
        {
            entities.RemoveRange(bulkUpdateData);
            Save();
        }

        public void BulkDelete(IEnumerable<T> bulkDeleteData)
        {
            entities.RemoveRange(bulkDeleteData);
            Save();
        }

        #endregion BULK Işlemleri Metot imzaları işlemleri

        #region (Asenkron) Standar CRUD işlemleri için metot işlemleri
        public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> filter = null)
        {
            return entities.ToList();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
        {
            return entities.Where(filter).FirstOrDefault();
        }

        public async Task InsertAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            Save();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            Save();
        }

        #endregion (Asenkron) Standar CRUD işlemleri için metot işlemleri


        public IEnumerable<T> GetSql(string sql)
        {
            return entities.FromSqlRaw(sql).AsNoTracking();
        }

        public int ExecuteSqlCommand(string query, params object[] parameters)
        {
            return _context.Database.ExecuteSqlRaw(query, parameters);
        }

        public void Save()
        {
            if (_context.ChangeTracker.HasChanges())
                _context.SaveChanges();
            else
                _context.SaveChanges();
        }

    }

}
