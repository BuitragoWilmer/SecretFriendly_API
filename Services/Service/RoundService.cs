using AutoMapper;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Services.DTO;
using Services.Repository;

namespace Services.Service
{
    public class RoundService : IRoundService
    {
        private readonly IRepository<Round> _repository;
        private readonly IRepository<UserRound> _repositoryUserRound;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public RoundService(IUserService userService, IRepository<UserRound> userRound,IRepository<Round> repository, IMapper mapper) 
        {
            _repositoryUserRound = userRound;
            _repository = repository;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<int> AddAsync(RoundDto entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "RoundDto cannot be null.");

            var Round = _mapper.Map<Round>(entity);
            await _repository.AddAsync(Round);
            var newRound = await _repository.GetByConditionAsync(x=>x.Description == entity.description);
            return newRound.Last().Id;
        }


        public async Task <int> AddUsertoRoundAsync(UserRoundDto entity)
        {
            RoundDto round =await GetRoundByIdAsync(entity.RoundId);
            if (round == null)
                throw new ArgumentNullException(nameof(entity), "Round cannot be null.");

            var Round = _mapper.Map<UserRound>(entity);
            await _repositoryUserRound.AddAsync(Round);

            return (int)Round.Id;
        }

        public async Task<List<User>> GetUserByIdRoundAsync(int idRound)
        {
            List<User> users = new List<User>();
            var Round = await _repositoryUserRound.GetByConditionAsync(x=>x.RoundId==idRound);
            foreach (var user in Round)
            {
                users.Add(await _userService.GetUserByIdAsync(user.UserId));
            }
            return users;
        }

        public async Task<RoundOperationDto> GetRoundByIdAsync(int id)
        {
            var round = await _repository.GetWithIncludesAsync(x => x.Id == id, x => x.UserRounds);

            RoundOperationDto dto = new RoundOperationDto();
            dto.description = round.Description;
            dto.Id = round.Id;

            foreach (var user in round.UserRounds)
            {
                dto.users.Add(_mapper.Map<UserDto>(await _userService.GetUserByIdAsync(user.UserId)));
            }
        
            return dto;
        }

        public async Task<IEnumerable<RoundOperationDto>> GetRoundAsync()
        {
            var Round = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<RoundOperationDto>>(Round);
        }

        public bool DeleteRoundAsync(int id)
        {
            var round = _repository.GetByIdAsync(id);
            _repository.Delete(round.Result);
            return true;
        }
    }
}
