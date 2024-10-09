using devhouse.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using devhouse.Data;

namespace devhouse.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class DeveloperController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public DeveloperController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public class DeveloperTool
        {
            public int Id { get; set; }
            public required string FirstName { get; set; }
            public required string LastName { get; set; }
            public int RoleId { get; set; }
            public int TeamId { get; set; }
            public required string Role { get; set; }
            public required string Team { get; set; }
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeveloperTool>>> GetDevelopers()
        {
           var developer = await _dataContext.Developers
                .Include(p => p.Team)
                .Include(p => p.Role)
                .Select(p => new DeveloperTool
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    RoleId = p.RoleId,
                    Role = p.Role != null ? p.Role.Name :"No role",
                    TeamId = p.TeamId,
                    Team = p.Team != null ? p.Team.Name : "No team"
                }).ToListAsync();

            if(developer == null)
            {
                return NotFound();
            }
            return developer;
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<DeveloperTool>> GetDeveloper(int Id)
        {
            var developer = await _dataContext.Developers
                .Where(p => p.Id == Id)
                .Include(p => p.Role)
                .Include(p => p.Team)
                .Select(p => new DeveloperTool
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    RoleId = p.RoleId,
                    Role = p.Role != null ? p.Role.Name :"No role",
                    TeamId = p.TeamId,
                    Team = p.Team != null ? p.Team.Name : "No team"
                }).FirstOrDefaultAsync();

                if(developer == null)
                {
                    return NotFound();
                }
                
                return developer;
        }
        [HttpPost]
         public async Task<ActionResult<Developer>> AddDeveloper(Developer developer)
        {
            _dataContext.Developers.Add(developer);
            await _dataContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDeveloper), new {id = developer.Id}, developer);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<Developer>> UpdateDeveloper(int Id, Developer developer)
        {
            if (Id != developer.Id)
            {
                return BadRequest();
            }
            
            _dataContext.Update(developer);
            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!DeveloperExists(Id))
                {
                    return NotFound();
                }
                else { throw; }
            }
            return NoContent();
        }
        private bool DeveloperExists(int id)
        {
            return (_dataContext.Developers?.Any(developer => developer.Id == id)).GetValueOrDefault();
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<Developer>> DeleteDeveloper(int Id)
        {
            if(_dataContext.Developers == null)
            {
                return NotFound();
            }
            var developer = await _dataContext.Developers.FindAsync(Id);
            if(developer is null)
            {
                return NotFound();
            }
            _dataContext.Developers.Remove(developer);
            await _dataContext.SaveChangesAsync();
            return NoContent();
        }

    }

}