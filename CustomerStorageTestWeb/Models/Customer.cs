using System;

namespace CustomerStorageTestWeb.Models
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
    }
}