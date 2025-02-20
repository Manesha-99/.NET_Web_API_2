using System.ComponentModel.DataAnnotations;

namespace NZWalks_API.Models.DTO
{
    public class ChangeWalkDto
    {
        [Required]
        [MaxLength(255, ErrorMessage = "Name has only 255 characters")]
        public string Name { get; set; }

        [Required]
        [MaxLength(255, ErrorMessage = "Description has only 255 characters")]
        public string Description { get; set; }

        [Required]
        [Range(0,1000, ErrorMessage = "Length must be below 1000km")]
        public double LengthInKm { get; set; }

        public string? WalkImageUrl { get; set; }
        
        [Required]
        public Guid DifficultyId { get; set; }

        [Required]
        public Guid RegionId { get; set; }
    }
}
