using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using easyNetAPI.Data.Migrations;

namespace easyNetAPI.Data.Repository.IRepository
{
    public interface IRepository<T> : IDisposable where T : class
    {
        T? GetFirstOrDefault(Expression<Func<T, bool>> filter);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Remove(string id);
    }
}