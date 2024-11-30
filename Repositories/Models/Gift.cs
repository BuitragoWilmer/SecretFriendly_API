using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Gift
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string? Message { get; set; }

        public virtual User? User { get; set; }
    }
}
