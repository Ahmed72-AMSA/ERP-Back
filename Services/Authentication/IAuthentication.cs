using erp_back.Models;

namespace erp_back.Services
{
    public interface IAuthenticationService
    {
        Task<IEnumerable<Authentication>> GetAllAsync();
        Task<Authentication> GetByIdAsync(int id);
        Task UpdateAsync(Authentication authentication);
        Task<ServiceResponse> AddAsync(Authentication authentication);
        Task DeleteAsync(Authentication authentication);
    }
}
