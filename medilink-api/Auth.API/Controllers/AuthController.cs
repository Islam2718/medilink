using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Models;
using Shared.Models.DTOs;
using Shared.Models.Entities;
using Shared.Services;
using System.Security.Cryptography;
using System.Text;

namespace Auth.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IJwtService _jwtService;

        public AuthController(AppDbContext context, IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserCreateDto request)
        {
            // Check if user already exists
            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
            {
                return BadRequest(new { message = "User already exists with this email" });
            }

            if (await _context.Users.AnyAsync(u => u.Username == request.Username))
            {
                return BadRequest(new { message = "Username already taken" });
            }

            // Create new user
            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                Phone = request.Phone,
                Password = HashPassword(request.Password),
                Address = request.Address,
                DateOfBirth = request.DateOfBirth,
                NationalId = request.NationalId,
                BloodGroup = request.BloodGroup,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Status = 1 // Active
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Generate token
            var token = _jwtService.GenerateToken(user);

            return Ok(new AuthResponse
            {
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddHours(24),
                UserId = user.UserId.ToString(),
                Email = user.Email,
                Username = user.Username
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto request)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email && u.Status == 1);

            if (user == null || user.Password != HashPassword(request.Password))
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }

            var token = _jwtService.GenerateToken(user);

            return Ok(new AuthResponse
            {
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddHours(24),
                UserId = user.UserId.ToString(),
                Email = user.Email,
                Username = user.Username
            });
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}