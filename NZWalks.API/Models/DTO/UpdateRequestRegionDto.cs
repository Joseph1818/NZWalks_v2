using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class UpdateRequestRegionDto
    {
        [Required]
        [MaxLength(15, ErrorMessage = "Name has to be 15 Characteres or less")]
        [MinLength(3, ErrorMessage = "Name has to be 3 Characteres or more")]
        public string Name { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "Description has to be 30 Characteres or less")]
        [MinLength(10, ErrorMessage = "Description has to be 10 Characteres or more")]
        public string Code { get; set; }
        public string RegionImageUrl { get; set; }
    }
}
