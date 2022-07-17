using System;
using System.ComponentModel.DataAnnotations;

namespace CustomerStorageTestWeb.Models
{
    public class UpdateCustomerRequest
    {
        [Required]
        public Guid Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string City { get; set; }
    }
}