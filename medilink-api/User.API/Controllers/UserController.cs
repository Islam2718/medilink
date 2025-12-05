using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Shared.Models;
using Shared.Models.DTOs;
using Shared.Models.Entities;
using System.Security.Claims;

namespace User.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int id))
            {
                return Unauthorized();
            }

            var user = await _context.Users
                .Where(u => u.UserId == id && u.Status == 1)
                .Select(u => new UserResponseDto
                {
                    UserId = u.UserId,
                    Username = u.Username,
                    Email = u.Email,
                    Phone = u.Phone,
                    Address = u.Address,
                    // DateOfBirth = u.DateOfBirth,
                    NationalId = u.NationalId,
                    BloodGroup = u.BloodGroup,
                    FacebookId = u.FacebookId,
                    GoogleId = u.GoogleId,
                    CreatedAt = u.CreatedAt,
                    UpdatedAt = u.UpdatedAt,
                    Status = u.Status
                })
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            return Ok(user);
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UserUpdateDto request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int id))
            {
                return Unauthorized();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null || user.Status != 1)
            {
                return NotFound(new { message = "User not found" });
            }

            // Update fields if provided
            if (!string.IsNullOrEmpty(request.Username)) 
                user.Username = request.Username;
            if (!string.IsNullOrEmpty(request.Email)) 
                user.Email = request.Email;
            if (!string.IsNullOrEmpty(request.Phone)) 
                user.Phone = request.Phone;
            if (!string.IsNullOrEmpty(request.Address)) 
                user.Address = request.Address;
            // if (!string.IsNullOrEmpty(request.DateOfBirth)) 
            //     user.DateOfBirth = request.DateOfBirth;
            if (!string.IsNullOrEmpty(request.NationalId)) 
                user.NationalId = request.NationalId;
            if (!string.IsNullOrEmpty(request.BloodGroup)) 
                user.BloodGroup = request.BloodGroup;

            user.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Profile updated successfully" });
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _context.Users
                .Where(u => u.UserId == id && u.Status == 1)
                .Select(u => new UserResponseDto
                {
                    UserId = u.UserId,
                    Username = u.Username,
                    Email = u.Email,
                    Phone = u.Phone,
                    Address = u.Address,
                    // DateOfBirth = u.DateOfBirth,
                    NationalId = u.NationalId,
                    BloodGroup = u.BloodGroup,
                    CreatedAt = u.CreatedAt,
                    UpdatedAt = u.UpdatedAt,
                    Status = u.Status
                })
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            return Ok(user);
        }
    }
}