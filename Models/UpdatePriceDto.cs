using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Models
{
    public class UpdatePriceDto
    {
        [Required]
        [Range(0, 999.99)]
        public decimal price { get; set; }
    }
}
