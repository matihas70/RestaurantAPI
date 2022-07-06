using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Models
{
    public class AddRestaurantDto
    {
        [Required]
        [MaxLength(40)]
        public string name { get; set; }
        [MaxLength(20)]
        public string category { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(40)]
        public string email { get; set; }
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
