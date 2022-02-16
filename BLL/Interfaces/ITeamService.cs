using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ITeamService
    {
        Task<TeamDto> AddTeam(TeamCreateDto team);
        Task<ICollection<TeamDto>> GetAllTeams();
        Task<TeamDto> GetTeam(int id);
        Task<TeamDto> UpdateTeam(int id, TeamDto team);
        Task DeleteTeam(int id);
    }
}
