using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using LanguageHelper.BusinessLayer.Interfaces;

namespace LanguageHelper.BusinessLayer.Services
{
    public class SpreadSheetsService : ISpreadSheetsService
    {
        private static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
        private const string SpreadsheetId = "SpreadsheetId";
        private const string GoogleCredentialsFileName = "GoogleCredentialsSheets.json";
        private const string ReadRange = "Sheet1!A:B";
        private const string WriteRange = "A15:B15";
        /* Sheet1 - tab name in a spreadsheet
           A:B     - range of values we want to receive         */

        public SheetsService GetSheetsService()
        {
            using (var stream = new FileStream(GoogleCredentialsFileName, FileMode.Open, FileAccess.Read))
            {
                var serviceInitializer = new BaseClientService.Initializer
                {
                    HttpClientInitializer = GoogleCredential.FromStream(stream).CreateScoped(Scopes)
                };
                return new SheetsService(serviceInitializer);
            }
        }

        public async Task<string> ReadAsync(SpreadsheetsResource.ValuesResource valuesResource)
        {
            var response = await valuesResource.Get(SpreadsheetId, ReadRange).ExecuteAsync();
            var values = response.Values;
            if (values == null || !values.Any())
            {
                return "No data found.";
            }
            //"Header: "
            var result = string.Join(" ", values.First().Select(r => r.ToString())) + "\n";

            foreach (var list in values.Skip(1))
            {
                var columns = list.Select(r => r.ToString()).ToList();
                var tmp = string.Join(" ", columns);

                result = tmp.Any() && columns.Count>1 && columns[0].Any()
                    ? result + tmp + "\n "
                    : result;

            }
            return result;
        }

        public async Task WriteAsync(SpreadsheetsResource.ValuesResource valuesResource)
        {
            var valueRange = new ValueRange { Values = new List<IList<object>> { new List<object> { "stan", 18 } } };
            var update = valuesResource.Update(valueRange, SpreadsheetId, WriteRange);
            update.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
            var response = await update.ExecuteAsync();
            var forCheck = response.UpdatedData;
        }
    }
}
