using devhouse.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using devhouse.Data;

namespace devhouse.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ProjectTypesController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public ProjectTypesController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectType>>> GetProjectTypes()
        {
            if (_dataContext.ProjectTypes == null)
            {
                return NotFound();
            }

            return await _dataContext.ProjectTypes.ToListAsync();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<ProjectType>> GetProjectType(int Id)
        {
            if(_dataContext.ProjectTypes == null)
            {
                return NotFound();
            }
            var projectType = await _dataContext.ProjectTypes.FindAsync(Id);
            if(projectType is null)
            {
                return NotFound();
            }
            return projectType;
        }
        [HttpPost]
         public async Task<ActionResult<ProjectType>> AddProjectType(ProjectType projectType)
        {
            _dataContext.ProjectTypes.Add(projectType);
            await _dataContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProjectType), new {id = projectType.Id}, projectType);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<ProjectType>> UpdateProjectType(int Id, ProjectType projectType)
        {
            if (Id != projectType.Id)
            {
                return BadRequest();
            }
            
            _dataContext.Update(projectType);
            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!ProjectTypeExists(Id))
                {
                    return NotFound();
                }
                else { throw; }
            }
            return NoContent();
        }
        private bool ProjectTypeExists(int id)
        {
            return (_dataContext.ProjectTypes?.Any(projectType => projectType.Id == id)).GetValueOrDefault();
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<ProjectType>> DeleteProjectType(int Id)
        {
            if(_dataContext.ProjectTypes == null)
            {
                return NotFound();
            }
            var projectType = await _dataContext.ProjectTypes.FindAsync(Id);
            if(projectType is null)
            {
                return NotFound();
            }
            _dataContext.ProjectTypes.Remove(projectType);
            await _dataContext.SaveChangesAsync();
            return NoContent();
        }

    }
}