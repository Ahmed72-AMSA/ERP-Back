using erp_back.Models;
using Microsoft.EntityFrameworkCore;
namespace erp_back.Data


{
    public class DataRepository<T> : IDataRepository<T> where T : class
    {
        private readonly DatabaseContext _db;
        private readonly DbSet<T> _table;

        public DataRepository(DatabaseContext db)
        {
            _db = db;
            _table = _db.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _table.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _table.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _table.AddAsync(entity);
            await Save();
        }

        public async Task UpdateAsync(T entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            await Save();
        }

        public async Task DeleteAsync(T entity)
        {
            _table.Remove(entity);
            await Save();
        }

        public async Task<bool> Save()
        {
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<T> GetByIdsAsync(params object[] keyValues)
        {
            return await _table.FindAsync(keyValues);
        }

        public DatabaseContext GetContext()
        {
            return _db;
        }
    }
}
