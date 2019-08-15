using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using LanguageHelper.BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LanguageHelper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SheetsController : ControllerBase
    {
        private readonly ISpreadSheetsService _sheetsService;

        public SheetsController(ISpreadSheetsService service)
        {
            _sheetsService = service;
        }

        // GET api/sheets
        [HttpGet]
        public async Task<string> Get()
        {
            var serviceValues = _sheetsService.GetSheetsService().Spreadsheets.Values;
            return await _sheetsService.ReadAsync(serviceValues);
        }
        //will get the string with numbers 0 or 1 separated by space
        // POST api/sheets
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

    }
}