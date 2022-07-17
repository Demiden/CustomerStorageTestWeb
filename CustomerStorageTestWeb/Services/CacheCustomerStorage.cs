using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerStorageTestWeb.Models;
using CustomerStorageTestWeb.Services.Abstractions;
using Microsoft.Extensions.Caching.Memory;

namespace CustomerStorageTestWeb.Services
{
    public class CacheCustomerStorage : ICustomersRepository
    {
        private const string CustomerListCacheKey = "customerList";
        private readonly IMemoryCache _memoryCache;

        public CacheCustomerStorage(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public Task<List<Customer>> GetUsers()
        {
            if (_memoryCache.TryGetValue(CustomerListCacheKey, out List<Customer> customers))
            {
                return Task.FromResult(customers);
            }
            
            return Task.FromResult(new List<Customer>());
        }
        
        public Task<Customer> GetUserById(Guid customerId)
        {
            if (_memoryCache.TryGetValue(CustomerListCacheKey, out List<Customer> customers))
            {
                return Task.FromResult(customers.FirstOrDefault(c => c.Id == customerId));
            }
            
            return null;
        }

        public Task<Guid> AddCustomer(Customer addedCustomer)
        {
            addedCustomer.Id = Guid.NewGuid();
            
            if (_memoryCache.TryGetValue(CustomerListCacheKey, out List<Customer> customers))
            {
                customers.Add(addedCustomer);
                _memoryCache.Set(CustomerListCacheKey, customers);
            }
            else
            {
                _memoryCache.Set(CustomerListCacheKey, new List<Customer>{addedCustomer});
            }
            
            return Task.FromResult(addedCustomer.Id);
        }

        public Task<bool> UpdateCustomer(Customer updatedCustomer)
        {
            if (_memoryCache.TryGetValue(CustomerListCacheKey, out List<Customer> customers))
            {
                var customer = customers.FirstOrDefault(c => c.Id == updatedCustomer.Id);

                if (customer == null)
                    return Task.FromResult(false);

                customer.Name = updatedCustomer.Name;
                customer.City = updatedCustomer.City;

                _memoryCache.Set(CustomerListCacheKey, customers);
            }
            else
            {
                return Task.FromResult(false);
            }
            
            return Task.FromResult(true);
        }

        public Task<bool> Delete(Guid customerId)
        {
            if (_memoryCache.TryGetValue(CustomerListCacheKey, out List<Customer> customers))
            {
                var customer = customers.FirstOrDefault(c => c.Id == customerId);

                if (customer == null)
                    return Task.FromResult(false);;

                customers.Remove(customer);
                
                _memoryCache.Set(CustomerListCacheKey, customers);

                return Task.FromResult(true);;
            }
            
            return Task.FromResult(false);;
        }
    }
}