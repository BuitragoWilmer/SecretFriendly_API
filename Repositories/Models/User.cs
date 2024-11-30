using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class User
    {
        public User()
        {
            AssignmentSecretSanta = new HashSet<Assignment>();
            Gifts = new HashSet<Gift>();
            UserRounds = new HashSet<UserRound>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? GroupFamilyId { get; set; }

        public virtual GroupsFamily? GroupFamily { get; set; }
        public virtual Assignment? AssignmentUser { get; set; }
        public virtual ICollection<Assignment> AssignmentSecretSanta { get; set; }
        public virtual ICollection<Gift> Gifts { get; set; }
        public virtual ICollection<UserRound> UserRounds { get; set; }
    }
}
