using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Assignment
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? SecretSantaId { get; set; }
        public int? RoundId { get; set; }

        public virtual Round? Round { get; set; }
        public virtual User? SecretSanta { get; set; }
        public virtual User? User { get; set; }
    }
}
