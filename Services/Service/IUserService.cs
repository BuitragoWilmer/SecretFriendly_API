using Data.Models;
using Services.DTO;
using Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public interface IUserService 
    {
        Task<int> AddAsync(UserCreateDto entity);
        Task<UserDto> GetUserDtoByIdAsync(int id);
        Task<User> GetUserByIdAsync(int id);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        bool DeleteUserAsync(int id);
        Task<IEnumerable<User>> GetUsersByGroupIdAsync(int groupId);
        UserDto UpdateUser(int id, UserUpdateDto userUpdate, User user);
    }
}
