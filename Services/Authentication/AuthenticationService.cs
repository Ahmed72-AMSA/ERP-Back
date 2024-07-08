using erp_back.Models;
using erp_back.Data;


namespace erp_back.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IDataRepository<Authentication> _authRepository;

        public AuthenticationService(IDataRepository<Authentication> authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<IEnumerable<Authentication>> GetAllAsync()
        {
            return await _authRepository.GetAllAsync();
        }

        public async Task<Authentication> GetByIdAsync(int id)
        {
            return await _authRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Authentication authentication)
        {
            await _authRepository.UpdateAsync(authentication);
        }

        public async Task AddAsync(Authentication authentication)
        {
            await _authRepository.AddAsync(authentication);
        }

        public async Task DeleteAsync(Authentication authentication)
        {
            await _authRepository.DeleteAsync(authentication);
        }
    }
}
