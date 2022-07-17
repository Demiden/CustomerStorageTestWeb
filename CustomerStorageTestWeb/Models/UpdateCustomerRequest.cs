using System;

namespace CustomerStorageTestWeb.Models
{
    public class UpdateCustomerRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
    }
}