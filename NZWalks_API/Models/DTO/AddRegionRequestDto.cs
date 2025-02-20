using System.ComponentModel.DataAnnotations;

namespace NZWalks_API.Models.DTO
{
    public class AddRegionRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage ="Code must have 3 characters")]
        [MaxLength(3, ErrorMessage ="Code can have only 3 characters")]
        public string Code { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage ="Name can only have 50 characters")]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
