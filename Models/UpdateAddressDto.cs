using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Models
{
    public class UpdateAddressDto
    {
        [Required]
        public int id { get; set; }
        [Required]
        [MaxLength(30)]
        public string city { get; set; }
        [Required]
        [MaxLength(60)]
        public string street { get; set; }
        [Required]
        [MaxLength(6)]
        public string postal_code { get; set; }
    }
}
