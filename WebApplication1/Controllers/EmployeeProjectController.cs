using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeProjectController : ControllerBase
    {
        private readonly IEmployeeProjectService _service;

        public EmployeeProjectController(IEmployeeProjectService service)
        {
            _service = service;
        }

        [HttpPost("assign")]
        public async Task<IActionResult> Assign(int employeeId, int projectId)
        {
            var result = await _service.AssignEmployeeToProjectAsync(employeeId, projectId);

            if (!result)
                return BadRequest("Assignment failed (invalid IDs or already assigned)");

            return Ok("Employee assigned to project");
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> Remove(int employeeId, int projectId)
        {
            var result = await _service.RemoveEmployeeFromProjectAsync(employeeId, projectId);

            if (!result)
                return NotFound("Relation not found");

            return NoContent();
        }
    }
}
