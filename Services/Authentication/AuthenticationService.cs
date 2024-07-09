using erp_back.Models;
using erp_back.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace erp_back.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IDataRepository<Authentication> _authRepository;
        private readonly DatabaseContext _context;
        private readonly ILogger<AuthenticationService> _logger;

        public AuthenticationService(
            IDataRepository<Authentication> authRepository, 
            DatabaseContext context, 
            ILogger<AuthenticationService> logger)
        {
            _authRepository = authRepository;
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Authentication>> GetAllAsync()
        {
            try
            {
                return await _authRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all authentications.");
                throw new ServiceException("An error occurred while retrieving all authentications.", ex);
            }
        }

        public async Task<Authentication> GetByIdAsync(int id)
        {
            try
            {
                return await _authRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving the authentication with ID {id}.");
                throw new ServiceException($"An error occurred while retrieving the authentication with ID {id}.", ex);
            }
        }

        public async Task<ServiceResponse> AddAsync(Authentication authentication)
        {
            var response = new ServiceResponse();

            try
            {
                // Validate email
                if (await EmailExistsAsync(authentication.Email))
                {
                    response.Success = false;
                    response.Message = "Email already exists.";
                    return response;
                }

                // Validate company name
                if (await CompanyNameExistsAsync(authentication.CompanyName))
                {
                    response.Success = false;
                    response.Message = "Company Name already exists.";
                    return response;
                }

                // Validate password
                if (!Validations.IsPasswordValid(authentication.Password))
                {
                    response.Success = false;
                    response.Message = "Password must contain at least 8 characters with one special character, one lowercase letter, and one uppercase letter.";
                    return response;
                }

                // Handle file upload
                if (authentication.File != null)
                {
                    string filePath = await FileSaver.SaveFileAsync(authentication.File);
                    authentication.FilePath = filePath;
                }

                // Add authentication to the repository
                await _authRepository.AddAsync(authentication);

                response.Success = true;
                response.Message = "Authentication added successfully.";
                response.Data = authentication;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "An error occurred while adding the authentication.");
                response.Success = false;
                response.Message = "An error occurred while adding the authentication.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while adding the authentication.");
                response.Success = false;
                response.Message = "An unexpected error occurred while adding the authentication.";
            }

            return response;
        }

        public async Task UpdateAsync(Authentication authentication)
        {
            try
            {
                if (await EmailExistsAsync(authentication.Email, authentication.ID))
                {
                    throw new ServiceException("Email already exists.", null);
                }

                if (await CompanyNameExistsAsync(authentication.CompanyName, authentication.ID))
                {
                    throw new ServiceException("Company Name already exists.", null);
                }

                await _authRepository.UpdateAsync(authentication);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "A concurrency error occurred while updating the authentication.");
                throw new ServiceException("A concurrency error occurred while updating the authentication.", ex);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "An error occurred while updating the authentication.");
                throw new ServiceException("An error occurred while updating the authentication.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while updating the authentication.");
                throw new ServiceException("An unexpected error occurred while updating the authentication.", ex);
            }
        }

        public async Task DeleteAsync(Authentication authentication)
        {
            try
            {
                await _authRepository.DeleteAsync(authentication);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the authentication.");
                throw new ServiceException("An error occurred while deleting the authentication.", ex);
            }
        }

        private async Task<bool> EmailExistsAsync(string email, int? ignoreId = null)
        {
            return await _context.Set<Authentication>()
                .AnyAsync(a => a.Email == email && (!ignoreId.HasValue || a.ID != ignoreId.Value));
        }

        private async Task<bool> CompanyNameExistsAsync(string companyName, int? ignoreId = null)
        {
            return await _context.Set<Authentication>()
                .AnyAsync(a => a.CompanyName == companyName && (!ignoreId.HasValue || a.ID != ignoreId.Value));
        }







    }
}
