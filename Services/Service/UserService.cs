using AutoMapper;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Services.DTO;
using Services.Repository;
using System.Collections.Generic;

namespace Services.Service
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repository;
        private readonly IRepository<UserRound> _repositoryRound;
        private readonly IGroupFamilyService _serviceFamily;
        private readonly IMapper _mapper;
        public UserService(IRepository<UserRound> repositoryRound, IRepository<User> repository, IMapper mapper, IGroupFamilyService groupFamilyService) 
        {
            _repositoryRound = repositoryRound;
            _repository = repository;
            _mapper = mapper;
            _serviceFamily = groupFamilyService;
        }

        public async Task<int> AddAsync(UserCreateDto entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "UserDto cannot be null.");

            GroupsFamilyDto group = await _serviceFamily.GetGroupFamilyByIdAsync(entity.GroupFamilyId);
            if (group == null)
            {
                return 404;
            }

            var user = _mapper.Map<User>(entity);

            await _repository.AddAsync(user);

            return user.Id; // Suponiendo que Id es generado por la base de datos
        }

        public async Task<UserDto> GetUserDtoByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            return user;
        }

        public UserDto UpdateUser(int id, UserUpdateDto userUpdate, User user)
        {
            user.Name = userUpdate.Name;

            _repository.Update(user);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }


        public async Task<IEnumerable<UserDto>> GetUsersDtoByGroupIdAsync(int groupId)
        {
            var users = await _repositoryRound.GetByConditionAsync(u => u.RoundId == groupId);
            List<User> userlist = new List<User>();
            foreach (var userGroup in users)
            {
                userlist.Add(userGroup.User);
            }
            return _mapper.Map<IEnumerable<UserDto>>(userlist);
        }

        public async Task<IEnumerable<User>> GetUsersByGroupIdAsync(int groupId)
        {
            var users = await _repositoryRound.GetByConditionAsync(u => u.RoundId == groupId);
            List<User> userlist = new List<User>();
            foreach (var userGroup in users)
            {
                userlist.Add(userGroup.User);
            }
            return userlist;
        }

        public bool DeleteUserAsync(int id)
        {
            var user = _repository.GetByIdAsync(id);
            _repository.Delete(user.Result);
            return true;
        }
    }
}
