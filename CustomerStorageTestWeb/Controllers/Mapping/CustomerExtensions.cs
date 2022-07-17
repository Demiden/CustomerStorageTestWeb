using CustomerStorageTestWeb.Models;

namespace CustomerStorageTestWeb.Controllers.Mapping
{
    public static class CustomerExtensions
    {
        public static Customer ConstructCustomer(this AddCustomerRequest customerRequest)
        {
            return new Customer
            {
                City = customerRequest.City,
                Name = customerRequest.Name
            };
        }
        
        public static Customer ConstructCustomer(this UpdateCustomerRequest customerRequest)
        {
            return new Customer
            {
                Id = customerRequest.Id,
                City = customerRequest.City,
                Name = customerRequest.Name
            };
        }
    }
}