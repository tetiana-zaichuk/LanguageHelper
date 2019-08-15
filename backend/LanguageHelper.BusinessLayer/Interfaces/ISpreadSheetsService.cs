using System.Threading.Tasks;
using Google.Apis.Sheets.v4;

namespace LanguageHelper.BusinessLayer.Interfaces
{
    public interface ISpreadSheetsService
    {
        SheetsService GetSheetsService();

        Task<string> ReadAsync(SpreadsheetsResource.ValuesResource valuesResource);

        Task WriteAsync(SpreadsheetsResource.ValuesResource valuesResource);
    }
}
