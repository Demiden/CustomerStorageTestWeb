using System.Collections.Generic;
using System.Linq;
using CustomerStorageTestWeb.Models;

namespace CustomerStorageTestWeb.Controllers.Mapping
{
    public static class CustomerExtensions
    {
        public static Customer ConstructCustomerRequest(this AddCustomerRequest customerRequest)
        {
            return new Customer
            {
                City = customerRequest.City,
                Name = customerRequest.Name
            };
        }
        
        public static Customer ConstructCustomerRequest(this UpdateCustomerRequest customerRequest)
        {
            return new Customer
            {
                Id = customerRequest.Id,
                City = customerRequest.City,
                Name = customerRequest.Name
            };
        }

        public static GetCustomerResponse ConstructCustomerResponse(this Customer customer)
        {
            return new GetCustomerResponse()
            {
                Id = customer.Id,
                City = customer.City,
                Name = customer.Name
            };
        }
        
        public static IEnumerable<GetCustomerResponse> ConstructCustomerResponses(this IEnumerable<Customer> customers)
        {
            return customers.Select(x => x.ConstructCustomerResponse());
        }
    }
}