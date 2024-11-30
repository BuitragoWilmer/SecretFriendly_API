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
    public interface IGiftService 
    {
        Task<int> AddAsync(GiftOperationDto entity);
        Task<GiftDto> GetGiftDtoByIdAsync(int id);
        Task<IEnumerable<GiftDto>> GetAllGiftsAsync();
        bool DeleteGiftAsync(int id);
    }
}
