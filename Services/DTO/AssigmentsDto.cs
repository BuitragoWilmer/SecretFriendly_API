using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class AssigmentsDto
    {
        public int UserId { get; set; }
        public int SecretSantaId { get; set; }  // El ID del amigo secreto asignado
        public UserDto User { get; set; }
        public UserDto SecretSanta { get; set; }
    }
}
