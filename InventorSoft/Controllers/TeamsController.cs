using BLL.DTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventorSoft.Controllers
{
    [Authorize(Roles = "Admin,Moder")]
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamService teamService;

        public TeamsController(ITeamService teamService)
        {
            this.teamService = teamService;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<TeamDto>>> GetTeams()
        {
            return Ok(await teamService.GetAllTeams());
        }

        [HttpPost]
        public async Task<ActionResult<TeamDto>> CreateTeam([FromBody] TeamCreateDto newTeam)
        {
            return Ok(await teamService.AddTeam(newTeam));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TeamDto>> UpdateTeam(int id, [FromBody] TeamDto newTeam)
        {
            return Ok(await teamService.UpdateTeam(id, newTeam));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            await teamService.DeleteTeam(id);
            return NoContent();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TeamDto>> GetTeam(int id)
        {
            return Ok(await teamService.GetTeam(id));

        }

    }

}
