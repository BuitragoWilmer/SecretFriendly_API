using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class UserRound
    {
        public ulong Id { get; set; }
        public int UserId { get; set; }
        public int RoundId { get; set; }

        public virtual Round Round { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
