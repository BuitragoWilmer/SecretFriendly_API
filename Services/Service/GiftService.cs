using AutoMapper;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Services.DTO;
using Services.Repository;

namespace Services.Service
{
    public class GiftService : IGiftService
    {
        private readonly IRepository<Gift> _repository;
        private readonly IMapper _mapper;
        public GiftService(IRepository<Gift> repository, IMapper mapper) 
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<int> AddAsync(GiftOperationDto entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "GiftDto cannot be null.");

            var user = _mapper.Map<Gift>(entity);
            await _repository.AddAsync(user);

            return user.Id;
        }

        public async Task<GiftDto> GetGiftDtoByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            return _mapper.Map<GiftDto>(user);
        }

        public async Task<IEnumerable<GiftDto>> GetAllGiftsAsync()
        {
            var users = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<GiftDto>>(users);
        }

        public bool DeleteGiftAsync(int id)
        {
            var gift = _repository.GetByIdAsync(id);
            _repository.Delete(gift.Result);
            return true;
        }
    }
}
