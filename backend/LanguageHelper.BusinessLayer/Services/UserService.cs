using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LanguageHelper.BusinessLayer.Interfaces;
using LanguageHelper.DataAccessLayer.Entities;
using LanguageHelper.DataAccessLayer.Interfaces;
using LanguageHelper.Shared.Dtos;

namespace LanguageHelper.BusinessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public bool EntityValidation(UserDto request) 
            => !string.IsNullOrWhiteSpace(request.NickName) && !string.IsNullOrWhiteSpace(request.Email) && 
                request.LastActive > request.CreatedAt && _uow.RoleRepository.GetAsync(request.RoleId) != null;

        public async Task<IEnumerable<UserDto>> GetAllEntitiesAsync()
        {
            var entities = await _uow.UserRepository.GetRangeAsync();
            return _mapper.Map<List<User>, List<UserDto>>(entities);
        }

        public async Task<UserDto> GetEntityByIdAsync(int id)
        {
            var entity = await _uow.UserRepository.GetAsync(id);
            var dto = entity == null ? null : _mapper.Map<User, UserDto>(entity);
            return dto;
        }

        public async Task<UserDto> CreateEntityAsync(UserDto request)
        {
            var entity = _mapper.Map<UserDto, User>(request);
            entity = await _uow.UserRepository.CreateAsync(entity);
            var result = await _uow.SaveAsync();
            if (!result)
            {
                return null;
            }

            var dto = entity == null ? null : _mapper.Map<User, UserDto>(entity);
            return dto;
        }

        public async Task<bool> UpdateEntityByIdAsync(UserDto request, int id)
        {
            var entity = _mapper.Map<UserDto, User>(request);
            entity.Id = id;
            var updated = _uow.UserRepository.Update(entity);
            return await _uow.SaveAsync();
        }

        public async Task<bool> DeleteEntityByIdAsync(int id)
        {
            await _uow.UserRepository.DeleteAsync(id);
            return await _uow.SaveAsync();
        }
    }
}
