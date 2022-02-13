using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class TeamService : ITeamService
    {
        protected readonly ProjectContext _context;
        protected readonly IMapper _mapper;

        public TeamService(ProjectContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TeamDto> AddTeam(TeamCreateDto team)
        {
            var item = _mapper.Map<Team>(team);
            item.CreatedAt = DateTime.Now;
            await _context.Teams.AddAsync(item);
            await _context.SaveChangesAsync();

            var created = await _context.Teams.FirstAsync(u => u.Id == item.Id);

            return _mapper.Map<TeamDto>(created);
        }

        public async System.Threading.Tasks.Task DeleteTeam(int id)
        {
            Team team =  _mapper.Map<Team>(await GetTeam(id));
            if (team == null)
                throw new System.Exception("Invalid id");

            _context.Teams.Remove(team);
             await _context.SaveChangesAsync();
        }

        public async Task<ICollection<TeamDto>> GetAllTeams()
        {
            ICollection<Team> teams = await _context.Teams.ToListAsync();
            var result = _mapper.Map<ICollection<TeamDto>>(teams);
            return result;
        }

        public async Task<TeamDto> GetTeam(int id)
        {
            var result = await _context.Teams.FirstOrDefaultAsync(c => c.Id == id);
            return _mapper.Map<TeamDto>(result);
        }

        public async Task<TeamDto> UpdateTeam(int id, TeamDto item)
        {
            Team team = _mapper.Map<Team>(await GetTeam(id));
            if (team == null)
                throw new System.Exception("Invalid id");
            team.Name = item.Name;
            team.CreatedAt = team.CreatedAt;
            await _context.SaveChangesAsync();
            return _mapper.Map<TeamDto>(team);
        }
    }
}
