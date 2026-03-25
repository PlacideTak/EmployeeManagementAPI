using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _service;
        private readonly IMapper _mapper;

        public ProjectController(IProjectService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: api/project
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectReadDTO>>> GetAll()
        {
            var projects = await _service.GetAllProjectsAsync();
            if (!projects.Any()) return NotFound();

            var projectDTOs = _mapper.Map<IEnumerable<ProjectReadDTO>>(projects);
            return Ok(projectDTOs);
        }

        // GET: api/project/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectReadDTO>> GetById(int id)
        {
            var project = await _service.GetProjectByIdAsync(id);
            if (project == null) return NotFound();

            var projectDTO = _mapper.Map<ProjectReadDTO>(project);
            return Ok(projectDTO);
        }

        [HttpPost]
        public async Task<ActionResult<ProjectReadDTO>> Create([FromBody] ProjectCreateUpdateDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var project = _mapper.Map<Project>(dto);
            await _service.AddProjectAsync(project, dto.EmployeeIds);

            var projectDTO = _mapper.Map<ProjectReadDTO>(project);
            return CreatedAtAction(nameof(GetById), new { id = project.Id }, projectDTO);
        }

        // PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProjectCreateUpdateDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existingProject = await _service.GetProjectByIdAsync(id);
            if (existingProject == null) return NotFound();

            _mapper.Map(dto, existingProject);
            await _service.UpdateProjectAsync(existingProject, dto.EmployeeIds);

            return NoContent();
        }

        // DELETE: api/project/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var project = await _service.GetProjectByIdAsync(id);
            if (project == null) return NotFound();

            await _service.DeleteProjectAsync(project);
            return NoContent();
        }
    }
}
