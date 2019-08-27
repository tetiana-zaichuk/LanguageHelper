using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using LanguageHelper.BusinessLayer.Interfaces;
using LanguageHelper.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LanguageHelper.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class SheetsController : ControllerBase
    { 
        /*
        private readonly ISpreadSheetsService _sheetsService;

        public SheetsController(ISpreadSheetsService service)
        {
            _sheetsService = service;
        }

       
        [HttpGet]
        public async Task<string> Get()
        {
            var serviceValues = _sheetsService.GetSheetsService().Spreadsheets.Values;
            return await _sheetsService.ReadAsync(serviceValues);
        }*/

        private readonly ISheetService _sheetService;

        public SheetsController(ISheetService sheetService) => _sheetService = sheetService;

        // GET: api/Sheets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SheetDto>>> Get()
        {
            var result = await _sheetService.GetAllEntitiesAsync();
            if (!result.Any())
                return NoContent();
            return Ok(result);
        }

        // GET: api/Sheets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SheetDto>> Get(int id)
        {
            var result = await _sheetService.GetEntityByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        // POST: api/Sheets
        [HttpPost]
        public async Task<ActionResult<SheetDto>> Post([FromBody] SheetDto value)
        {
            if (!_sheetService.EntityValidation(value))
                return BadRequest("Please fill out these fields: title, spreadsheet id, last used and user id.");
            
            var result = await _sheetService.CreateEntityAsync(value);
            if (result == null)
                return StatusCode(500);
            return Ok(result);
        }

        // PUT: api/Sheets/5
        [HttpPut("{id}")]
        public async Task<ActionResult<SheetDto>> Put(int id, [FromBody] SheetDto value)
        {
            if (!_sheetService.EntityValidation(value))
                return BadRequest("Please fill out these fields: title, spreadsheet id, last used and user id.");

            var result = await _sheetService.UpdateEntityByIdAsync(value, id);
            return !result ? StatusCode(500) : Ok();
        }

        // DELETE: api/Sheets/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _sheetService.DeleteEntityByIdAsync(id);
            return NoContent();
        }

    }
}