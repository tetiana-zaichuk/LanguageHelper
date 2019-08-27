using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LanguageHelper.BusinessLayer.Interfaces;
using LanguageHelper.DataAccessLayer.Entities;
using LanguageHelper.DataAccessLayer.Interfaces;
using LanguageHelper.Shared.Dtos;

namespace LanguageHelper.BusinessLayer.Services
{
    public class SheetService : ISheetService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public SheetService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public bool EntityValidation(SheetDto request)
            => !string.IsNullOrWhiteSpace(request.Title) && !string.IsNullOrWhiteSpace(request.SpreadsheetId) &&
               _uow.UserRepository.GetAsync(request.UserId) != null;

        public async Task<IEnumerable<SheetDto>> GetAllEntitiesAsync()
        {
            var entities = await _uow.SheetRepository.GetRangeAsync();
            return _mapper.Map<List<Sheet>, List<SheetDto>>(entities);
        }

        public async Task<SheetDto> GetEntityByIdAsync(int id)
        {
            var entity = await _uow.SheetRepository.GetAsync(id);
            var dto = entity == null ? null : _mapper.Map<Sheet, SheetDto>(entity);
            return dto;
        }

        public async Task<SheetDto> CreateEntityAsync(SheetDto request)
        {
            var entity = _mapper.Map<SheetDto, Sheet>(request);
            entity = await _uow.SheetRepository.CreateAsync(entity);
            var result = await _uow.SaveAsync();
            if (!result)
            {
                return null;
            }

            var dto = entity == null ? null : _mapper.Map<Sheet, SheetDto>(entity);
            return dto;
        }

        public async Task<bool> UpdateEntityByIdAsync(SheetDto request, int id)
        {
            var entity = _mapper.Map<SheetDto, Sheet>(request);
            entity.Id = id;
            var updated = _uow.SheetRepository.Update(entity);
            return await _uow.SaveAsync();
        }

        public async Task<bool> DeleteEntityByIdAsync(int id)
        {
            await _uow.SheetRepository.DeleteAsync(id);
            return await _uow.SaveAsync();
        }
    }
}
