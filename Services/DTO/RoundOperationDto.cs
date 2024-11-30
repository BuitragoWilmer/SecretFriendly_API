using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class RoundOperationDto : RoundDto
    {
        public int Id { get; set; }

        public List<UserDto> users { get; set; } = new List<UserDto>();
    }
}
