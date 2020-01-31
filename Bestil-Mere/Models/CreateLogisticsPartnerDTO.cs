using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class CreateLogisticsPartnerDTO
    {
        [Required]
        public string Name { get; set; }
    }
}