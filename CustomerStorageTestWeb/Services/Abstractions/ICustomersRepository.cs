using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerStorageTestWeb.Models;

namespace CustomerStorageTestWeb.Services.Abstractions
{
    public interface ICustomersRepository
    {
        /// <summary>
        /// Get customer by id
        /// </summary>
        Task<Customer> GetUserById(Guid customerId);
        Task<List<Customer>> GetUsers();
        Task<Guid> AddCustomer(Customer addedCustomer);
        Task<bool> UpdateCustomer(Customer updatedCustomer);
        Task<bool> Delete(Guid customerId);
    }
}