using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class UpdateRestaurantModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string RestaurantName { get; set; }
       
        [Required]
        public string RestaurantType { get; set; }
        
        [Required]
        public string Cvr { get; set; }
        
        [Required]
        public string Address { get; set; }

        [Required]
        public string PostalCode { get; set; }

        [Required]
        public string City { get; set; }
        
        [Required]
        public string Country { get; set; }
        
    }
}