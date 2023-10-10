using SandBoxApp.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SandBoxApp.Application.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetOneAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<T> GetOneWithTrackingAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetManyWithTrackingAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<PagedResult<T>> Pagination(PaginationParameters<T> paginationParameters);
        Task Insert(T obj);
        void Update(T obj);
        void Delete(T obj);
    }
}
