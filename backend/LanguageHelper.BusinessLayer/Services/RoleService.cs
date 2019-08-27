using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LanguageHelper.BusinessLayer.Interfaces;
using LanguageHelper.DataAccessLayer.Entities;
using LanguageHelper.DataAccessLayer.Interfaces;
using LanguageHelper.Shared.Dtos;

namespace LanguageHelper.BusinessLayer.Services
{
    public class RoleService: IRoleService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public RoleService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public bool EntityValidation(RoleDto request) => !string.IsNullOrWhiteSpace(request.Title);

        public async Task<IEnumerable<RoleDto>> GetAllEntitiesAsync()
        {
            var entities = await _uow.RoleRepository.GetRangeAsync();
            return _mapper.Map<List<Role>, List<RoleDto>>(entities);
        }

        public async Task<RoleDto> GetEntityByIdAsync(int id)
        {
            var entity = await _uow.RoleRepository.GetAsync(id);
            var dto = entity == null ? null : _mapper.Map<Role, RoleDto>(entity);
            return dto;
        }

        public async Task<RoleDto> CreateEntityAsync(RoleDto request)
        {
            var entity = _mapper.Map<RoleDto, Role>(request);
            entity = await _uow.RoleRepository.CreateAsync(entity);
            var result = await _uow.SaveAsync();
            if (!result)
            {
                return null;
            }
            
            var dto = entity == null ? null : _mapper.Map<Role, RoleDto>(entity);
            return dto;
        }

        public async Task<bool> UpdateEntityByIdAsync(RoleDto request, int id)
        {
            var entity = _mapper.Map<RoleDto, Role>(request);
            entity.Id = id;
            var updated = _uow.RoleRepository.Update(entity);
            return await _uow.SaveAsync();
        }

        public async Task<bool> DeleteEntityByIdAsync(int id)
        {
            await _uow.RoleRepository.DeleteAsync(id);
            return await _uow.SaveAsync();
        }
    }
}
