using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication1.DTOs;
using WebApplication1.Helpers;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EmployeeController(IEmployeeService service, IMapper mapper, ILogger<EmployeeController> logger, JwtService jwtService, UserService uservice, IRoleHelper roleHelper) : ControllerBase
    {
        private readonly IEmployeeService _service = service;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<EmployeeController> _logger = logger;
        private readonly JwtService _jwtService = jwtService;
        private readonly UserService _uservice = uservice;
        private readonly IRoleHelper _roleHelper = roleHelper;

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            // Chercher l'utilisateur par email
            var user = await _uservice.GetByEmailAsync(dto.Email);

            if (user == null)
                return Unauthorized(new { message = "Utilisateur non trouvé" });

            // 🔒 Ici tu peux vérifier le mot de passe
            // Pour l'instant, supposons qu'il est stocké en clair (dev/test)
            if ("123" != dto.Password)
                return Unauthorized(new { message = "Mot de passe incorrect" });

            // Générer le token
            var token = _jwtService.GenerateToken(user.Id, user.Email, user.Role);

            return Ok(new { Token = token });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllAsync()
        {
            if (!_roleHelper.IsAdmin(User))
            {
                return Forbid();
            }

            var users = await _service.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<UserReadDTO>>(users);

            return Ok(dtos);
        }

        // 🔹 GET: api/employee/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var employee = await _service.GetByIdAsync(id);
            if (employee == null)
                return NotFound();
            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeCreateDTO dto)
        {
            // 1️⃣ Validation
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // 2️⃣ Map DTO -> Entity
            var employee = _mapper.Map<Employee>(dto);

            // 3️⃣ Sauvegarde en base
            await _service.CreateAsync(employee);

            // 4️⃣ Map Entity -> ReadDTO (avec Id généré)
            var employeeReadDto = _mapper.Map<EmployeeReadDTO>(employee);

            // 5️⃣ Log
            _logger.LogInformation("Employee created with ID {Id}", employee.Id);

            // 6️⃣ Retourne CreatedAtAction
            return CreatedAtAction(
                nameof(GetByIdAsync),               // nom de l'action GET par Id
                new { id = employeeReadDto.Id }, // route values
                employeeReadDto                // corps de la réponse
            );
        }

        // 🔹 PUT: api/employee/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployeeAsync(int id, EmployeeReadDTO dto)
        {
            var employee = _mapper.Map<Employee>(dto);
            employee.Id = id;

            var updated = await _service.UpdateEmployeeAsync(employee);

            if (!updated)
                return NotFound();

            return NoContent();
        }

        // 🔹 DELETE: api/employee/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeAsync(int id)
        {
            var employee = await _service.GetByIdAsync(id);

            if (employee == null)
                return NotFound();

            await _service.DeleteEmployeeAsync(id);

            return NoContent();
        }

    }
}
