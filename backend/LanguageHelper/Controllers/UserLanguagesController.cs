using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LanguageHelper.BusinessLayer.Interfaces;
using LanguageHelper.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LanguageHelper.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class UserLanguagesController : ControllerBase
    {
        private readonly IUserLanguageService _userLanguageService;

        public UserLanguagesController(IUserLanguageService service) => _userLanguageService = service;

        // GET: api/UserLanguages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserLanguageDto>>> Get()
        {
            var result = await _userLanguageService.GetAllEntitiesAsync();
            if (!result.Any())
                return NoContent();
            return Ok(result);
        }

        // GET: api/UserLanguages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserLanguageDto>> Get(int id)
        {
            var result = await _userLanguageService.GetEntityByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        // POST: api/UserLanguages
        [HttpPost]
        public async Task<ActionResult<UserLanguageDto>> Post([FromBody] UserLanguageDto value)
        {
            if (!_userLanguageService.EntityValidation(value))
                return BadRequest("Please fill out these fields: language and user id.");

            var result = await _userLanguageService.CreateEntityAsync(value);
            if (result == null)
                return StatusCode(500);
            return Ok(result);
        }

        // PUT: api/UserLanguages/5
        [HttpPut("{id}")]
        public async Task<ActionResult<UserLanguageDto>> Put(int id, [FromBody] UserLanguageDto value)
        {
            if (!_userLanguageService.EntityValidation(value))
                return BadRequest("Please fill out these fields: language and user id.");

            var result = await _userLanguageService.UpdateEntityByIdAsync(value, id);
            return !result ? StatusCode(500) : Ok();
        }

        // DELETE: api/UserLanguages/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _userLanguageService.DeleteEntityByIdAsync(id);
            return NoContent();
        }
    }
}
