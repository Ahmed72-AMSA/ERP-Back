using erp_back.Models;
using erp_back.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace erp_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationsController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationsController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        // GET: api/Authentications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Authentication>>> GetAuthentications()
        {
            var authentications = await _authenticationService.GetAllAsync();
            return Ok(authentications);
        }

        // GET: api/Authentications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Authentication>> GetAuthentication(int id)
        {
            var authentication = await _authenticationService.GetByIdAsync(id);

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
                await _authenticationService.UpdateAsync(authentication);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _authenticationService.GetByIdAsync(id) == null)
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
            await _authenticationService.AddAsync(authentication);
            return CreatedAtAction("GetAuthentication", new { id = authentication.ID }, authentication);
        }

        // DELETE: api/Authentications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthentication(int id)
        {
            var authentication = await _authenticationService.GetByIdAsync(id);
            if (authentication == null)
            {
                return NotFound();
            }

            await _authenticationService.DeleteAsync(authentication);
            return NoContent();
        }
    }
}
