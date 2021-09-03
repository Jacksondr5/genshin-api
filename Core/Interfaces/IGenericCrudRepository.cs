using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGenericCrudRepository<T> where T : StorableData
    {
        Task Create(T entity);
        Task Delete(T entity);
        Task<List<T>> GetAll();
        Task<T?> Get(int id);
        Task<int?> GetMaxId();
        Task Update(T entity);
    }
}