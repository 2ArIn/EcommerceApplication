using EcommerceApplication.Models;
using System.Linq.Expressions;

namespace EcommerceApplication.Data.Base
{
    public interface IEntityBaseRepository<T> where T: class,IEntityBase , new()
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAll(params string[] includeProperties);
        Task<T> GetById(int id);
        Task Add(T entity);
        Task UpdateAsync(int id,T entity);
        Task Delete(int id);
    }
}
