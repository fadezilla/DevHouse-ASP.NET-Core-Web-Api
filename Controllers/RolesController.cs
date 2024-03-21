using devhouse.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using devhouse.Data;

namespace devhouse.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public RoleController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            if (_dataContext.Roles == null)
            {
                return NotFound();
            }

            return await _dataContext.Roles.ToListAsync();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Role>> GetRole(int Id)
        {
            if(_dataContext.Roles == null)
            {
                return NotFound();
            }
            var role = await _dataContext.Roles.FindAsync(Id);
            if(role is null)
            {
                return NotFound();
            }
            return role;
        }
        [HttpPost]
         public async Task<ActionResult<Role>> AddRole(Role role)
        {
            _dataContext.Roles.Add(role);
            await _dataContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRole), new {id = role.Id}, role);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<Role>> UpdateRole(int Id, Role role)
        {
            if (Id != role.Id)
            {
                return BadRequest();
            }
            
            _dataContext.Update(role);
            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!RoleExists(Id))
                {
                    return NotFound();
                }
                else { throw; }
            }
            return NoContent();
        }
        private bool RoleExists(int id)
        {
            return (_dataContext.Roles?.Any(role => role.Id == id)).GetValueOrDefault();
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<Role>> DeleteRole(int Id)
        {
            if(_dataContext.Roles == null)
            {
                return NotFound();
            }
            var role = await _dataContext.Roles.FindAsync(Id);
            if(role is null)
            {
                return NotFound();
            }
            _dataContext.Roles.Remove(role);
            await _dataContext.SaveChangesAsync();
            return NoContent();
        }

    }
}