using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LanguageHelper.BusinessLayer.Interfaces;
using LanguageHelper.DataAccessLayer.Entities;
using LanguageHelper.DataAccessLayer.Interfaces;
using LanguageHelper.Shared.Dtos;

namespace LanguageHelper.BusinessLayer.Services
{
    public class UserLanguageService : IUserLanguageService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public UserLanguageService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public bool EntityValidation(UserLanguageDto request)
            => !string.IsNullOrWhiteSpace(request.Language) && _uow.UserRepository.GetAsync(request.UserId)!=null;

        public async Task<IEnumerable<UserLanguageDto>> GetAllEntitiesAsync()
        {
            var entities = await _uow.UserLanguageRepository.GetRangeAsync();
            return _mapper.Map<List<UserLanguage>, List<UserLanguageDto>>(entities);
        }

        public async Task<UserLanguageDto> GetEntityByIdAsync(int id)
        {
            var entity = await _uow.UserLanguageRepository.GetAsync(id);
            var dto = entity == null ? null : _mapper.Map<UserLanguage, UserLanguageDto>(entity);
            return dto;
        }

        public async Task<UserLanguageDto> CreateEntityAsync(UserLanguageDto request)
        {
            var entity = _mapper.Map<UserLanguageDto, UserLanguage>(request);
            entity = await _uow.UserLanguageRepository.CreateAsync(entity);
            var result = await _uow.SaveAsync();
            if (!result)
            {
                return null;
            }

            var dto = entity == null ? null : _mapper.Map<UserLanguage, UserLanguageDto>(entity);
            return dto;
        }

        public async Task<bool> UpdateEntityByIdAsync(UserLanguageDto request, int id)
        {
            var entity = _mapper.Map<UserLanguageDto, UserLanguage>(request);
            entity.Id = id;
            var updated = _uow.UserLanguageRepository.Update(entity);
            return await _uow.SaveAsync();
        }

        public async Task<bool> DeleteEntityByIdAsync(int id)
        {
            await _uow.UserLanguageRepository.DeleteAsync(id);
            return await _uow.SaveAsync();
        }
    }
}
