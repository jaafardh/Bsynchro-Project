using Bsynchro.Domain.Entities;
using Bsynchro.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bsynchro.Services.Managers
{
    public class CustomersManager
    {
        private readonly ICustomerRepository customerRepository;
        public CustomersManager(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }
        public async Task<List<Customer>> GetCustomers()
        {
            return (await customerRepository.GetAllAsync()).ToList();
        }
        public async Task<Customer> GetCustomer(int id)
        {
            return await customerRepository.FindAsync(id);
        }
        public async Task<int> AddCustomer(Customer customer)
        {
            return await customerRepository.InsertAsync(customer);
        }
        public async void AddCustomerRange(IEnumerable<Customer> customers)
        {
            await customerRepository.InsertRangeAsync(customers);
        }
        public async Task<int> DeleteCustomer(int id)
        {
            return await customerRepository.DeleteAsyncById(id);
        }
        public async void DeleteCustomerRange(IEnumerable<Customer> customers)
        {
            await customerRepository.DeleteRangeAsync(customers, false);
        }
        public async Task<int> UpdateCustomer(Customer customer)
        {
            return await customerRepository.UpdateAsync(customer);
        }
    }
}
