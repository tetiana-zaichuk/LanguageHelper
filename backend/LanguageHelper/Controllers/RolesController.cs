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
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService) => _roleService = roleService;

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDto>>> Get()
        {
            var result = await _roleService.GetAllEntitiesAsync();
            if (!result.Any())
                return NoContent();
            return  Ok(result);
        }

        // GET: api/Roles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleDto>> Get(int id)
        {
            var result = await _roleService.GetEntityByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        // POST: api/Roles
        [HttpPost]
        public async Task<ActionResult<RoleDto>> Post([FromBody] RoleDto value)
        {
            if (!_roleService.EntityValidation(value))
                return BadRequest("Please fill out title field.");
            var result = await _roleService.CreateEntityAsync(value);
            if (result == null)
                return StatusCode(500);
            return Ok(result);
        }

        // PUT: api/Roles/5
        [HttpPut("{id}")]
        public async Task<ActionResult<RoleDto>> Put(int id, [FromBody] RoleDto value)
        {
            if (!_roleService.EntityValidation(value))
                return BadRequest("Please fill out title field.");
            var result = await _roleService.UpdateEntityByIdAsync(value, id);
            return !result ? StatusCode(500) : Ok();
        }

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _roleService.DeleteEntityByIdAsync(id);
            return NoContent();
        }
    }
}
