using System.ComponentModel.DataAnnotations;

namespace WalksAPI.Models.DTO
{
    public class UpdateRegionRequestDto
    {
        [Required]
        [MinLength(2, ErrorMessage = "Code has to be a minimum of 2 characters")]
        [MaxLength(3, ErrorMessage = "Code has to be a maximum of 3 characters")]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        public String? RegionImageUrl { get; set; }
    }
}
