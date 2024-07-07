using erp_back.Models;
using erp_back.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace erp_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationsController : ControllerBase
    {
        private readonly IDataRepository<Authentication> _authRepository;

        public AuthenticationsController(IDataRepository<Authentication> authRepository)
        {
            _authRepository = authRepository;
        }

        // GET: api/Authentications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Authentication>>> GetAuthentications()
        {
            return Ok(await _authRepository.GetAllAsync());
        }




        

        // GET: api/Authentications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Authentication>> GetAuthentication(int id)
        {
            var authentication = await _authRepository.GetByIdAsync(id);

            if (authentication == null)
            {
                return NotFound();
            }

            return authentication;
        }





        // PUT: api/Authentications/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthentication(int id, Authentication authentication)
        {
            if (id != authentication.ID)
            {
                return BadRequest();
            }

            try
            {
                await _authRepository.UpdateAsync(authentication);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _authRepository.GetByIdAsync(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }




        // POST: api/Authentications
        [HttpPost]
        public async Task<ActionResult<Authentication>> PostAuthentication(Authentication authentication)
        {
            await _authRepository.AddAsync(authentication);
            return CreatedAtAction("GetAuthentication", new { id = authentication.ID }, authentication);
        }





        // DELETE: api/Authentications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthentication(int id)
        {
            var authentication = await _authRepository.GetByIdAsync(id);
            if (authentication == null)
            {
                return NotFound();
            }

            await _authRepository.DeleteAsync(authentication);
            return NoContent();
        }
    }
}
