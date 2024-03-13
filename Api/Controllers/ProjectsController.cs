using Business;
using Entities.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var projects = await _projectService.GetAllProjectsAsync();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null) return NotFound();
            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProjectDetailsVm project)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var createdProject = await _projectService.CreateProjectAsync(project);
            return CreatedAtAction(nameof(GetById), new { id = createdProject.ProjectId }, createdProject);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(ProjectDetailsVm project)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _projectService.UpdateProjectAsync(project);
            return NoContent();
        }
    }
}
