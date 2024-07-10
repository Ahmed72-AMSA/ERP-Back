using AutoMapper;
using erp_back.Models;
using erp_back.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace erp_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationsController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;


        public AuthenticationsController(IAuthenticationService authenticationService,IMapper mapper)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
            
        }

        // GET: api/Authentications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Authentication>>> GetAuthentications()
        {
            try
            {
                var authentications = await _authenticationService.GetAllAsync();
                return Ok(authentications);
            }
            catch (ServiceException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET: api/Authentications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Authentication>> GetAuthentication(int id)
        {
            try
            {
                var authentication = await _authenticationService.GetByIdAsync(id);

                if (authentication == null)
                {
                    return NotFound();
                }

                return Ok(authentication);
            }
            catch (ServiceException ex)
            {
                return StatusCode(500, ex.Message);
            }
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
                return NoContent();
            }
            catch (ServiceException ex)
            {
                if (ex.InnerException is DbUpdateConcurrencyException && await _authenticationService.GetByIdAsync(id) == null)
                {
                    return NotFound();
                }
                return StatusCode(500, ex.Message);
            }
        }

        // POST: api/Authentications
        [HttpPost]
        public async Task<ActionResult<Authentication>> PostAuthentication(Authentication authentication)
        {
            try
            {
                var response = await _authenticationService.AddAsync(authentication);
                if (!response.Success)
                {
                    return BadRequest(response.Message);
                }
                return CreatedAtAction("GetAuthentication", new { id = authentication.ID }, authentication);
            }
            catch (ServiceException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE: api/Authentications/5
        [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthentication(int id)
        {
            try
            {
                var authentication = await _authenticationService.GetByIdAsync(id);
                if (authentication == null)
                {
                    return NotFound();
                }

                await _authenticationService.DeleteAsync(authentication);
                return NoContent();
            }
            catch (ServiceException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    
    

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var response = await _authenticationService.LoginAsync(loginDto);
        
        if (!response.Success)
        {
            return BadRequest(response.Message);
        }

        return Ok(response);
    }


    
    
    
    }




}


