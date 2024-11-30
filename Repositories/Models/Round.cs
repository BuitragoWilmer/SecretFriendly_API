using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Round
    {
        public Round()
        {
            Assignments = new HashSet<Assignment>();
            UserRounds = new HashSet<UserRound>();
        }

        public int Id { get; set; }
        public string Description { get; set; } = null!;

        public virtual ICollection<Assignment> Assignments { get; set; }
        public virtual ICollection<UserRound> UserRounds { get; set; }
    }
}
