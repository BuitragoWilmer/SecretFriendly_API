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
    public interface IRoundService
    {
        Task<int> AddAsync(RoundDto entity);
        Task<RoundOperationDto> GetRoundByIdAsync(int id);
        Task<IEnumerable<RoundOperationDto>> GetRoundAsync();
        bool DeleteRoundAsync(int id);
        Task<int> AddUsertoRoundAsync(UserRoundDto entity);

        Task<List<User>> GetUserByIdRoundAsync(int idRound);
    }
}
