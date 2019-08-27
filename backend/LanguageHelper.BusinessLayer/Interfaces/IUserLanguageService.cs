using System.Collections.Generic;
using System.Threading.Tasks;
using LanguageHelper.Shared.Dtos;

namespace LanguageHelper.BusinessLayer.Interfaces
{
    public interface IUserLanguageService
    {
        bool EntityValidation(UserLanguageDto request);

        Task<IEnumerable<UserLanguageDto>> GetAllEntitiesAsync();

        Task<UserLanguageDto> GetEntityByIdAsync(int id);

        Task<UserLanguageDto> CreateEntityAsync(UserLanguageDto request);

        Task<bool> UpdateEntityByIdAsync(UserLanguageDto request, int id);

        Task<bool> DeleteEntityByIdAsync(int id);
    }
}
