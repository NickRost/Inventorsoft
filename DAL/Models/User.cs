using DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public string Role { get; set; }

        public int? TeamId { get; set; }
        public Team Team { get; set; }

        public DateTime? RegisteredAt { get; set; }

        public ICollection<Models.Task> Tasks { get; set; }
        public ICollection<Models.Project> Projects { get; set; }
    }
}
