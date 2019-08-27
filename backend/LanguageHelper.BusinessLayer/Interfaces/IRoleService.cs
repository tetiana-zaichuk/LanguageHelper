using System.Collections.Generic;
using System.Threading.Tasks;
using LanguageHelper.Shared.Dtos;

namespace LanguageHelper.BusinessLayer.Interfaces
{
    public interface IRoleService
    {
        bool EntityValidation(RoleDto request);

        Task<IEnumerable<RoleDto>> GetAllEntitiesAsync();

        Task<RoleDto> GetEntityByIdAsync(int id);

        Task<RoleDto> CreateEntityAsync(RoleDto request);

        Task<bool> UpdateEntityByIdAsync(RoleDto request, int id);

        Task<bool> DeleteEntityByIdAsync(int id);
    }
}
