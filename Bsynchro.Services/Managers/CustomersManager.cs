using Bsynchro.Domain.Entities;
using Bsynchro.Domain.Repositories;
using Newtonsoft.Json;
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
        private readonly ITransactionRepository transactionRepository;
        private readonly IAccountRepository accountRepository;
        public CustomersManager(ICustomerRepository customerRepository, ITransactionRepository transactionRepository,IAccountRepository accountRepository)
        {
            this.customerRepository = customerRepository;
            this.transactionRepository = transactionRepository;
            this.accountRepository = accountRepository;
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

        public async Task<Customer> GetFullCustomer(int customerId)
        {
            var customer = await customerRepository.FindAsync(customerId);
            customer.Accounts = accountRepository.GetCustomerAccounts(customerId);
            return customer;
        }

        public async Task<int> UpdateCustomer(Customer customer)
        {
            return await customerRepository.UpdateAsync(customer);
        }
        public async Task<int> AddAccount(int customerId,double initialCredit)
        {
            var result = await customerRepository.AddAccount(customerId);
            if (result == null) { return -1; }  //failed to add account
            if(initialCredit > 0)
            {
                var accounts = result.ToList();
                if (accounts.Count < 2) { return -2; } // failed in accounts
                Transaction transaction = new Transaction
                {
                    SenderId = accounts[0].AccountId,
                    RecipientId = accounts.Last().AccountId,
                    Amount = initialCredit,
                };
                return await transactionRepository.Transact(transaction);
            }
            else
            {
                return 1;
            }
        }
    }
}
