using System.ComponentModel.DataAnnotations;

namespace Shared.Models.DTOs
{
    public class UserCreateDto
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; } = string.Empty;

        [StringLength(50)]
        public string? Phone { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; } = string.Empty;

        [StringLength(50)]
        public string? Address { get; set; }

        [StringLength(50)]
        public DateOnly? DateOfBirth { get; set; }

        [StringLength(50)]
        public string? NationalId { get; set; }

        [StringLength(10)]
        public string? BloodGroup { get; set; }
    }

    public class UserUpdateDto
    {
        [StringLength(50)]
        public string? Username { get; set; }

        [EmailAddress]
        [StringLength(50)]
        public string? Email { get; set; }

        [StringLength(50)]
        public string? Phone { get; set; }

        [StringLength(50)]
        public string? Address { get; set; }

        [StringLength(50)]
        public string? DateOfBirth { get; set; }

        [StringLength(50)]
        public string? NationalId { get; set; }

        [StringLength(10)]
        public string? BloodGroup { get; set; }
    }

    public class UserResponseDto
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? DateOfBirth { get; set; }
        public string? NationalId { get; set; }
        public string? BloodGroup { get; set; }
        public string? FacebookId { get; set; }
        public string? GoogleId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Status { get; set; }
    }

    public class UserLoginDto
    {
        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}