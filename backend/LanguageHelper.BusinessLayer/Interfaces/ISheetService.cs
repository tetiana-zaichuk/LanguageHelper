using System.Collections.Generic;
using System.Threading.Tasks;
using LanguageHelper.Shared.Dtos;

namespace LanguageHelper.BusinessLayer.Interfaces
{
    public interface ISheetService
    {
        bool EntityValidation(SheetDto request);

        Task<IEnumerable<SheetDto>> GetAllEntitiesAsync();

        Task<SheetDto> GetEntityByIdAsync(int id);

        Task<SheetDto> CreateEntityAsync(SheetDto request);

        Task<bool> UpdateEntityByIdAsync(SheetDto request, int id);

        Task<bool> DeleteEntityByIdAsync(int id);
    }
}
