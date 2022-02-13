using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public class Team : BaseEntity
    {
        public string Name { get; set; }
        public DateTime? CreatedAt { get; set; }

        public ICollection<User> Users { get; set; }
    }
}