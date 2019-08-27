using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LanguageHelper.BusinessLayer.Interfaces;
using LanguageHelper.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace LanguageHelper.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService) => _userService = userService;

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> Get()
        {
            var result = await _userService.GetAllEntitiesAsync();
            if (!result.Any())
                return NoContent();
            return Ok(result);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> Get(int id)
        {
            var result = await _userService.GetEntityByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<UserDto>> Post([FromBody] UserDto value)
        {
            if (!_userService.EntityValidation(value))
                return BadRequest("Please fill out these fields: nickname, email, created at, last active and role id.");
            
            var result = await _userService.CreateEntityAsync(value);
            if (result == null)
                return StatusCode(500);
            return Ok(result);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<ActionResult<UserDto>> Put(int id, [FromBody] UserDto value)
        {
            if (!_userService.EntityValidation(value))
                return BadRequest("Please fill out these fields: nickname, email, created at, last active and role id.");

            var result = await _userService.UpdateEntityByIdAsync(value, id);
            return !result ? StatusCode(500) : Ok();
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _userService.DeleteEntityByIdAsync(id);
            return NoContent();
        }
    }
}
