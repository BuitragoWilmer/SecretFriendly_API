using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public interface IAssigmentService
    {
        Task<bool> assigmentByGroupAsync(int group);
        Task<AssigmentsDto> GetAssigmentDtoByIdAsync(int idUser, int idRound);
    }
}
