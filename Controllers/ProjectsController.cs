using devhouse.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using devhouse.Data;

namespace devhouse.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public ProjectsController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public class ProjectTool
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int ProjectTypeId { get; set; }
            public int TeamId { get; set; }
            public string ProjectType { get; set; }
            public string Team { get; set; }
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectTool>>> GetProjects()
        {
           var project = await _dataContext.Projects
                .Include(p => p.Team)
                .Include(p => p.ProjectType)
                .Select(p => new ProjectTool
                {
                    Id = p.Id,
                    Name = p.Name,
                    ProjectTypeId = p.ProjectTypeId,
                    ProjectType = p.ProjectType.Name,
                    TeamId = p.TeamId,
                    Team = p.Team.Name
                }).ToListAsync();

            if(project == null)
            {
                return NotFound();
            }
            return project;
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<ProjectTool>> GetProject(int Id)
        {
            var project = await _dataContext.Projects
                .Where(p => p.Id == Id)
                .Include(p => p.ProjectType)
                .Include(p => p.Team)
                .Select(p => new ProjectTool
                {
                    Id = p.Id,
                    Name = p.Name,
                    ProjectTypeId = p.ProjectTypeId,
                    ProjectType = p.ProjectType.Name,
                    TeamId = p.TeamId,
                    Team = p.Team.Name
                }).FirstOrDefaultAsync();

                if(project == null)
                {
                    return NotFound();
                }
                
                return project;
        }
        [HttpPost]
         public async Task<ActionResult<Project>> AddProject(Project project)
        {
            _dataContext.Projects.Add(project);
            await _dataContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProject), new {id = project.Id}, project);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<Project>> UpdateProject(int Id, Project project)
        {
            if (Id != project.Id)
            {
                return BadRequest();
            }
            
            _dataContext.Update(project);
            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!ProjectExists(Id))
                {
                    return NotFound();
                }
                else { throw; }
            }
            return NoContent();
        }
        private bool ProjectExists(int id)
        {
            return (_dataContext.Projects?.Any(project => project.Id == id)).GetValueOrDefault();
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<Project>> DeleteProject(int Id)
        {
            if(_dataContext.Projects == null)
            {
                return NotFound();
            }
            var project = await _dataContext.Projects.FindAsync(Id);
            if(project is null)
            {
                return NotFound();
            }
            _dataContext.Projects.Remove(project);
            await _dataContext.SaveChangesAsync();
            return NoContent();
        }

    }
    
    
}