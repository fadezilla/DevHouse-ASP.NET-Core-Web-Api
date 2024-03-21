using devhouse.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using devhouse.Data;

namespace devhouse.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public TeamController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
        {
            if (_dataContext.Teams == null)
            {
                return NotFound();
            }

            return await _dataContext.Teams.ToListAsync();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Team>> GetTeam(int Id)
        {
            if(_dataContext.Teams == null)
            {
                return NotFound();
            }
            var team = await _dataContext.Teams.FindAsync(Id);
            if(team is null)
            {
                return NotFound();
            }
            return team;
        }
        [HttpPost]
         public async Task<ActionResult<Team>> AddTeam(Team team)
        {
            _dataContext.Teams.Add(team);
            await _dataContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTeam), new {id = team.Id}, team);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<Team>> UpdateTeam(int Id, Team team)
        {
            if (Id != team.Id)
            {
                return BadRequest();
            }
            
            _dataContext.Update(team);
            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!TeamExists(Id))
                {
                    return NotFound();
                }
                else { throw; }
            }
            return NoContent();
        }
        private bool TeamExists(int id)
        {
            return (_dataContext.Teams?.Any(team => team.Id == id)).GetValueOrDefault();
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<Team>> DeleteTeam(int Id)
        {
            if(_dataContext.Teams == null)
            {
                return NotFound();
            }
            var team = await _dataContext.Teams.FindAsync(Id);
            if(team is null)
            {
                return NotFound();
            }
            _dataContext.Teams.Remove(team);
            await _dataContext.SaveChangesAsync();
            return NoContent();
        }

    }
}