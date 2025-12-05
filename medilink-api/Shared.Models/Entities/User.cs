using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Models.Entities
{
    [Table("User")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
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

        [StringLength(50)]
        public string? FacebookId { get; set; }

        [StringLength(50)]
        public string? GoogleId { get; set; }

        [Column(TypeName = "varbinary(50)")]
        public byte[]? LinkedInId { get; set; }

        // public string Role { get; set; } = string.Empty; // Patient, Doctor, MedicalStore, Admin

        [Column(TypeName = "date")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column(TypeName = "date")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public int Status { get; set; } = 1; // Default active status
        // public bool IsActive { get; set; } = true;        
    }
}