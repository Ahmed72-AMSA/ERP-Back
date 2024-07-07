using erp_back.Models;

namespace erp_back.Data{
    public interface IDataRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<bool> Save();
        DatabaseContext GetContext(); 
        Task<T> GetByIdsAsync(params object[] keyValues);
    }

    }


