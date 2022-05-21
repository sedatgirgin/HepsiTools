using HepsiTools.GenericRepositories.Helper;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HepsiTools.GenericRepositories.Abstract
{
    public interface IGenericRepository<T> where T : class, new()
    {
        #region Standar CRUD işlemleri için metoto işlemleri
        public List<T> GetList();
        public List<T> GetList(Expression<Func<T, bool>> filter);
        public IEnumerable<T> GetList(Func<T, object> orderByFilter, Sorted _sort = Sorted.ASC);

        public IEnumerable<T> GetList(Func<T, object> orderByFilter, Sorted _sort = Sorted.ASC, int _count = 0, int _skip = 0);
        public IEnumerable<T> GetList(Expression<Func<T, bool>> filter, Func<T, object> orderByFilter, Sorted _sort = Sorted.ASC, int _skip = 0, int _count = 0);

        public IEnumerable<T> GetAll();
        public T Get(int id);
        public T Get(Expression<Func<T, bool>> filter);
        public T Insert(T entity);
        public T Update(T entity);

        public T Delete(object id);
        public T Delete(T entity);
        #endregion BULK Işlemleri Metot imzaları işlemleri

        #region BULK Işlemleri Metot imzaları işlemleri

        public void BulkInsert(IEnumerable<T> bulkInsetData);

        public void BulkUpdate(IEnumerable<T> bulkUpdateData);

        public void BulkDelete(IEnumerable<T> bulkDeleteData);
        #endregion BULK Işlemleri Metot imzaları işlemleri

        #region (Asenkron) Standar CRUD işlemleri için metot işlemleri
        public Task<List<T>> GetListAsync(Expression<Func<T, bool>> filter = null);
        public Task<T> GetAsync(Expression<Func<T, bool>> filter);
        public Task InsertAsync(T entity);
        public Task UpdateAsync(T entity);

        #endregion (Asenkron) Standar CRUD işlemleri için metot işlemleri

    }
}
