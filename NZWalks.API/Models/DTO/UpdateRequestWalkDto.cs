using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class UpdateRequestWalkDto
    {
        [Required]
        [MaxLength(30, ErrorMessage = "Name has to be 30 Characteres or less")]
        [MinLength(3, ErrorMessage = "Name has to be 3 Characteres or more")]
        public string Name { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Description has to be 100 Characteres or less")]
        [MinLength(10, ErrorMessage = "Description has to be 10 Characteres or more")]
        public string Description { get; set; }
        [Required]
        [Range(0,50, ErrorMessage = "Length has to be greater than 0")]
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

    }
}
