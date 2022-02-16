using DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Task : BaseEntity
    {
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public int? PerformerId { get; set; }
        public User Performer { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        public State State { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
    }
}
