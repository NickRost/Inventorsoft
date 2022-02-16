using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public TeamDto Team { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime? RegisteredAt { get; set; }
        public DateTime? BirthDay { get; set; }
    }
}
