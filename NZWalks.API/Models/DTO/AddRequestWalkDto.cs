using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class AddRequestWalkDto
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "Name has to be 30 Characteres or less")]
        [MinLength(5, ErrorMessage = "Name has to be 5 Characteres or more")]
        public string Name { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Description has to be 100 Characteres or less")]
        [MinLength(10, ErrorMessage = "Description has to be 10 Characteres or more")]
        public string Description { get; set; }
        [Required]
        [Range(0, 50, ErrorMessage = "Length in Km has to be between 0 and 50")]
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }

    }
}
