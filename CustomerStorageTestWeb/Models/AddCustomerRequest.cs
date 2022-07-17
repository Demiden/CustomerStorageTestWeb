using System.ComponentModel.DataAnnotations;

namespace CustomerStorageTestWeb.Models
{
    public class AddCustomerRequest
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string City { get; set; }
    }
}