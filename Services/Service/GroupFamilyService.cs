using AutoMapper;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Services.DTO;
using Services.Repository;

namespace Services.Service
{
    public class GroupFamilyService : IGroupFamilyService
    {
        private readonly IRepository<GroupsFamily> _repository;
        private readonly IMapper _mapper;
        public GroupFamilyService(IRepository<GroupsFamily> repository, IMapper mapper) 
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<int> AddAsync(GroupsFamilyDto entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "GroupsFamilyDto cannot be null.");

            var user = _mapper.Map<GroupsFamily>(entity);
            await _repository.AddAsync(user);

            return user.Id;
        }

        public async Task<GroupsFamilyOperationDto> GetGroupFamilyByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            return _mapper.Map<GroupsFamilyOperationDto>(user);
        }

        public async Task<IEnumerable<GroupsFamilyOperationDto>> GetAllGroupsFamilyAsync()
        {
            var users = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<GroupsFamilyOperationDto>>(users);
        }

        public bool DeleteGroupsFamilyAsync(int id)
        {
            var user = _repository.GetByIdAsync(id);
            _repository.Delete(user.Result);
            return true;
        }
    }
}
