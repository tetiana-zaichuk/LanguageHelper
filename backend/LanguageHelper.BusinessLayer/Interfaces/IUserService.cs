using System.Collections.Generic;
using System.Threading.Tasks;
using LanguageHelper.Shared.Dtos;

namespace LanguageHelper.BusinessLayer.Interfaces
{
    public interface IUserService
    {
        bool EntityValidation(UserDto request);

        Task<IEnumerable<UserDto>> GetAllEntitiesAsync();

        Task<UserDto> GetEntityByIdAsync(int id);

        Task<UserDto> CreateEntityAsync(UserDto request);

        Task<bool> UpdateEntityByIdAsync(UserDto request, int id);

        Task<bool> DeleteEntityByIdAsync(int id);
    }
}
