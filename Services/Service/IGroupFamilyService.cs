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
    public interface IGroupFamilyService
    {
        Task<int> AddAsync(GroupsFamilyDto entity);
        Task<GroupsFamilyOperationDto> GetGroupFamilyByIdAsync(int id);
        Task<IEnumerable<GroupsFamilyOperationDto>> GetAllGroupsFamilyAsync();
        bool DeleteGroupsFamilyAsync(int id);
    }
}
