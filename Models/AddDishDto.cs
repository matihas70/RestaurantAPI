using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Models
{
    public class AddDishDto
    {
        [Required]
        [MaxLength(40)]
        public string name { get; set; }
        [Required]
        [MaxLength(30)]
        public string type { get; set; }
        [Required]
        [Range(0, 999.99)]
        public decimal price { get; set; }
    }
}
