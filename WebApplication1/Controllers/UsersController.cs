namespace WebApplication1.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;
    using WebApplication1.DTOs;
    using WebApplication1.Models;
    using WebApplication1.Services;

    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _service;
        private readonly IMapper _mapper;
        public UsersController(UserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserCreateDto dto)
        {
            var user = _mapper.Map<User>(dto);
            var created = await _service.CreateAsync(user);
            var readDto = _mapper.Map<UserReadDTO>(created);
            return CreatedAtAction(nameof(GetById), new { id = readDto.Id }, readDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _service.GetByIdAsync(id);
            if (user == null) return NotFound();
            var readDto = _mapper.Map<UserReadDTO>(user);
            return Ok(readDto);
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetMe()
        {
            // Récupérer l'email depuis le token
            var emailClaim = User.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.Email || c.Type == "email")?.Value;

            if (string.IsNullOrEmpty(emailClaim))
                return Unauthorized();

            // Récupérer l'utilisateur dans la DB
            var user = await _service.GetByEmailAsync(emailClaim);
            if (user == null)
                return NotFound(new { message = "Utilisateur non trouvé" });

            // Mapper en DTO
            var readDto = _mapper.Map<UserReadDTO>(user);
            return Ok(readDto);
        }
    }
}
