
using Accord.Math.Distances;
using AutoMapper;
using Data.Models;
using Services.DTO;
using Services.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Xml;

namespace Services.Service
{
    public class AssigmentService: IAssigmentService
    {
        private readonly IRoundService _roundService;
        private readonly IRepository<Assignment> _repository;
        private readonly IMapper _mapper;
        private List<Assignment> assignmentsforAdd = new List<Assignment>();
        public AssigmentService(IRepository<Assignment> repository, IMapper mapper, IRoundService roundService)
        {
            _repository = repository;
            _roundService = roundService;
            _mapper = mapper;
        }

        public async Task<AssigmentsDto> GetAssigmentDtoByIdAsync(int idUser, int idRound)
        {
            var filters = new List<Expression<Func<Assignment, bool>>>
            {
                x => x.UserId == idUser,  
                x => x.RoundId==idRound,        
            };

            var user = await _repository.GetWithIncludesAndFiltersAsync(filters, x=>x.SecretSanta);
            return _mapper.Map<AssigmentsDto>(user);
        }

        public async Task<bool> assigmentByGroupAsync(int idRound) 
        {
            List<User> userList = await _roundService.GetUserByIdRoundAsync(idRound);
            Dictionary<int, List<User>> usersbyFamily = SplitByGroup(userList);
            int count =0;
            while(!matchUser( usersbyFamily, idRound))
            {
                assignmentsforAdd.Clear();             
                if(count == 10)
                {
                    MatchUsersWithoutGroups(userList, idRound);
                    break;

                }
                count++;
            }
            foreach(Assignment assignment in assignmentsforAdd)
            {
               await  _repository.AddAsync(assignment);
            }
            return true;

        }

        private bool matchUser(Dictionary<int, List<User>> usersbyFamily, int IdRound)
        {
            Random random = new Random();
            HashSet<int> usersPaired = new HashSet<int>();

            foreach (KeyValuePair<int, List<User>> familyGroup in usersbyFamily)
            {
                List<User> potencialMatch = getUsersdifferentGroup(new Dictionary<int, List<User>>(usersbyFamily), familyGroup.Key, usersPaired);
                if (potencialMatch.Count() < familyGroup.Value.Count)
                {
                    return false;
                }

                foreach (User user in familyGroup.Value)
                {

                    User userPick = potencialMatch[random.Next(potencialMatch.Count)];
                    assignmentsforAdd.Add(new Assignment { SecretSantaId = userPick.Id, SecretSanta = userPick, User = user, UserId = user.Id, RoundId = IdRound });
                    potencialMatch.Remove(userPick);
                    usersPaired.Add(userPick.Id);
                }
            }
            return true;

        }

        private Dictionary<int, List<User>> SplitByGroup(List<User> items)
        {
            // Usar un diccionario para agrupar por número de grupo
            Dictionary<int, List<User>> grupos = new Dictionary<int, List<User>>();

            foreach (var item in items)
            {
                int idGroup = (int)item.GroupFamilyId;
                if (!grupos.ContainsKey(idGroup))
                {
                    grupos[idGroup] = new List<User>();
                }
                grupos[idGroup].Add(item);
            }
            return grupos;
        }
        private List<User> getUsersdifferentGroup(Dictionary<int, List<User>> usersbyFamily, int currentGroup, HashSet<int> usersPaired)
        {
            List<User> users = new List<User>();
            usersbyFamily.Remove(currentGroup);

            foreach (List<User> familyGroup in usersbyFamily.Values)
            {
                foreach (User user in familyGroup)
                {
                    if (!usersPaired.Contains(user.Id))
                        users.Add(user);
                }              
            }

            return users;
        }


        private void MatchUsersWithoutGroups(List<User> users, int IdRound)
        {
            if (users.Count < 2)
            {
                throw new ArgumentException("Debe haber al menos dos usuarios para realizar emparejamientos.");
            }

            var givers = new List<User>(users);
            var receivers = new List<User>(users);
            var random = new Random();
            var matches = new Dictionary<User, User>();

            foreach (var giver in givers)
            {
                // Filtra para evitar que un usuario se empareje consigo mismo
                var possibleReceivers = receivers.Where(r => r.Id != giver.Id).ToList();

                if (possibleReceivers.Count == 0)
                {
                    assignmentsforAdd.Clear();
                }

                // Selecciona un receptor aleatorio
                var receiver = possibleReceivers[random.Next(possibleReceivers.Count)];

                assignmentsforAdd.Add(new Assignment { SecretSantaId = receiver.Id, UserId = giver.Id, RoundId = IdRound });

                // Elimina el receptor de la lista para evitar repeticiones
                receivers.Remove(receiver);
            }


        }


    }
}
