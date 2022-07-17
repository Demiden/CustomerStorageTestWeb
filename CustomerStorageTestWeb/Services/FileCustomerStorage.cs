using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CustomerStorageTestWeb.Models;
using CustomerStorageTestWeb.Services.Abstractions;
using Newtonsoft.Json;

namespace CustomerStorageTestWeb.Services
{
    public class FileCustomerStorage : ICustomersRepository
    {
        private const string BasePath = "CustomerStorage";
        private const string FileStorageName = "CustomerStorage.json";
        public async Task<Customer> GetUserById(Guid customerId)
        {
            var customers = await ReadCustomers();

            return customers.FirstOrDefault(c => c.Id == customerId);
        }

        public async Task<List<Customer>> GetUsers()
        {
            return await ReadCustomers();
        }

        public async Task<Guid> AddCustomer(Customer addedCustomer)
        {
            addedCustomer.Id = Guid.NewGuid();
            var customers = await ReadCustomers();
            
            if (customers != null)
            {
                customers.Add(addedCustomer);
            }
            else
            {
                customers = new List<Customer> {addedCustomer};
            }
            
            await SaveCustomers(customers);

            return addedCustomer.Id;
        }

        public async Task<bool> UpdateCustomer(Customer updatedCustomer)
        {
            var customers = await ReadCustomers();
            
            if (customers != null)
            {
                var customer = customers.FirstOrDefault(c => c.Id == updatedCustomer.Id);

                if (customer == null)
                    return false;

                customer.Name = updatedCustomer.Name;
                customer.City = updatedCustomer.City;
            }
            else
            {
                return false;
            }
            
            await SaveCustomers(customers);

            return true;
        }

        public async Task<bool> Delete(Guid customerId)
        {
            var customers = await ReadCustomers();
            
            if (customers != null)
            {
                var customer = customers.FirstOrDefault(c => c.Id == customerId);

                if (customer == null)
                    return false;
                
                customers.Remove(customer);
                
                await SaveCustomers(customers);

                return true;
            }

            return false;
        }

        private async Task<List<Customer>> ReadCustomers()
        {
            var path = Path.Combine(BasePath, FileStorageName);

            if (File.Exists(path))
            {
                return JsonConvert.DeserializeObject<List<Customer>>(await File.ReadAllTextAsync(path));
            }

            return new List<Customer>();
        }

        private async Task SaveCustomers(List<Customer> customers)
        {
            var path = Path.Combine(BasePath, FileStorageName);
            
            if (!Directory.Exists(BasePath))
            {
                Directory.CreateDirectory(BasePath);
            }
            
            await File.WriteAllTextAsync(path, JsonConvert.SerializeObject(customers));
        }
    }
}